using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.TDD.Collections.Lib
{
    /// <summary>
    /// Realization of Single-Linked List
    /// </summary>
    public class MyList<T> : IList<T>, IEnumerator<T>
    {
        // realization of node
        private class ListItem
        {
            public T Value { get; set; }
            public ListItem Next { get; set; }
            public static ListItem operator ++(ListItem item) => item.Next;
        }

        private ListItem Head { get; set; }
        private ListItem Tail { get; set; }

        //indexer
        public T this[int index]
        {
            get => GetItem(index).Value;
            set => GetItem(index).Value = value;
        }

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public MyList()
        {
            Count = 0;
            Head = Tail = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="arr">Init list with array</param>
        public MyList(IEnumerable<T> arr)
        {
            using (IEnumerator<T> e = arr.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    // add first element
                    Count = 1;
                    Head = Tail = new ListItem { Value = e.Current };
                    //add other elements
                    while (e.MoveNext())
                    {
                        Tail = Tail.Next = new ListItem { Value = e.Current };
                        ++Count;
                    }
                }
                //if array is empty
                else
                {
                    Count = 0;
                    Head = Tail = null;
                    return;
                }

            }
        }
        #endregion

        #region Methods and Properties
        /// <summary>
        /// Number of elements in the list
        /// </summary>
        public int Count { get; private set; }
        public bool IsReadOnly { get => false; }

        private ListItem GetItem(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Index Out Of Range");
            }
            ListItem item = Head;
            while (index-- > 0)
            {
                item = item.Next;
            }
            return item;
        }

        /// <summary>
        /// Add new element to the end of the list
        /// </summary>
        /// <param name="value">Add value to the list</param>
        public void Add(T value)
        {
            if (Head == null && Tail == null)
            {
                Head = Tail = new ListItem { Value = value };
                Count = 1;
                return;
            }
            Tail = Tail.Next = new ListItem { Value = value };
            ++Count;
        }

        /// <summary>
        /// Add new element to the begining of the list
        /// </summary>
        /// <param name="value">Value to add to the list</param>
        public void AddHead(T value)
        {
            if (Head == null && Tail == null)
            {
                Head = Tail = new ListItem { Value = value };
                Count = 1;
                return;
            }
            Head = new ListItem { Value = value, Next = Head };
            ++Count;
        }

        /// <summary>
        /// Clear the list
        /// </summary>
        public void Clear()
        {
            Tail = Head = null;
            Count = 0;
        }

        /// <summary>
        /// Determines if the element is exist in the list
        /// </summary>
        /// <param name="value">Element which you want to find</param>
        /// <returns>Returns true if element is exist in the list</returns>
        public bool Contains(T value)
        {
            using (IEnumerator<T> e = GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (e.Current.Equals(value)) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Copy the list into an array
        /// </summary>
        /// <param name="array">Array to copy the list</param>
        /// <param name="arrayIndex">Start position of copying</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            using (IEnumerator<T> e = GetEnumerator())
            {
                while (e.MoveNext())
                {
                    array[arrayIndex++] = e.Current;
                }
            }
        }

        /// <summary>
        /// Index of specified element of the array
        /// </summary>
        /// <param name="value">Element you want to find</param>
        /// <returns>Position of element in the list (-1 if element doesn't exist)</returns>
        public int IndexOf(T value)
        {
            using (IEnumerator<T> e = GetEnumerator())
            {
                for (int i = 0; e.MoveNext(); ++i)
                {
                    if (e.Current.Equals(value))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Insert the element into the list
        /// </summary>
        /// <param name="index">Index to past the element</param>
        /// <param name="value">Element you want to past</param>
        public void Insert(int index, T value)
        {
            // check the index on the range of allowed values
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            // if index = 0, add value to the head 
            if (index == 0) AddHead(value);
            else
            {
                //get the link on the previous element
                ListItem prevItem = GetItem(index - 1);
                prevItem.Next = new ListItem { Value = value, Next = prevItem.Next };
                ++Count;
            }
        }

        /// <summary>
        /// Remove specific element from the list
        /// </summary>
        /// <param name="value">Element you want to remove</param>
        /// <returns>True if element was removed</returns>
        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if (index < 0) return false;
            RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Remove element from specific position in the list
        /// </summary>
        /// <param name="index">Position of the element</param>
        public void RemoveAt(int index)
        {
            // check the index on the range of allowed values
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            // if index = 0, remove value from the head 
            if (index == 0)
            {
                Head = Head.Next;
                --Count;
            }
            else
            {
                //get the link on the previous element
                ListItem prevItem = GetItem(index - 1);

                if (prevItem.Next == Tail)
                {
                    prevItem.Next = null;
                    Tail = prevItem;
                }
                else
                {
                    prevItem.Next = prevItem.Next.Next;
                }
                --Count;
            }
        }
        #endregion

        #region IEnumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }
        
        ListItem currentItem = null; //init

        /// <summary>
        /// Current element of Enumerator
        /// </summary>
        public T Current
        {
            get => currentItem.Value;
        }
        object IEnumerator.Current
        {
            get => Current;
        }

        /// <summary>
        /// Move to the next element
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (currentItem == null)
            {
                currentItem = Head;
            }
            else
            {
                ++currentItem;
            }
            return currentItem != null;
        }

        /// <summary>
        /// Reset index of Enumerator
        /// </summary>
        public void Reset()
        {
            currentItem = null;
        }

        /// <summary>
        /// Dispose: clean the memory after using Enumerator
        /// </summary>
        void IDisposable.Dispose()
        {
            Reset();
        }
        #endregion
    }
}
