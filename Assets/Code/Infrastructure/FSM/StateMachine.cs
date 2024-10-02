using System;
using System.Collections.Generic;

namespace Code.Infrastructure.FSM
{
    public class StateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new();
        private IExitableState _activeState;

        public void RegisterState<TState>(TState state) where TState : IExitableState
        {
            _states.Add(typeof(TState), state);
        }

        public void UnregisterState<TState>() where TState : IExitableState
        {
            _states.Remove(typeof(TState));
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            var state = GetState<TState>();
            _activeState?.Exit();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}