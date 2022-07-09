using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class PlayerCharacterController : MonoBehaviour, IDamageable, ICommandable
    {
        #region Variables
        protected PlayerCharacterController opponentPlayerController;
        protected VirtualJoyStick virtualJoyStick;
        private Animator animator;
        private Rigidbody rigid;

        private int health = 200;

        protected Direction inputDirection;

        [Header("ĳ���� ���ǵ�")]
        public float speed = 2.0f;
        [Header("ĳ���� ���� ����")]
        public float jumpHeightAmount = 2.0f;
        [Header("Ground Mask")]
        [SerializeField] private LayerMask groundMask;

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

            virtualJoyStick = FindObjectOfType<VirtualJoyStick>();
            animator = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            MoveCharacter();
        }

        #endregion Unity Methods

        #region Helper Methods
        private void MoveCharacter()
        {
            if (CommandManager.Instance.isInputButton)
            {
                return;
            }

            // Func<float, float> f = x => -4 * x + 3.2f; 

            inputDirection = virtualJoyStick.CalcLeverDirection();

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
            else if (inputDirection.Equals(Direction.Up))
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
            else if (inputDirection.Equals(Direction.Down))
            {
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
        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                animator.applyRootMotion = true;
                animator.SetTrigger(hashDeadTrigger);
                return;
            }

            animator.SetTrigger(hashHitTrigger);
            animator.ResetTrigger(hashHitTrigger);
        }

        #endregion IDamageable

        #region ICommandable
        public void LeftPunch()
        {
            animator.SetTrigger(hashLeftPunch);
        }

        public void RightPunch()
        {
            animator.SetTrigger(hashRightPunch);
        }

        public void LeftKick()
        {
            animator.SetTrigger(hashLeftKick);
        }

        public void RightKick()
        {
            animator.SetTrigger(hashRightKick);
        }

        #endregion ICommandable
    }
}