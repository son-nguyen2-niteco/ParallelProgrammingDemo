namespace PPD.ConsoleApp.NetCore
{
    internal static class Program
    {
        private static void Main()
        {
            PLINQDemo.Run();
            //ParallelDemo.Run();
            //DynamicParallelismDemo.Run();
            //BlockingCollectionDemo.Run();
            //DataflowDemo.Run().GetAwaiter().GetResult();
            //ConcurrencyIssuesDemo.Run().GetAwaiter().GetResult();
            //ChannelDemo.Run();
        }
    }
}