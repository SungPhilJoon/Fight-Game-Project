using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class AttackState : State<PlayerCharacterController>
    {
        private ManualCollision upperManualCollision;
        private ManualCollision lowerManualCollision;

        public override void OnInitialized()
        {
            upperManualCollision = context.upperManualCollision;
            lowerManualCollision = context.lowerManualCollision;
        }

        public override void OnEnter()
        {
            
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