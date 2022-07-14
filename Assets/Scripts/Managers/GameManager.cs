using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class GameManager : Singleton<GameManager>
    {
        #region Variables
        [SerializeField] private PlayerCharacterController player1;
        [SerializeField] private PlayerCharacterController player2;

        private bool isEndRound = false;

        #endregion Variables

        #region Properties
        public PlayerCharacterController Player1 => player1;
        public PlayerCharacterController Player2 => player2;

        public bool IsEndRound => isEndRound;

        #endregion Properties

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();

            player1.gameObject.layer = LayerMask.NameToLayer("1P");
            player2.gameObject.layer = LayerMask.NameToLayer("2P");
        }

        void Start()
        {
            player1.DeadEvent += EndRound;
        }

        #endregion Unity Methods

        #region Helper Methods
        private void EndRound(object sender = null , EventArgs args = null)
        {
            isEndRound = true;
        }

        #endregion Helper Methods
    }
}