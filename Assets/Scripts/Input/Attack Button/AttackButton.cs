using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public enum CommandButton
    {
        LeftPunch,
        RightPunch,
        LeftKick,
        RightKick,
    }

    public class AttackButton : MonoBehaviour
    {
        #region Delegate
        private Action InputMethod;

        #endregion Delegate

        #region Variables
        public CommandButton commandButton;

        [SerializeField] private PlayerCharacterController playerController;

        #endregion Variables

        #region Properties
        public PlayerCharacterController PlayerController => playerController;

        #endregion Properties

        #region Unity Methods
        void Awake()
        {
            switch(commandButton)
            {
                case CommandButton.LeftPunch:
                    InputMethod = playerController.LeftPunch;
                    break;
                case CommandButton.RightPunch:
                    InputMethod = playerController.RightPunch;
                    break;
                case CommandButton.LeftKick:
                    InputMethod = playerController.LeftKick;
                    break;
                case CommandButton.RightKick:
                    InputMethod = playerController.RightKick;
                    break;
            }
        }

        #endregion Unity Methods

        #region Helper Methods
        public void InputCommand()
        {
            CommandManager.Instance.inputCommandList.Add(commandButton);

            InputMethod.Invoke();

            playerController.StateMachine.ChangeState<AttackState>();
        }

        #endregion Helper Methods
    }
}