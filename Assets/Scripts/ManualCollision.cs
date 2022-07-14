using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class ManualCollision : MonoBehaviour
    {
        #region Variables
        public Vector3 boxSize;
        private PlayerCharacterController playerController;

        public Collider[] targetColliders;

        #endregion Variables

        #region Unity Methods
        void Awake()
        {
            playerController = GetComponentInParent<PlayerCharacterController>();
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(Vector3.zero, boxSize);
        }

#endif

#endregion Unity Methods

        #region Helper Methods
        public void CheckCollision()
        {
            targetColliders = Physics.OverlapBox(transform.position, boxSize * 0.5f, transform.rotation, playerController.targetMask);
        }

        #endregion Helper Methods
    }
}