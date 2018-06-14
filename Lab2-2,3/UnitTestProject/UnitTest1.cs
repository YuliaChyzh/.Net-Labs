using System;
using NUnit.Framework;


namespace Lab2_2.UnitTestProject1
{
    [TestFixture]
    public class MyQueueTests
    {
        [Test]
        public void Add_ItemsAddToQueue_ContainsAllElementCollection()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            foreach (var item in a)
            {
                q.Add(item);
            }
            Assert.AreEqual(q.Count, 3);
        }

        [Test]
        public void Add_INullAddToQueue_ContainsAllElementCollection()
        {
            var q = new MyQueue<string>();
            string a = null;

                q.Add(a);
            
            Assert.AreEqual(q.Contains(null), true);
        }

        [Test]
        public void ClearTest()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            foreach (var item in a)
            {
                q.Add(item);
            }
            q.Clear();
            Assert.AreEqual(q.Count, 0);
        }

        [Test]
        public void Contains_ItemIsContainInQueue_True()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            foreach (var item in a)
            {
                q.Add(item);
            }
            Assert.AreEqual(q.Contains(2), true);
        }

        [Test]
        public void Contains_ItemIsNotContainInQueue_False()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            foreach (var item in a)
            {
                q.Add(item);
            }
            Assert.AreEqual(q.Contains(5), false);
        }

        [Test]
        public void CopyTo_CopyQueueToArray_True()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3, 4, 5 };
            foreach (var item in a)
            {
                q.Add(item);
            }
            int[] b = { 0, 0, 0, 0, 0, 0, 0 };
            q.CopyTo(b, 2);

            Assert.That(b, Is.EquivalentTo(new int[] { 0, 0, 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void CopyTo_CantCopyQueueToArray_Exeption()
        {
                var q = new MyQueue<int>();
                int[] a = { 1, 2, 3, 4, 5 };
                foreach (var item in a)
                {
                    q.Add(item);
                }
                int[] b = new int[4];
                Assert.That(() => q.CopyTo(b, 2), Throws.Exception.TypeOf < ArgumentOutOfRangeException>());
        }

        [Test]
        public void Remove_DeleteItemFromCollection_SuccessfulRemove()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3, 4, 5 };
            foreach (var item in a)
            {
                q.Add(item);
            }

            q.Remove(2);

            Assert.AreEqual(q.Contains(2), false);
        }

        [Test]
        public void Remove_DeleteItemFromCollection_True()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3, 4, 5 };
            foreach (var item in a)
            {
                q.Add(item);
            }
            Assert.AreEqual(q.Remove(2), true);
        }

        [Test]
        public void Remove_CantDeleteUncontainedItemFromCollection_False()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3, 4, 5 };
            foreach (var item in a)
            {
                q.Add(item);
            }
            Assert.AreEqual(q.Remove(5), false);
        }

        [Test]
        public void Remove_CantDeleteItemFromCollection_Exeption()
        {
            var q = new MyQueue<int>();
            Assert.That(() => q.Remove(2), Throws.Exception.TypeOf<IndexOutOfRangeException>());
        }


        [Test]
        public void Enqueue_AddItemInCollection_SuccessfulAdd()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            foreach (var item in a)
            {
                q.Enqueue(item);
            }
            Assert.AreEqual(q.Count, 3);
        }

        [Test]
        public void Dequeue_DeleteFirstItemFromCollection_DeletedItem()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            foreach (var item in a)
            {
                q.Enqueue(item);
            }
            Assert.AreEqual(q.Dequeue(), 1);
        }

        [Test]
        public void Peek_FindFirstItemFromCollection_FirstItemFromCollection()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            foreach (var item in a)
            {
                q.Enqueue(item);
            }
            Assert.AreEqual(q.Peek(), 1);
        }

        [Test]
        public void Peek_CantFindFirstItemFromEmptyCollection_Exeption()
        {
            var q = new MyQueue<int>();
            Assert.That(() => q.Peek(), Throws.Exception.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Count_CalcCountOfElementsInCollection_CountOfElementsInCollection()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            foreach (var item in a)
            {
                q.Enqueue(item);
            }
            Assert.AreEqual(q.Count, 3);
        }

        [Test]
        public void Enqueue_EventHappen_EventOnEnqueue()
        {
            var q = new MyQueue<int>();
            int item = 1;
            bool flag = true;
        
            q.OnEnqueue += (sender, arg) =>
            {
                flag = false;
            };
            q.Enqueue(item);
            Assert.AreEqual(flag, false);
        }

        [Test]
        public void Dequeue_EventHappen_EventOnDequeue()
        {
            
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            bool flag = true;
            foreach (var item in a)
            {
                q.Enqueue(item);
            }
            q.OnDequeue += (sender, arg) =>
            {
                flag = false;
            };
            q.Dequeue();
            Assert.AreEqual(flag, false);
        }

        [Test]
        public void Clear_EventHappen_EventOnClear()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            bool flag = true;
            q.OnClear += (sender) =>
            {
                flag = false;
            };
            foreach (var item in a)
            {
                q.Add(item);
            }
            q.Clear();
            Assert.AreEqual(flag, false);
        }

        [Test]
        public void Dequeue_EventHappen_EventOnEndQueue()
        {
            var q = new MyQueue<int>();
            int[] a = { 1, 2, 3 };
            foreach (var item in a)
            {
                q.Enqueue(item);
            }
            bool flag = true;
            q.OnEndQueue += (sender) =>
            {
                flag = false;
            };
            for(int i=0;i<a.Length;i++)
            {
                q.Dequeue();
            }
            Assert.AreEqual(flag, false);
        }

    }
}