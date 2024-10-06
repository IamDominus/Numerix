using System;
using System.Collections;
using System.Collections.Generic;

namespace Code.Utils
{
    // Snatched from here and slightly modified
    // https://github.com/Nims72/Enumerable-Drop-Out-Stack/tree/master
    public class DropOutStack<T> : IEnumerable<T>
    {
        private readonly T[] _items;
        private int _top;
        private int _count;

        public DropOutStack(int capacity)
        {
            _items = new T[capacity];
        }

        public void Push(T item)
        {
            _count = _count + 1 > _items.Length ? _items.Length : _count + 1;

            _items[_top] = item;
            _top = (_top + 1) % _items.Length;
        }

        public T Pop()
        {
            _count = _count - 1 < 0 ? 0 : _count - 1;

            _top = (_items.Length + _top - 1) % _items.Length;
            return _items[_top];
        }

        public bool TryPop(out T result)
        {
            if (_count == 0)
            {
                result = default;
                return false;
            }

            result = Pop();
            return true;
        }

        public T Peek()
        {
            return _items[(_items.Length + _top - 1) % _items.Length];
        }

        public int Count()
        {
            return _count;
        }

        public T GetItem(int index)
        {
            if (index > Count())
            {
                throw new InvalidOperationException("Index out of bounds");
            }

            return _items[(_items.Length + _top - (index + 1)) % _items.Length];
        }

        public void Clear()
        {
            _count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count(); i++)
            {
                yield return GetItem(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}