using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{
    class Program
    {
        static void Main(string[] args)
        {


            //var task1 = new Task(() => Example("1"));
            //var task2 = new Task(() => Example("2"));
            //var task3 = new Task(() => Example("3"));
            //var task4 = new Task(() => Example("4"));
            //var task5 = new Task(() => Example("5"));

            ParallelOptions op = new ParallelOptions
            {
                MaxDegreeOfParallelism = 8
            };

            var lista = new List<string>();
            for (int i = 0; i < 100000; i++)
            {
                lista.Add((i + 1).ToString());
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            //Parallel.ForEach(source: lista, parallelOptions: op, body: item => {
            //    Example(item);
            //});
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);
            //task1.Start();
            //task2.Start();
            //task1.Wait();
            //task3.Start();
            //task4.Start();
            //task5.Start();

            var stopWatch1 = new Stopwatch();
            stopWatch1.Start();
            Task<int> task1 = Task.Factory.StartNew(() => GetLov1(1));
            Task<int> task2 = Task.Factory.StartNew(() => GetLov2(2));
            Task<int> task3 = Task.Factory.StartNew(() => GetLov3(3));

            Task.WaitAll(task1, task2, task3);
            stopWatch1.Stop();
            Console.WriteLine(stopWatch1.Elapsed);


            Console.WriteLine("Iniciando uma Thread com delay de 5 s");
            //Threads
            Thread th = new Thread(() =>
            {
                Console.WriteLine("inside thread");
                Thread.Sleep(5000);
            });
            Console.WriteLine("Iniciando uma Task com delay de 5 s");
            //Tasks
            Task t = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("inside task");
                Thread.Sleep(5000);
            });

            Console.WriteLine(th.ThreadState);
            th.Start();
            Console.WriteLine(th.ThreadState);
            th.Join();
            Console.WriteLine(th.ThreadState);

            Console.ReadKey();

            Console.ReadKey();
        }

        private static int GetLov3(int v)
        {
            Console.WriteLine($"Starts {v}");
            Thread.Sleep(100);
            Console.WriteLine($"Compute: {v}, current thread: {Thread.CurrentThread.ManagedThreadId}, {Thread.CurrentThread.ThreadState} - {DateTime.Now}");         
            return v * 1;
        }

        private static int GetLov2(int v)
        {
            Console.WriteLine($"Starts {v}");
            Thread.Sleep(2000);
            Console.WriteLine($"Compute: {v}, current thread: {Thread.CurrentThread.ManagedThreadId}, {Thread.CurrentThread.ThreadState} - {DateTime.Now}");          
            return v * 10;
        }

        private static int GetLov1(int v)
        {
            Console.WriteLine($"Starts {v}");
            Thread.Sleep(5000);
            Console.WriteLine($"Compute: {v}, current thread: {Thread.CurrentThread.ManagedThreadId}, {Thread.CurrentThread.ThreadState} - {DateTime.Now}");          
            return v * 100;
        }

        static void Example(string x)
        {
            // This method runs asynchronously.         
            Console.WriteLine($"Compute: {x}, current thread: {Thread.CurrentThread.ManagedThreadId}, {Thread.CurrentThread.ThreadState}");
            Thread.Sleep(500);
        }
    }
}
