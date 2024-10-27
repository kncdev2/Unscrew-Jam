using System;
using System.Collections.Generic;

namespace HG
{
    public class Fsm<T>
    {
        private T _owner;
        private Dictionary<System.Type, State<T>> states;
        private State<T> currentState;


        public Fsm(T ownerOfFsm)
        {
            _owner = ownerOfFsm;
            states = new Dictionary<Type, State<T>>();
        }

        public void AddState<TS>() where TS:State<T>, new()
        {
            states[typeof(TS)] = new TS().SetState(this, _owner);
        }

        public void AddState(State<T> state)
        {
            state.SetState(this, _owner);
            states[state.GetType()] = state;
        }

        public void SwitchState<TS>() where TS: State<T>
        {

            TS newState = GetState<TS>();
            if (newState != null && newState != currentState)
            {
                currentState?.Exit();
                currentState = newState;
                currentState.Enter();
            }
        }

        public TS GetState<TS>() where TS: State<T>
        {
            System.Type stateType = typeof(TS);
            if (states.ContainsKey(stateType))
            {
                return states[stateType] as TS;
            }

            return null;
        }

        public State<T> GetCurrentState()
        {
            return currentState;
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void LateUpdate()
        {
            currentState?.LateUpdate();
        }

        public void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }
    }
}


