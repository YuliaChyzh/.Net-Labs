using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab2_2
{
    public class MyQueue<T> : ICollection<T>
    {
        private Item _head;
        private Item _tail;

        private int _count;
        public int Count { get { return _count; } }

        public bool IsReadOnly { get { return false; } }

        public void Add(T item)
        {
            var temp = new Item
            {
                Value = item
            };
            if (_head == null)
            {
                _tail = _head = temp;
                _count = 1;
            }
            else
            {
                _tail.Next = temp;
                _tail = temp;
                _count++;
            }
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
            if (OnClear != null)
            {
                OnClear(this);
            }
        }

        public bool Contains(T item)
        {
            if (_head == null)
            {
                return false;
            }
            var currentItem = _head;
            do
            {
                if (Equals(currentItem.Value, item))
                {
                    return true;
                }
                currentItem = currentItem.Next;
            } while (currentItem.Next != null);
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length < _count)
            {
                throw new ArgumentOutOfRangeException();
            }

            int currentIndex = arrayIndex;
            var currentItem = _head;
            while (currentItem.Next != null)
            {
                array[currentIndex] = currentItem.Value;
                currentIndex++;
                currentItem = currentItem.Next;
            }
            array[currentIndex] = currentItem.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {

            return (IEnumerator<T>)new MyQueue<T>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Remove(T item)
        {
            if (_head == null)
            {
                throw new IndexOutOfRangeException();
            }
            Item previousItem = null;
            var currentItem = _head;
            do
            {
                if (Equals(currentItem.Value, item))
                {
                    if (previousItem == null)
                    {
                        _head = currentItem.Next;
                    }
                    else
                    {
                        previousItem.Next = currentItem.Next;
                    }
                    return true;
                }
                currentItem = currentItem.Next;
            } while (currentItem.Next != null);
            return false;
        }

        public void Enqueue(T item)
        {
            Add(item);
            if (OnEnqueue != null)
            {
                OnEnqueue(this, item);
            }
        }

        public T Dequeue()
        {
            var result = _head.Value;
            Remove(result);
            if (OnDequeue != null)
            {
                OnDequeue(this, result);
            }

            if (OnEndQueue != null && _head == null)
            {
                OnEndQueue(this);
            }
            return result;
        }

        public T Peek()
        {
            if (_head == null)
            {
                throw new IndexOutOfRangeException();
            }
            return _head.Value;
        }

        public Action<object> OnClear;
        public Action<object, T> OnEnqueue;
        public Action<object, T> OnDequeue;
        public Action<object> OnEndQueue;

        private class Item
        {
            public Item Next { get; set; }

            public T Value { get; set; }

        }
    }
}
