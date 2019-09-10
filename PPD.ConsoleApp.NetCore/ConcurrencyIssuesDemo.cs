using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPD.ConsoleApp.NetCore
{
    public static class ConcurrencyIssuesDemo
    {
        public static async Task Run()
        {
            //await Once();
            await Repeat();
        }

        private static async Task Once()
        {
            var message = await FillBucketConcurrently();

            Console.WriteLine(message);
        }

        private static async Task<string> FillBucketConcurrently()
        {
            var tasks = new List<Task>();
            var bucket = new List<int>();

            for (var i = 0; i < 10_000; i++)
            {
                var value = i;
                var task = Task.Run(async () => await AddItemAsync(bucket, value));
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            return $"bucket has {bucket.Count} items";
        }

        private static async Task AddItemAsync(List<int> bucket, int value)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            bucket.Add(value);
        }

        private static async Task Repeat()
        {
            for (var i = 0; i < 100; i++)
            {
                await Once();
            }
        }
    }
}