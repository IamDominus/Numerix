using System;
using Code.EventSystem.Events;

namespace Code.EventSystem
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent;
        void Subscribe<TEvent>(Action callback) where TEvent : IEvent;
        void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent;
        void Unsubscribe<TEvent>(Action callback) where TEvent : IEvent;
        void Invoke<TEvent>(TEvent eventPayload) where TEvent : IEvent;
        void Invoke<TEvent>() where TEvent : IEvent;
    }
}