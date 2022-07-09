using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Feeljoon.FightingGame
{
    public class VirtualJoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Variables
        private RectTransform lever;
        private RectTransform rectTransform;

        [SerializeField, Range(50, 150)] private float leverRange;

        private Vector2 inputDirection = Vector2.zero;
        private bool isInput = false;

        #endregion Variables

        #region Properties
        public Vector2 InputDirection => inputDirection;

        #endregion Properties

        #region Unity Methods
        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            lever = transform.GetChild(0).GetComponent<RectTransform>();
        }

        #endregion Unity Methods

        #region Helper Methods
        private void ControlJoyStickLever(PointerEventData eventData)
        {
            Vector2 inputPos = eventData.position - rectTransform.anchoredPosition;
            Vector2 inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
            lever.anchoredPosition = inputVector;
            inputDirection = inputVector / leverRange;
        }

        public Direction CalcLeverDirection()
        {
            Direction direction = Direction.Middle;

            float magnitude = inputDirection.magnitude;

            float x = inputDirection.x;
            float y = inputDirection.y;

            float standardValue = Mathf.Abs(y); // 기준이 되는 값을 y로 잡는다.

            if (magnitude < 0.2f)
            {
                return direction;
            }
            else
            {
                if (standardValue > 0.5f)
                {
                    direction = y > 0f ? Direction.Up : Direction.Down;
                }
                else
                {
                    direction = x > 0f ? Direction.Right : Direction.Left;
                }
            }

            return direction;
        }

        #endregion Helper Methods

        #region Interface
        public void OnBeginDrag(PointerEventData eventData)
        {
            ControlJoyStickLever(eventData);

            isInput = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            ControlJoyStickLever(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            lever.anchoredPosition = Vector2.zero;
            inputDirection = Vector2.zero;

            isInput = false;
        }

        #endregion Interface
    }
}