using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public abstract class State<T>
    {
        protected StateMachine<T> stateMachine;
        protected T context;
        protected Animator animator;

        public State()
        {

        }

        public void SetStateMachineAndContext(StateMachine<T> stateMachine, T context)
        {
            this.stateMachine = stateMachine;
            this.context = context;

            OnInitialized();
        }

        public virtual void OnInitialized()
        {

        }

        public virtual void OnEnter()
        {

        }

        public abstract void Update(float deltaTime);

        public virtual void OnExit()
        {

        }
    }

    public sealed class StateMachine<T>
    {
        private T context;

        private State<T> currentState;
        public State<T> CurrentState => currentState;

        private State<T> previousState;
        public State<T> PreviousState => previousState;

        private float elapsedTimeInState = 0.0f;
        public float ElapsedTimeInState => elapsedTimeInState;

        private Dictionary<System.Type, State<T>> states = new Dictionary<System.Type, State<T>>();

        public StateMachine(T context, State<T> initialState)
        {
            this.context = context;

            AddState(initialState);
            currentState = initialState;
            currentState.OnEnter();
        }

        public void AddState(State<T> state)
        {
            state.SetStateMachineAndContext(this, context);
            states[state.GetType()] = state;
        }

        public void Update(float deltaTime)
        {
            elapsedTimeInState += deltaTime;

            currentState.Update(deltaTime);
        }

        public R ChangeState<R>() where R : State<T>
        {
            var newType = typeof(R);
            if (currentState.GetType() == newType)
            {
                currentState.OnEnter();
                return currentState as R;
            }

            if (currentState != null)
            {
                currentState.OnExit();
            }

            previousState = currentState;
            currentState = states[newType];
            currentState.OnEnter();
            elapsedTimeInState = 0.0f;

            return currentState as R;
        }
    }
}