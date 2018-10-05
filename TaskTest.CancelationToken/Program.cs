using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest.CancelationToken
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //SimpleCancellationToken();
            SimpleCancellationTokenTimeElapse();

            Console.ReadKey();
        }

        private static void SimpleCancellationTokenTimeElapse()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Task myTask = new Task(() =>
            {
                token.ThrowIfCancellationRequested();

                for (int i = 0; i < 1000; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Stop requested");
                        token.ThrowIfCancellationRequested();
                    }
                    // Body of for loop.
                    Thread.Sleep(100);
                    Console.WriteLine(i);
                }
            });

            try
            {
                myTask.Start();
                Thread.Sleep(2200);
                cts.CancelAfter(1000);
                myTask.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Console.WriteLine(e.Message + " " + v.Message);
            }
            finally
            {
                cts.Dispose();
            }
        }

        private static void SimpleCancellationToken()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Task myTask = new Task(() =>
            {
                token.ThrowIfCancellationRequested();

                for (int i = 0; i < 1000; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Stop requested");
                        token.ThrowIfCancellationRequested();
                    }
                    // Body of for loop.
                    Thread.Sleep(100);
                    Console.WriteLine(i);
                }
            });

            try
            {
                myTask.Start();
                Thread.Sleep(2200);
                cts.Cancel();
                myTask.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Console.WriteLine(e.Message + " " + v.Message);
            }
            finally
            {
                cts.Dispose();
            }
        }
    }
}
