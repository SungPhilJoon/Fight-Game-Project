using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    [CreateAssetMenu(fileName = "New Command", menuName = "Command/New Command Object")]
    public class CommandObject : ScriptableObject
    {
        #region Variables
        public List<Command> commandList;

        public int commandNumber;

        #endregion Variables

        #region Unity Methods
        void OnEnable()
        {
            
        }

        #endregion Unity Methods

        #region Helper Methods


        #endregion Helper Methods
    }
}