using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class ManualCollision : MonoBehaviour
    {
        #region Variables
        public Vector3 boxSize;

        #endregion Variables

        #region Unity Methods
        void OnDrawGizmos()
        {
            
        }

        #endregion Unity Methods

        #region Helper Methods
        public void CheckCollision()
        {
            //Collider[] opponentCollider = Physics.OverlapBox()
        }

        #endregion Helper Methods
    }
}