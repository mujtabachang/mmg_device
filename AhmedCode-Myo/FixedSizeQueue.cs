using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhmedCode_Myo
{
    public class FixedSizedQueue<T>
    {
        ConcurrentQueue<T> q = new ConcurrentQueue<T>();

        public int Limit { get; set; }
        public void Enqueue(T obj)
        {
            q.Enqueue(obj);
            lock (this)
            {
                T overflow;
                while (q.Count > Limit && q.TryDequeue(out overflow)) ;
            }
        }
        public void print()
        {
            string a = "";
            q.ToString();
            for (int i = 0; i < q.Count; i++)
            {
                a += " " + q.ElementAt(i).ToString();
            }
            Debug.WriteLine(a);
        }
        public T[] ToArray()
        {
            T[] arr = new T[q.Count];
            for (int i = 0; i < q.Count; i++)
            {
                arr[i] = q.ElementAt(i);
                //a += " " + q.ElementAt(i).ToString();
            }
            return arr;
        }
    }
}
