using System;

namespace Code.Infrastructure
{
    [Serializable]
    public class Observable<T> : IEquatable<Observable<T>>
    {
        private T _value;

        public Observable()
        {
        }

        public Observable(T value)
        {
            _value = value;
        }

        public event Action<T> ValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(value);
            }
        }

        public static implicit operator Observable<T>(T observable)
        {
            return new Observable<T>(observable);
        }

        public static explicit operator T(Observable<T> observable)
        {
            return observable._value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool Equals(Observable<T> other)
        {
            return other._value.Equals(_value);
        }

        public override bool Equals(object other)
        {
            return other != null
                   && other is Observable<T> observable
                   && observable._value.Equals(_value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}