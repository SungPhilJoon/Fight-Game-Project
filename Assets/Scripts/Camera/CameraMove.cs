using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class CameraMove : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float distance;
        [SerializeField] private float limitDistance;
        [SerializeField] private float cameraSensitivly;

        private Transform player1;
        private Transform player2;

        #endregion Variables

        #region Properties
        public float PlayerInterval => Vector3.Distance(player1.position, player2.position);

        public float CameraPositionZ
        {
            get => (player2.position.z + player1.position.z) / 2;
        }

        #endregion Properties

        #region Unity Methods
        void Awake()
        {
            player1 = GameManager.Instance.Player1.transform;
            player2 = GameManager.Instance.Player2.transform;
        }

        void Start()
        {
            distance = Vector3.Distance(player1.position, player2.position);
        }

        void Update()
        {
            MoveCamera();
        }

        #endregion Unity Methods

        #region Helper Methods
        private void MoveCamera()
        {
            if (PlayerInterval < distance)
            {
                return;
            }

            float interval = PlayerInterval - distance;

            if (interval > limitDistance)
            {
                interval = 4.0f;
            }

            transform.localPosition = new Vector3(interval * cameraSensitivly, 0f, CameraPositionZ);
        }

        #endregion Helper Methods
    }
}