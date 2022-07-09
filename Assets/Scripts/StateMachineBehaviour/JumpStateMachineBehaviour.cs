using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class JumpStateMachineBehaviour : StateMachineBehaviour
    {
        private float duration;
        private Func<float, float> f;
        private PlayerCharacterController player;

        private readonly int hashDudge = Animator.StringToHash("Dudge");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = animator.GetComponentInParent<PlayerCharacterController>();
            duration = stateInfo.length;
            f = x => player.jumpHeightAmount * -1 * (x - (CommandManager.Instance.checkInputTime + duration / 2f));

            Debug.Log(duration);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.transform.parent.position += Vector3.up * f(CommandManager.Instance.calcInputTime) * Time.deltaTime;

            if (CommandManager.Instance.calcInputTime - CommandManager.Instance.checkInputTime > duration)
            {
                CommandManager.Instance.calcInputTime = 0f;
                animator.SetTrigger(hashDudge);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}