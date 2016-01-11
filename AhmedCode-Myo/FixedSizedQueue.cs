using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhmedCode_Myo
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        readonly int size;
        readonly object locker;

        int count;
        int head;
        int rear;
        T[] values;

        public CircularBuffer(int max)
        {
            this.size = max;
            locker = new object();
            count = 0;
            head = 0;
            rear = 0;
            values = new T[size];
        }

        static int Incr(int index, int size)
        {
            return (index + 1) % size;
        }

        private void UnsafeEnsureQueueNotEmpty()
        {
            if (count == 0)
                throw new Exception("Empty queue");
        }

        public int Size { get { return size; } }
        public object SyncRoot { get { return locker; } }

        #region Count

        public int Count { get { return UnsafeCount; } }
        public int SafeCount { get { lock (locker) { return UnsafeCount; } } }
        public int UnsafeCount { get { return count; } }

        #endregion

        #region Enqueue

        public void Enqueue(T obj)
        {
            UnsafeEnqueue(obj);
        }

        public void SafeEnqueue(T obj)
        {
            lock (locker) { UnsafeEnqueue(obj); }
        }

        public void UnsafeEnqueue(T obj)
        {
            values[rear] = obj;

            if (Count == Size)
                head = Incr(head, Size);
            rear = Incr(rear, Size);
            count = Math.Min(count + 1, Size);
        }

        #endregion

        #region Dequeue

        public T Dequeue()
        {
            return UnsafeDequeue();
        }

        public T SafeDequeue()
        {
            lock (locker) { return UnsafeDequeue(); }
        }

        public T UnsafeDequeue()
        {
            UnsafeEnsureQueueNotEmpty();

            T res = values[head];
            values[head] = default(T);
            head = Incr(head, Size);
            count--;

            return res;
        }

        #endregion

        #region Peek

        public T Peek()
        {
            return UnsafePeek();
        }

        public T SafePeek()
        {
            lock (locker) { return UnsafePeek(); }
        }

        public T UnsafePeek()
        {
            UnsafeEnsureQueueNotEmpty();

            return values[head];
        }

        #endregion


        #region GetEnumerator

        public IEnumerator<T> GetEnumerator()
        {
            return UnsafeGetEnumerator();
        }

        public IEnumerator<T> SafeGetEnumerator()
        {
            lock (locker)
            {
                List<T> res = new List<T>(count);
                var enumerator = UnsafeGetEnumerator();
                while (enumerator.MoveNext())
                    res.Add(enumerator.Current);
                return res.GetEnumerator();
            }
        }

        public IEnumerator<T> UnsafeGetEnumerator()
        {
            int index = head;
            for (int i = 0; i < count; i++)
            {
                yield return values[index];
                index = Incr(index, size);
            }
        }

        public T[] ToArray3()
        {
            T[] arr = new T[count];
            int index = head;
            for (int i = 0; i < count; i++)
            {
                arr[i] = values[index];
                index = Incr(index, size);
            }
            return arr;
        }
        public T[] ToArray2()
        {
            //T[] arr = new T[count];
            return values.Skip(head).Concat(values.Take(head)).ToArray();
            
            //return arr;
        }
        

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
