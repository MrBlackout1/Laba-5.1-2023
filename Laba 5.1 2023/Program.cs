using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static Queue<int> queue = new Queue<int>();
    static object lockObject = new object();
    static Random random = new Random();

    static void Main(string[] args)
    {
        Thread producerThread = new Thread(Producer);
        Thread consumerThread = new Thread(Consumer);
        producerThread.Start();
        consumerThread.Start();
        producerThread.Join();
        consumerThread.Join();
    }

    static void Producer()
    {
        while (true)
        {
            int value = random.Next(100);
            lock (lockObject)
            {
                queue.Enqueue(value);
                Console.WriteLine("Producer enqueued: " + value);
            }
            Thread.Sleep(1000);
        }
    }

    static void Consumer()
    {
        while (true)
        {
            int value;
            lock (lockObject)
            {
                if (queue.Count > 0)
                {
                    value = queue.Dequeue();
                    Console.WriteLine("Consumer dequeued: " + value);
                }
            }
            Thread.Sleep(1000);
        }
    }
}