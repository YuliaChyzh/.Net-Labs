using System;
using MyQueue;

namespace Lab2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new MyQueue<string>();
            q.OnEnqueue += (sender, arg) =>
            {
                Console.WriteLine("Added " + arg.ToString());
            };
            q.OnDequeue += (sender, arg) =>
            {
                Console.WriteLine("Deleted " + arg.ToString());
            };
            q.OnClear += (sender) =>
            {
                Console.WriteLine("Queue is cleared");
            };
            q.OnEndQueue += (sender) =>
            {
                Console.WriteLine("Queue is empty");
            };

            q.Enqueue("last");
            q.Enqueue("beforeLast");
            q.Enqueue("first");
            var a = q.Dequeue();
            a = q.Dequeue();
            a = q.Dequeue();
            q.Enqueue("last");
            q.Enqueue("beforeLast");
            q.Clear();
            Console.ReadKey();
        }
    }
 }
