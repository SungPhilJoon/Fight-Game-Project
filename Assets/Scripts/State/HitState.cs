using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class HitState : State<PlayerCharacterController>
    {
        #region Variables
        private Animator animator;

        #endregion Variables

        #region Animator Hash Code
        private readonly int hashHitTrigger = Animator.StringToHash("Hit");

        #endregion Animator Hash Code

        public override void OnInitialized()
        {
            animator = context.GetComponentInChildren<Animator>();
        }

        public override void OnEnter()
        {
            animator.SetTrigger(hashHitTrigger);
        }

        public override void Update(float deltaTime)
        {
            if (stateMachine.ElapsedTimeInState > 0.3f)
            {
                stateMachine.ChangeState<IdleState>();
            }
        }

        public override void OnExit()
        {

        }
    }
}