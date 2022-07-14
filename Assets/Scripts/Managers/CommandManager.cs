using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class CommandManager : Singleton<CommandManager>
    {
        #region Variables
        public List<CommandButton> inputCommandList = new List<CommandButton>();
        public Stack<Direction> directionContain = new Stack<Direction>();

        public bool isInputButton = false;

        public float calcInputTime = 0f;
        public float checkInputTime = 10f;

        public float clearContainDelay = 0.5f;

        [Header("입력 방향 인식 시간")]
        public float delayWithInputDirection;

        #endregion Variables

        #region Properties
        public bool IsEndRound => GameManager.Instance.IsEndRound;

        #endregion Properties

        #region Unity Methods
        void Start()
        {
            PushDirection(Direction.Middle);
            StartCoroutine(ClearDirectionContainWithDelay(clearContainDelay));
        }

        void Update()
        {
            Debug.Log(PeekDirection());
        }

        #endregion Unity Methods

        #region Helper Methods
        public void ClearDirection()
        {
            if (directionContain.Count < 50)
            {
                return;
            }

            Direction previouseDirection = PeekDirection();

            directionContain.Clear();
            PushDirection(previouseDirection);
        }

        public void PushDirection(Direction direction)
        {
            directionContain.Push(direction);
        }

        public Direction PopDirection()
        {
            Direction direction = directionContain.Pop();

            return direction;
        }

        public Direction PeekDirection()
        {
            return directionContain.Peek();
        }

        private IEnumerator ClearDirectionContainWithDelay(float delay)
        {
            while (IsEndRound == false)
            {
                ClearDirection();

                yield return delay;
            }
        }

        #endregion Helper Methods
    }
}