using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.TDD.Collections.Lib
{
    /// <summary>
    /// Realization of List
    /// </summary>
    public class MyArrayList<T> : IEnumerator<T>, ICollection<T>, IList<T>
    {
        #region Fields
        private T[] arr;
        int count = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor MyArrayList
        /// </summary>
        /// <param name="capacity">Initial capacity of arrayList</param>
        public MyArrayList(int capacity = 10)
        {
            arr = new T[capacity];
        }

        /// <summary>
        /// Constructor MyArrayList
        /// </summary>
        /// <param name="newarr">Array to initialize into list</param>
        public MyArrayList(T[] newarr)
        {
            arr = new T[newarr.Length];
            for (int i =0;i<newarr.Length; i++)
            {
                arr[i] = newarr[i];
            }
            count = newarr.Length;
        }
        #endregion

        #region Properties and Indexator

        // use indexer in the code
        public T this[int index]
        {
            get => arr[index];
            set => arr[index] = value;
        }

        /// <summary>
        /// Number of list elements 
        /// </summary>
        public int Count
        {
            get => count;
        }

        public bool IsReadOnly => false;
        #endregion

        #region Methods

        /// <summary>
        /// Adds the element to the end of the list
        /// </summary>
        /// <param name="item">Element you want to add</param>
        public void Add(T item)
        {
            if (count == arr.Length)
            {
                // expand array boundaries 
                T[] tmp = new T[arr.Length + arr.Length / 2 + 1];
                for (int i = 0; i < arr.Length; ++i)
                {
                    tmp[i] = arr[i];
                }
                arr = tmp;
            }
            // add to the end
            arr[count++] = item;
        }

        /// <summary>
        /// Clears the list
        /// </summary>
        public void Clear()
        {
            count = 0;
        }

        /// <summary>
        /// Determines if the element is exist in the list
        /// </summary>
        /// <param name="item">Element which you want to find</param>
        /// <returns>Returns true if element is exist in the list</returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < count; ++i)
            {
                if (arr[i].Equals(item)) return true;
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
            for (int i = 0; i < count; ++i)
            {
                array[arrayIndex++] = arr[i];
            }
        }

        /// <summary>
        /// Index of specified element of the array
        /// </summary>
        /// <param name="item">Element you want to find</param>
        /// <returns>Position of element in the list (-1 if element doesn't exist)</returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < count; ++i)
            {
                if (arr[i].Equals(item)) return i;
            }
            return -1;
        }

        /// <summary>
        /// Insert the element into the list
        /// </summary>
        /// <param name="index">Index to past the element</param>
        /// <param name="item">Element you want to past</param>
        public void Insert(int index, T item)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            if (index < arr.Length && index >= 0)
            {
                // expand array boundaries 
                T[] tmp = new T[arr.Length + arr.Length / 2 + 1];
                for (int i = 0; i < index; ++i)
                {
                    tmp[i] = arr[i];
                }
                // add element to the index
                tmp[index] = item;
                // move index of in one position
                for (int i = index; i < arr.Length; ++i)
                {
                    tmp[i + 1] = arr[i];
                }
                arr = tmp;
                count++;
            }

        }

        /// <summary>
        /// Remove specific element from the list
        /// </summary>
        /// <param name="item">Element you want to remove</param>
        /// <returns>True if element was removed</returns>
        public bool Remove(T item)
        {
            int i = IndexOf(item);
            // if item is not in the list
            if (i < 0) return false;
            RemoveAt(i);
            return true;
        }

        /// <summary>
        /// Remove element from specific position in the list
        /// </summary>
        /// <param name="index">Position of the element</param>
        public void RemoveAt(int index)
        {
            for (++index; index < count; ++index)
            {
                arr[index - 1] = arr[index];
            }
            --count;
        }

        #endregion

        #region IEnumerator and IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Current element of Enumerator
        /// </summary>
        public T Current
        {
            get => arr[currentIndex];
        }
        object IEnumerator.Current => Current;
        private int currentIndex = -1;

        /// <summary>
        /// Move to the next element of Enumerator
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            ++currentIndex;
            return currentIndex < count;
        }

        /// <summary>
        /// Reset current index of Enumerator
        /// </summary>
        public void Reset()
        {
            currentIndex = -1;
        }

        /// <summary>
        /// Dispose: clear memory after using Enumerator
        /// </summary>
        public void Dispose()
        {
            Reset();
        }
        #endregion

    }
}
