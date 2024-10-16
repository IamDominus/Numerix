using System;

namespace Code.Infrastructure
{
    public class Observable<T>
    {
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }

        public event Action<T> ValueChanged;

        private T _value;

        public Observable()
        {
            _value = default;
        }

        public Observable(T value)
        {
            _value = value;
        }
    }
}