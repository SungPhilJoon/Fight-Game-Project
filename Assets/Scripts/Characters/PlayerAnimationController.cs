using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Variables
        private PlayerCharacterController playerController;

        #endregion Variables

        #region Unity Methods
        void Awake()
        {
            playerController = GetComponentInParent<PlayerCharacterController>();
        }

        #endregion Unity Methods

        #region Helper Methods
        public void OnExecuteLeftPunchAttack()
        {
            playerController.OnExecuteLeftPunchAttack();
        }

        public void OnExecuteRightPunchAttack()
        {
            playerController.OnExecuteRightPunchAttack();
        }

        public void OnExecuteLeftKickAttack()
        {
            playerController.OnExecuteLeftKickAttack();
        }

        public void OnExecuteRightKickAttack()
        {
            playerController.OnExecuteRightKickAttack();
        }

        #endregion Helper Methods
    }
}