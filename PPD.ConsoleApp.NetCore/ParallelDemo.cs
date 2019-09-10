using System;
using System.Threading.Tasks;

namespace PPD.ConsoleApp.NetCore
{
    public static class ParallelDemo
    {
        public static void Run()
        {
            var mutex = new object();
            var total = 0L;

            Parallel.For(
                0,
                1_000_000_000,
                parallelOptions: new ParallelOptions {MaxDegreeOfParallelism = 4},
                localInit: () => 0L,
                body: (item, state, subTotal) => (long) item * 2 + subTotal,
                localFinally: subTotal =>
                {
                    lock (mutex)
                    {
                        total += subTotal;
                    }
                }
            );

            Console.WriteLine(total);
        }
    }
}