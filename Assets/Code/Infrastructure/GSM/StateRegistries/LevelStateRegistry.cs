﻿using System;
using Code.Infrastructure.FSM;
using Code.Infrastructure.GSM.States;
using Zenject;

namespace Code.Infrastructure.GSM.StateRegistries
{
    public class LevelStateRegistry : IInitializable, IDisposable
    {
        private readonly StateFactory _stateFactory;
        private readonly GameStateMachine _gameStateMachine;

        public LevelStateRegistry(GameStateMachine gameStateMachine, StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.RegisterState(_stateFactory.Create<ConstructLevelState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameplayState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<RestartLevelState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameOverState>());
        }

        public void Dispose()
        {
            _gameStateMachine.UnregisterState<ConstructLevelState>();
            _gameStateMachine.UnregisterState<GameplayState>();
            _gameStateMachine.UnregisterState<RestartLevelState>();
            _gameStateMachine.UnregisterState<GameOverState>();
        }
    }
}