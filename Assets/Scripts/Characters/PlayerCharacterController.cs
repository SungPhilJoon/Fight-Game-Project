using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class PlayerCharacterController : MonoBehaviour, IDamageable, ICommandable
    {
        #region Event
        public delegate void DeadEvent();

        public event DeadEvent OnDeadEvent;

        #endregion Event

        #region Variables
        public PlayerCharacterController opponentPlayerController;
        [SerializeField] protected VirtualJoyStick virtualJoyStick;
        private StateMachine<PlayerCharacterController> stateMachine;
        private Animator animator;
        private Rigidbody rigid;

        private int health = 200;
        private int damage;
        private int playerMask;

        protected Direction inputDirection;

        [Header("캐릭터 스피드")]
        public float speed = 2.0f;
        [Header("캐릭터 점프 높이")]
        public float jumpHeightAmount = 2.0f;
        [Header("Hit Effect")]
        [SerializeField] private GameObject hitEffect;
        [Header("Ground Mask")]
        [SerializeField] private LayerMask groundMask;
        [Header("Target Mask")]
        public LayerMask targetMask;
        [Header("Manual Collision")]
        public ManualCollision leftPunchManualCollision;
        public ManualCollision rightPunchManualCollision;
        public ManualCollision leftKickManualCollision;
        public ManualCollision rightKickManualCollision;


        #endregion Variables

        #region Animator Hash Code
        protected readonly int hashMagnitude = Animator.StringToHash("Magnitude");
        protected readonly int hashHorizontal = Animator.StringToHash("Horizontal");
        protected readonly int hashVertical = Animator.StringToHash("Vertical");

        protected readonly int hashJump = Animator.StringToHash("Jump");
        protected readonly int hashFall = Animator.StringToHash("Fall");

        protected readonly int hashLeftPunch = Animator.StringToHash("LeftPunch");
        protected readonly int hashRightPunch = Animator.StringToHash("RightPunch");
        protected readonly int hashLeftKick = Animator.StringToHash("LeftKick");
        protected readonly int hashRightKick = Animator.StringToHash("RightKick");

        protected readonly int hashHitTrigger = Animator.StringToHash("Hit");
        protected readonly int hashDeadTrigger = Animator.StringToHash("Dead");

        #endregion Animator Hash Code

        #region Properties
        public VirtualJoyStick VirtualJoyStick => virtualJoyStick;

        public int Health
        {
            set => health = value;

            get => health;
        }

        public int Damage
        {
            get => damage;
            set => damage = value;
        }

        public StateMachine<PlayerCharacterController> StateMachine => stateMachine;

        #endregion Properties

        #region Unity Methods
        protected virtual void Awake()
        {
            PlayerCharacterController[] characters = FindObjectsOfType<PlayerCharacterController>();
            foreach(PlayerCharacterController character in characters)
            {
                if (character.gameObject == this.gameObject)
                {
                    continue;
                }

                opponentPlayerController = character;
            }

            animator = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();

            stateMachine = new StateMachine<PlayerCharacterController>(this, new IdleState());
            stateMachine.AddState(new MoveState());
            stateMachine.AddState(new AttackState());
            stateMachine.AddState(new HitState());
            stateMachine.AddState(new DeadState());
        }

        protected virtual void Start()
        {
            playerMask = this.gameObject.layer;

            if (playerMask.Equals(LayerMask.NameToLayer("1P")))
            {
                targetMask = 1 << LayerMask.NameToLayer("2P");
            }
            else if (playerMask.Equals(LayerMask.NameToLayer("2P")))
            {
                targetMask = 1 << LayerMask.NameToLayer("1P");
            }
        }

        protected virtual void Update()
        {
            MoveCharacter();

            stateMachine.Update(Time.deltaTime);
        }

        #endregion Unity Methods

        #region Helper Methods
        private void MoveCharacter()
        {
            if (virtualJoyStick == null)
            {
                return;
            }

            if (CommandManager.Instance.isInputButton)
            {
                return;
            }

            // Func<float, float> f = x => -4 * x + 3.2f; 

            inputDirection = virtualJoyStick.CalcDirection();

            animator.SetFloat(hashMagnitude, virtualJoyStick.InputDirection.magnitude);

            if (inputDirection.Equals(Direction.Middle))
            {
                animator.SetFloat(hashHorizontal, virtualJoyStick.InputDirection.x);
                animator.SetFloat(hashVertical, virtualJoyStick.InputDirection.y);
            }
            else if (inputDirection.Equals(Direction.Right))
            {
                transform.position += Vector3.forward * speed * Time.deltaTime * virtualJoyStick.InputDirection.x;
                animator.SetFloat(hashHorizontal, virtualJoyStick.InputDirection.x);
                animator.SetFloat(hashVertical, virtualJoyStick.InputDirection.y);
            }
            else if (inputDirection.Equals(Direction.Left))
            {
                transform.position += Vector3.back * speed * Time.deltaTime * -virtualJoyStick.InputDirection.x;
                animator.SetFloat(hashHorizontal, virtualJoyStick.InputDirection.x);
                animator.SetFloat(hashVertical, virtualJoyStick.InputDirection.y);
            }
            else if (inputDirection.Equals(Direction.Up) || inputDirection.Equals(Direction.RightUp) || inputDirection.Equals(Direction.LeftUp))
            {
                if (!PlayerIsGround())
                {
                    return;
                }
                else
                {
                    animator.SetTrigger(hashFall);
                }

                CommandManager.Instance.calcInputTime += Time.deltaTime;
                if (CommandManager.Instance.calcInputTime > CommandManager.Instance.checkInputTime)
                {
                    animator.SetTrigger(hashJump);

                    CommandManager.Instance.calcInputTime = 0f;

                    Vector3 jumpDirection = new Vector3(0f, virtualJoyStick.InputDirection.y, virtualJoyStick.InputDirection.x);
                    rigid.AddForce(jumpDirection * jumpHeightAmount, ForceMode.Impulse);
                }
            }
            else if (inputDirection.Equals(Direction.Down) || inputDirection.Equals(Direction.RightDown) || inputDirection.Equals(Direction.LeftDown))
            {
                transform.position += Vector3.forward * speed * Time.deltaTime * virtualJoyStick.InputDirection.x;

                animator.SetFloat(hashHorizontal, virtualJoyStick.InputDirection.x);
                animator.SetFloat(hashVertical, virtualJoyStick.InputDirection.y);
            }
        }

        private bool PlayerIsGround()
        {
            if (Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, 0.03f, groundMask))
            {
                return true;
            }

            return false;
        }

        #endregion Helper Methods

        #region IDamageable
        public void TakeDamage(int damage, Transform hitTransform = null)
        {
            health -= damage;

            if (hitTransform != null)
            {
                Instantiate(hitEffect, hitTransform.position, Quaternion.identity);
            }

            if (health <= 0)
            {
                stateMachine.ChangeState<DeadState>();
                OnDeadEvent.Invoke();

                return;
            }
            else
            {
                stateMachine.ChangeState<HitState>();
            }
        }

        #endregion IDamageable

        #region ICommandable
        public void LeftPunch()
        {
            animator.SetTrigger(hashLeftPunch);
            animator.ResetTrigger(hashRightPunch);
            animator.ResetTrigger(hashLeftKick);
            animator.ResetTrigger(hashRightKick);
        }

        public void RightPunch()
        {
            animator.SetTrigger(hashRightPunch);
            animator.ResetTrigger(hashLeftPunch);
            animator.ResetTrigger(hashLeftKick);
            animator.ResetTrigger(hashRightKick);
        }

        public void LeftKick()
        {
            animator.SetTrigger(hashLeftKick);
            animator.ResetTrigger(hashLeftPunch);
            animator.ResetTrigger(hashRightPunch);
            animator.ResetTrigger(hashRightKick);
        }

        public void RightKick()
        {
            animator.SetTrigger(hashRightKick);
            animator.ResetTrigger(hashLeftPunch);
            animator.ResetTrigger(hashRightPunch);
            animator.ResetTrigger(hashLeftKick);
        }

        public void OnExecuteLeftPunchAttack()
        {
            leftPunchManualCollision.CheckCollision();

            if (leftPunchManualCollision.targetColliders.Length == 0)
            {
                return;
            }

            if (leftPunchManualCollision.targetColliders[0].TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        public void OnExecuteRightPunchAttack()
        {
            rightPunchManualCollision.CheckCollision();

            if (rightPunchManualCollision.targetColliders.Length == 0)
            {
                return;
            }

            if (rightPunchManualCollision.targetColliders[0].TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        public void OnExecuteLeftKickAttack()
        {
            leftKickManualCollision.CheckCollision();

            if (leftKickManualCollision.targetColliders.Length == 0)
            {
                return;
            }

            if (leftKickManualCollision.targetColliders[0].TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        public void OnExecuteRightKickAttack()
        {
            rightKickManualCollision.CheckCollision();

            if (rightKickManualCollision.targetColliders.Length == 0)
            {
                return;
            }

            if (rightKickManualCollision.targetColliders[0].TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        public void CheckDirection()
        {
            Direction direction = virtualJoyStick.CalcDirection();

            CommandManager.Instance.PushDirection(direction);

            switch(direction)
            {
                case Direction.Middle:
                    
                    break;
            }
        }

        #endregion ICommandable
    }
}