using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class DeadState : State<PlayerCharacterController>
    {
        #region Variables
        private Animator animator;

        #endregion Variables

        #region Animator Hash Code
        private readonly int hashDeadTrigger = Animator.StringToHash("Dead");

        #endregion Animator Hash Code

        public override void OnInitialized()
        {
            animator = context.GetComponentInChildren<Animator>();
        }

        public override void OnEnter()
        {
            animator.applyRootMotion = true;
            animator.SetTrigger(hashDeadTrigger);

        }

        public override void Update(float deltaTime)
        {

        }

        public override void OnExit()
        {

        }
    }
}