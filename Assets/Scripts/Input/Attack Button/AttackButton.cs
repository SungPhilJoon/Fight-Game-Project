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
        #region Variables
        public CommandButton commandButton;

        #endregion Variables

        #region Properties


        #endregion Properties

        #region Helper Methods
        public void InputCommand()
        {
            CommandManager.Instance.inputCommandList.Add(commandButton);
            CommandManager.Instance.isInputButton = true;
        }

        #endregion Helper Methods
    }
}