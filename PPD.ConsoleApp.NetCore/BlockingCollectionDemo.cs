using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PPD.ConsoleApp.NetCore
{
    public static class BlockingCollectionDemo
    {
        public static void Run()
        {
            var collection = new BlockingCollection<long>(boundedCapacity: 10);

            SetupConsumer(collection, 3);

            Observable.Interval(TimeSpan.FromMilliseconds(100))
                .Subscribe(x => collection.Add(x));

            Console.ReadLine();
        }

        private static void SetupConsumer(BlockingCollection<long> collection, int numberOfConsumers = 1)
        {
            for (var i = 0; i < numberOfConsumers; i++)
            {
                Task.Run(() =>
                {
                    foreach (var item in collection.GetConsumingEnumerable())
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        Console.WriteLine($"on thread #{Thread.CurrentThread.ManagedThreadId}, received: {item}");
                    }
                });
            }
        }
    }
}