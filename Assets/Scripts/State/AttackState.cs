using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class AttackState : State<PlayerCharacterController>
    {
        #region Variables
        private Animator animator;
        private VirtualJoyStick virtualJoyStick;

        #endregion Variables

        #region Animator Hash Code
        protected readonly int hashMiddle = Animator.StringToHash("Middle");
        protected readonly int hashUp = Animator.StringToHash("Up");
        protected readonly int hashDown = Animator.StringToHash("Down");
        protected readonly int hashRight = Animator.StringToHash("Right");
        protected readonly int hashLeft = Animator.StringToHash("Left");
        protected readonly int hashRightUp = Animator.StringToHash("RightUp");
        protected readonly int hashRightDown = Animator.StringToHash("RightDown");
        protected readonly int hashLeftUp = Animator.StringToHash("LeftUp");
        protected readonly int hashLeftDown = Animator.StringToHash("LeftDown");

        #endregion Animator Hash Code

        public override void OnInitialized()
        {
            animator = context.GetComponentInChildren<Animator>();
            virtualJoyStick = context.VirtualJoyStick;
        }

        public override void OnEnter()
        {
            CommandManager.Instance.isInputButton = true;
        }

        public override void Update(float deltaTime)
        {
            if (stateMachine.ElapsedTimeInState > 0.4f)
            {
                stateMachine.ChangeState<IdleState>();
            }
        }

        public override void OnExit()
        {
            CommandManager.Instance.isInputButton = false;
        }

        #region Helper Methods
        public void CheckDirection()
        {
            Direction direction = virtualJoyStick.CalcDirection();

            switch (direction)
            {
                case Direction.Middle:

                    break;
            }
        }

        /// <summary>
        /// 애미메이터의 트리거를 리셋 해주는 함수
        /// </summary>
        /// <param name="direction"></param>
        private void ResetDirectionTrigger(Direction direction)
        {
            switch (direction)
            {
                case Direction.Middle:      animator.ResetTrigger(hashMiddle);  break;
                case Direction.Up:          animator.ResetTrigger(hashUp);  break;
                case Direction.Down:        animator.ResetTrigger(hashDown);    break;
                case Direction.Right:       animator.ResetTrigger(hashRight);   break;
                case Direction.Left:        animator.ResetTrigger(hashLeft);    break;
                case Direction.RightUp:     animator.ResetTrigger(hashRightUp); break;
                case Direction.RightDown:   animator.ResetTrigger(hashRightDown);   break;
                case Direction.LeftUp:      animator.ResetTrigger(hashLeftUp);  break;
                case Direction.LeftDown:    animator.ResetTrigger(hashLeftDown);    break;
            }
        }

        #endregion Helper Methods
    }
}