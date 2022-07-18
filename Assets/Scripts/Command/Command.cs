using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    [Serializable]
    public class Command
    {
        #region Delagate
        public Action<Direction, float> knockbackAction; 

        #endregion Delegate

        #region Variables
        public CommandButton command;
        public List<Direction> directions;
        public string decideCollision;

        public int damage;
        public int decreaseDamage;

        public Direction knockbackDirection;
        public float knockbackForce;

        public string commandName;

        private string upperAttackCollision = "Upper";
        private string lowerAttackCollision = "Lower";

        #endregion Variables

        #region Helper Methods
        public void InitDecideCollision(string decideCollision)
        {
            int hashUpper = upperAttackCollision.GetHashCode();
            int hashLower = lowerAttackCollision.GetHashCode();

            int hashDecide = decideCollision.GetHashCode();

            if (hashDecide.Equals(hashUpper) || hashDecide.Equals(hashLower))
            {
                this.decideCollision = decideCollision;
                return;
            }

            Debug.Log("This Keyword is not exist");
        }

        #endregion Helper Methods
    }
}