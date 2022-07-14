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
            if (commandButton.Equals(CommandButton.LeftPunch))
            {
                InputMethod = playerController.LeftPunch;
            }
            else if (commandButton.Equals(CommandButton.RightPunch))
            {
                InputMethod = playerController.RightPunch;
            }
            else if (commandButton.Equals(CommandButton.LeftKick))
            {
                InputMethod = playerController.LeftKick;
            }
            else if (commandButton.Equals(CommandButton.RightKick))
            {
                InputMethod = playerController.RightKick;
            }
        }

        #endregion Unity Methods

        #region Helper Methods
        public void InputCommand()
        {
            CommandManager.Instance.inputCommandList.Add(commandButton);
            CommandManager.Instance.isInputButton = true;

            InputMethod.Invoke();

            playerController.StateMachine.ChangeState<AttackState>();
        }

        

        #endregion Helper Methods
    }
}