using System;
using Demo.TDD.Collections.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.TDD.Collections.Test
{
    [TestClass]
    public class MyArrayListTest
    {
        [TestMethod]
        public void MyArrayListCtorTest()
        {
            MyArrayList<int> list = new MyArrayList<int>();
            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(false, list.IsReadOnly);
            MyArrayList<int> list2 = new MyArrayList<int>(100);
            Assert.AreEqual(0, list2.Count);
            MyArrayList<int> list3 = new MyArrayList<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Assert.AreEqual(10, list3.Count);
        }
        [TestMethod]
        public void MyArrayListTestGet()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyArrayList<int> list = new MyArrayList<int>(arr);
            for (int i = 0; i < arr.Length; ++i)
            {
                Assert.AreEqual(arr[i], list[i]);
            }
        }
        [TestMethod]
        public void MyArrayListTestSet()
        {
            MyArrayList<int> list = new MyArrayList<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            list[0] = 100;
            Assert.AreEqual(list[0], 100);
            list[9] = 100;
            Assert.AreEqual(list[9], 100);
            Assert.AreEqual(list[1], 2);
            Assert.AreEqual(list[8], 9);
            Assert.AreEqual(list[4], 5);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MyArrayListGetException1()
        {
            MyArrayList<int> list = new MyArrayList<int>();
            Assert.AreEqual(list[-1], 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MyArrayListGetException2()
        {
            MyArrayList<int> list = new MyArrayList<int>();
            list[-1] = 100;
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MyArrayListGetException3()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyArrayList<int> list = new MyArrayList<int>(arr);
            list[10] = 100;
        }

        [TestMethod]
        public void MyArrayListAddTest()
        {
            MyArrayList<int> list = new MyArrayList<int>();
            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count);
            list.Add(100);
            Assert.AreEqual(100, list[0]);
            Assert.AreEqual(1, list.Count);
            for (int i = 0; i < 9999; ++i)
            {
                list.Add(i);
            }
            Assert.AreEqual(10000, list.Count);
        }

        [TestMethod]
        public void MyArrayListClearTest()
        {
            MyArrayList<int> list = new MyArrayList<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            list.Add(111);
            Assert.AreEqual(11, list.Count);
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void MyArrayListContainsTest()
        {
            MyArrayList<int> list = new MyArrayList<int>(new int[] { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Assert.IsTrue(list.Contains(0));
            Assert.IsTrue(list.Contains(5));
            Assert.IsTrue(list.Contains(10));
            Assert.IsFalse(list.Contains(-1));
            Assert.IsFalse(list.Contains(64));
            Assert.IsFalse(list.Contains(-12));
            Assert.IsFalse(list.Contains(Int32.MinValue));
            Assert.IsFalse(list.Contains(Int32.MaxValue));
        }

        [TestMethod]
        public void MyArrayListCopyToTest()
        {
            MyArrayList<int> list = new MyArrayList<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            int[] arr = new int[list.Count];
            list.CopyTo(arr, 0);
            for(int i =0;i<list.Count; i++)
            {
                Assert.AreEqual(arr[i],list[i]);
            }
        }

        [TestMethod]
        public void MyArrayListIndexOfTest()
        {
            MyArrayList<int> list = new MyArrayList<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Assert.AreEqual(0, list.IndexOf(1));
            Assert.AreEqual(9, list.IndexOf(10));
            Assert.AreEqual(4, list.IndexOf(5));
            Assert.AreEqual(-1, list.IndexOf(Int32.MinValue));
            Assert.AreEqual(-1, list.IndexOf(Int32.MaxValue));
            Assert.AreEqual(-1, list.IndexOf(111));
            Assert.AreEqual(-1, list.IndexOf(0));
            Assert.AreEqual(-1, list.IndexOf(-1));
        }

        [TestMethod]
        public void MyArrayListInsertTest()
        {
            MyArrayList<int> list = new MyArrayList<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            list.Insert(0, 23);
            Assert.AreEqual(11, list.Count);
            Assert.AreEqual(list[0], 23);
            Assert.AreEqual(list[10], 10);
            list.Insert(6, 123);
            Assert.AreEqual(list[6], 123);
            Assert.AreEqual(list[7], 6);
            list.Insert(11, 123);
            Assert.AreEqual(list[11], 123);
        }

        [TestMethod]
        public void MyArrayListRemoveTest()
        {
            MyArrayList<int> list = new MyArrayList<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            list.Remove(1);
            Assert.AreEqual(9, list.Count);
            Assert.AreEqual(2, list[0]);
            list.Remove(5);
            Assert.AreEqual(8, list.Count);
            Assert.AreEqual(6, list[3]);
            list.Remove(10);
            Assert.AreEqual(7, list.Count);
            Assert.AreEqual(9, list[6]);
        }

        [TestMethod]
        public void MyArrayListRemoveAtTest()
        {
            MyArrayList<int> list = new MyArrayList<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            list.RemoveAt(1);
            Assert.AreEqual(9, list.Count);
            Assert.AreEqual(3, list[1]);
            list.RemoveAt(5);
            Assert.AreEqual(8, list.Count);
            Assert.AreEqual(8, list[5]);
            list.Remove(10);
            Assert.AreEqual(7, list.Count);
            Assert.AreEqual(9, list[6]);
        }

        [TestMethod]
        public void MyArrayListEnumeratorTest()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyArrayList<int> list = new MyArrayList<int>(arr);
            int ind = 0;
            foreach(var l in list)
            {
                Assert.AreEqual(l, arr[ind++]);
            }
        }
    }
}
