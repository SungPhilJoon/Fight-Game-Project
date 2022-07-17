using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class IdleState : State<PlayerCharacterController>
    {
        public override void OnInitialized()
        {

        }

        public override void OnEnter()
        {
            CommandManager.Instance.directionContain.Push(Direction.Middle);
        }

        public override void Update(float deltaTime)
        {
            
        }

        public override void OnExit()
        {

        }
    }
}