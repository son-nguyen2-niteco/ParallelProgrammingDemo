using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PPD.WebApp.NetFramework.Controllers
{
    public class ConcurrencyController : Controller
    {
        public async Task<string> Index()
        {
            return await FillBucket();
        }

        [HttpGet]
        [ActionName("repeat")]
        public async Task<string> Repeat()
        {
            var fillingResults = new List<string>();

            for (var i = 0; i < 10; i++)
            {
                fillingResults.Add(await FillBucket());
            }

            return string.Join(", ", fillingResults);
        }

        private static async Task<string> FillBucket()
        {
            var tasks = new List<Task>();
            var bucket = new List<int>();

            for (var i = 0; i < 10_000; i++)
            {
                tasks.Add(AddItemAsync(bucket, i));
            }

            await Task.WhenAll(tasks);

            return $"bucket has {bucket.Count} items";
        }

        private static async Task AddItemAsync(List<int> bucket, int value)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            bucket.Add(value);
        }
    }
}