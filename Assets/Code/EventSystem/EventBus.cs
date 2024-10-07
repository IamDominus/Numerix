using System;
using System.Collections.Generic;

namespace Code.EventSystem
{
    public class EventBus : IEventBus
    {
        private Dictionary<Type, List<Delegate>> _eventHandlers;

        public EventBus()
        {
            _eventHandlers = new Dictionary<Type, List<Delegate>>();
        }

        public void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (!_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType] = new List<Delegate>();
            }
            _eventHandlers[eventType].Add(callback);
        }

        public void Subscribe<TEvent>(Action callback) where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (!_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType] = new List<Delegate>();
            }
            _eventHandlers[eventType].Add(callback);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType].Remove(callback);
            }
        }

        public void Unsubscribe<TEvent>(Action callback) where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType].Remove(callback);
            }
        }

        public void Invoke<TEvent>(TEvent eventPayload) where TEvent : class
        {
            var eventType = eventPayload.GetType();
            if (_eventHandlers.ContainsKey(eventType))
            {
                var handlersCopy = new List<Delegate>(_eventHandlers[eventType]);

                foreach (var handler in handlersCopy)
                {
                    ((Action<TEvent>)handler)(eventPayload);
                }
            }
        }

        public void Invoke<TEvent>() where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (_eventHandlers.ContainsKey(eventType))
            {
                var handlersCopy = new List<Delegate>(_eventHandlers[eventType]);

                foreach (var handler in handlersCopy)
                {
                    ((Action)handler)();
                }
            }
        }

    }
}
