using System;

namespace GENERICLIST
{
    public class node<T>
    {
        public node<T>next{get;set;}
        public T data { get; set; }
        public node(T t)
        {
            next = null;
            data = t;
        }
    }
    public class genericlist<T>
    {
        private node<T> head;
        private node<T> tail;
        public genericlist()
        {
            tail = head = null;
        }
        public node<T> Head
        {
            get => head;
        }
        public void add(T t)
        {
            node<T> u = new node<T>(t);
            if(tail==null)
            {
                head = tail = u;
            }
            else
            {
                tail.next = u;
                tail = u;
            }
        }
        public void ForEach(Action<T> action)
        {
            for (node<T> u = head; u != null; u = u.next)
                action(u.data);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            genericlist<int> newlist = new genericlist<int>();
            for(int i=0;i<10;i++)
            {
                newlist.add(random.Next(100));
            }
            newlist.ForEach(u => Console.Write(u + "\t"));
            double min = double.MaxValue;
            double max = double.MinValue;
            double sum = 0;
            newlist.ForEach(n => {
                min = (n < min) ? n : min;
                max = (n > max) ? n : max;
                sum += n;
            }
            );
            Console.WriteLine();
            Console.WriteLine($"min={min},max={max},sum={sum}");
        }
    }
}
