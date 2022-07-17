using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class AttackState : State<PlayerCharacterController>
    {
        #region Variables
        private ManualCollision upperManualCollision;
        private ManualCollision lowerManualCollision;

        private Animator animator;

        #endregion Variables

        public override void OnInitialized()
        {
            upperManualCollision = context.upperManualCollision;
            lowerManualCollision = context.lowerManualCollision;

            animator = context.GetComponentInChildren<Animator>();
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
    }
}