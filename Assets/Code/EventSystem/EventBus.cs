using System;
using System.Collections.Generic;
using Code.EventSystem.Events;

namespace Code.EventSystem
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers;

        public EventBus()
        {
            _subscribers = new Dictionary<Type, List<Delegate>>();
        }

        public void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);

            if (_subscribers.ContainsKey(eventType) == false)
            {
                _subscribers[eventType] = new List<Delegate>();
            }

            _subscribers[eventType].Add(callback);
        }

        public void Subscribe<TEvent>(Action callback) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);

            if (_subscribers.ContainsKey(eventType) == false)
            {
                _subscribers[eventType] = new List<Delegate>();
            }

            _subscribers[eventType].Add(callback);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);

            if (_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType].Remove(callback);
            }
        }

        public void Unsubscribe<TEvent>(Action callback) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);

            if (_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType].Remove(callback);
            }
        }

        public void Invoke<TEvent>(TEvent eventPayload) where TEvent : IEvent
        {
            var eventType = eventPayload.GetType();

            if (_subscribers.TryGetValue(eventType, out var callbacks))
            {
                var callbacksCopy = new List<Delegate>(callbacks);

                foreach (var handler in callbacksCopy)
                {
                    ((Action<TEvent>)handler)(eventPayload);
                }
            }
        }

        public void Invoke<TEvent>() where TEvent : IEvent
        {
            var eventType = typeof(TEvent);

            if (_subscribers.TryGetValue(eventType, out var callbacks))
            {
                var callbacksCopy = new List<Delegate>(callbacks);

                foreach (var handler in callbacksCopy)
                {
                    ((Action)handler)();
                }
            }
        }
    }
}