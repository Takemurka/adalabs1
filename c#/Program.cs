using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelThreadDemo
{
    class Program
    {
        private bool canStop = false;
        private int step;

        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.StartAsync();
        }

        async Task StartAsync()
        {
            Console.Write("Enter the number of threads to create: ");
            int threadCount = int.Parse(Console.ReadLine());

            Console.Write("Enter the step value: ");
            step = int.Parse(Console.ReadLine());

            List<Task<(long, int, int)>> tasks = new List<Task<(long, int, int)>>();
            for (int i = 0; i < threadCount; i++)
            {
                int threadNum = i + 1;
                tasks.Add(Task.Run(() => Calculator(threadNum)));
            }

            Task stopTask = Task.Run(() => Stoper());

            await Task.WhenAll(tasks.ToArray());
            canStop = true;

            await stopTask;

            foreach (var result in await Task.WhenAll(tasks))
            {
                Console.WriteLine($"Thread {result.Item2}: Sum: {result.Item1}, Steps: {result.Item3}");
            }
        }

        async Task<(long, int, int)> Calculator(int threadNum)
        {
            long sum = 0;
            int steps = 0;
            do
            {
                sum += step;
                steps++;
                Thread.Sleep(10);
            } while (!CanStop);

            return (sum, threadNum, steps);
        }

        public bool CanStop { get => canStop; }

        async Task Stoper()
        {
            await Task.Delay(30000);
            canStop = true;
        }
    }
}
