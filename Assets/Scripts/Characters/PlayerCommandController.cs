using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class PlayerCommandController : MonoBehaviour
    {
        #region Variables
        public CommandObject[] commandObjects;

        #endregion Variables

        #region Unity Methods


        #endregion Unity Methods

        #region Helper Methods
        public void SearchCommandObject()
        {
            foreach (CommandObject commandObject in commandObjects)
            {
                List<CommandObject> tempList = new List<CommandObject>();

                if (commandObject.commandList[0].directions[0].Equals(CommandManager.Instance.PeekDirection()))
                {
                    tempList.Add(commandObject);
                }

                foreach(CommandObject temp in tempList)
                {

                }
            }
        }

        #endregion Helper Methods
    }
}