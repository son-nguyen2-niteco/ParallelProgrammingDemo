using System;
using System.Linq;

namespace PPD.ConsoleApp.NetCore
{
    public static class PLINQDemo
    {
        public static void Run()
        {
            var sum = Enumerable.Range(0, 1_000_000_000)
                .AsParallel()
                .Select(x => (long) x * 2)
                .Sum();

            Console.WriteLine(sum);
        }
    }
}