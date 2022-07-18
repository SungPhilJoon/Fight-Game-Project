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

        public Transform opponentTransform;
        public int commandNumber;

        #endregion Variables

        #region Unity Methods
        void OnEnable()
        {
            foreach (Command command in commandList)
            {
                command.knockbackAction -= OnKnockbackAction;
                command.knockbackAction += OnKnockbackAction;
            }
        }

        #endregion Unity Methods

        #region Helper Methods
        public void OnKnockbackAction(Direction knockbackDirection, float knockbackForce)
        {
            switch (knockbackDirection)
            {
                case Direction.Up:
                    opponentTransform.position += Vector3.up * Time.deltaTime * knockbackForce;
                    break;
                case Direction.Right:
                    opponentTransform.position += Vector3.forward * Time.deltaTime * knockbackForce;
                    break;
            }
        }

        #endregion Helper Methods
    }
}