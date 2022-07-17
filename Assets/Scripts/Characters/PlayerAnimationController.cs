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
        public void OnExecuteUpperAttack()
        {
            playerController.OnExecuteUpperAttack();
        }

        public void OnExecuteLowerAttack()
        {
            playerController.OnExecuteLowerAttack();
        }

        #endregion Helper Methods
    }
}