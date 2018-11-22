using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.TDD.Collections.Lib
{
    /// <summary>
    /// Realization of Double-Linked List
    /// </summary>
    public class DList<T> : IList<T>
    {
        // realization of node (with links to prev and next  elements)
        private class ListItem
        {
            public T Value { get; set; }
            public ListItem Next { get; set; }
            public ListItem Prev { get; set; }
            public static ListItem operator ++(ListItem item) => item.Next;
        }

        private ListItem Head { get; set; }
        private ListItem Tail { get; set; }

        #region Constructors

        /// <summary>
        /// Consructor
        /// </summary>
        public DList()
        {
            Count = 0;
            Head = Tail = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="arr">Init list with array</param>
        public DList(IEnumerable<T> arr)
        {
            using (IEnumerator<T> e = arr.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    // add first element
                    Count = 1;
                    Head = Tail = new ListItem { Value = e.Current };
                    // add other elements
                    while (e.MoveNext())
                    {
                        Tail = Tail.Next = new ListItem { Value = e.Current, Prev = Tail };
                        ++Count;
                    }
                }
                // if array is empty
                else
                {
                    Count = 0;
                    Head = Tail = null;
                    return;
                }
            }
        }
        #endregion

        // our indexer
        public T this[int index]
        {
            get => GetItem(index).Value;
            set => GetItem(index).Value = value;
        }

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
            Tail = Tail.Next = new ListItem { Value = value, Prev = Tail };
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
            if (index == 0)
            {
                AddHead(value);
                return;
            }
            // get item from the position we want to insert another
            ListItem item = GetItem(index);

            item.Prev = item.Prev.Next = new ListItem
            {
                Value = value,
                Next = item,
                Prev = item.Prev
            };
            ++Count;
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

            ListItem item = GetItem(index);
            // rewrite links of prev and next items (depends on position)
            if (item == Head)
                Head = item.Next;
            else
                item.Prev.Next = item.Next;

            if (item == Tail)
                Tail = item.Prev;
            else
                item.Next.Prev = item.Prev;

            --Count;
        }
        #endregion

        #region Enumerator
        //enumerator for nodes
        private IEnumerator<ListItem> GetListItemEnumerator()
        {
            for (ListItem item = Head; item != null; ++item)
            {
                yield return item;
            }
        }
        // enumerator for values of nodes
        public IEnumerator<T> GetEnumerator()
        {
            using (IEnumerator<ListItem> e = GetListItemEnumerator())
            {
                while (e.MoveNext())
                {
                    yield return e.Current.Value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
