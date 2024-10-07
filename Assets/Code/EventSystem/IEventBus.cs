using System;

namespace Code.EventSystem
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : class;
        void Subscribe<TEvent>(Action callback) where TEvent : class;
        void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : class;
        void Unsubscribe<TEvent>(Action callback) where TEvent : class;
        void Invoke<TEvent>(TEvent eventPayload) where TEvent : class;
        void Invoke<TEvent>() where TEvent : class;
    }
}