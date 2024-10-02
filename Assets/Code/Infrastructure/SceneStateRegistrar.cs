﻿using System;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM;
using Zenject;

namespace Code.Infrastructure
{
    public class SceneStateRegistrar : IInitializable, IDisposable
    {
        private readonly StateFactory _stateFactory;
        private readonly GameStateMachine _gameStateMachine;

        public SceneStateRegistrar(GameStateMachine gameStateMachine, StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.RegisterState(_stateFactory.Create<ConstructLevelState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameplayState>());
        }

        public void Dispose()
        {
            _gameStateMachine.UnregisterState<ConstructLevelState>();
            _gameStateMachine.UnregisterState<GameplayState>();
        }
    }
}