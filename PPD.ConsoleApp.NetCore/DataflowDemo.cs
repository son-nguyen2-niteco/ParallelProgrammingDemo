using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PPD.ConsoleApp.NetCore
{
    public static class DataflowDemo
    {
        public static async Task Run()
        {
            var urls = new[]
            {
                "https://github.com",
                "https://niteco.com",
                "https://www.duckduckgo.com",
                "https://www.google.com",
                "https://www.microsoft.com",
                "http://vnexpress.net",
            };

            var divCounter = DivCounterBlock();

            SetupConsumer(divCounter);

            foreach (var url in urls)
            {
                await divCounter.SendAsync(url);
            }

            divCounter.Complete();

            await divCounter.Completion;
        }

        private static IPropagatorBlock<string, ParseResponse> DivCounterBlock()
        {
            var httpClient = new HttpClient();

            var downloadBlock = new TransformBlock<string, (string, string)>(
                async url => (url, await httpClient.GetStringAsync(url)),
                new ExecutionDataflowBlockOptions {BoundedCapacity = 10, MaxDegreeOfParallelism = 5, EnsureOrdered = false}
            );

            var parserBlock = new TransformBlock<(string, string), ParseResponse>(
                tuple => CountDiv(tuple.Item1, tuple.Item2),
                new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = 5, EnsureOrdered = false}
            );

            downloadBlock.LinkTo(parserBlock, new DataflowLinkOptions {PropagateCompletion = true});

            return DataflowBlock.Encapsulate(downloadBlock, parserBlock);
        }

        private static ParseResponse CountDiv(string url, string document)
        {
            var indexStart = 0;
            var divCount = 0;
            int foundAt;

            while ((foundAt = document.IndexOf("<div>", indexStart, StringComparison.InvariantCultureIgnoreCase)) != -1)
            {
                divCount++;
                indexStart = foundAt + 1;
            }

            return new ParseResponse
            {
                Url = url,
                DocumentLength = document.Length,
                DivCount = divCount
            };
        }

        private static void SetupConsumer(IPropagatorBlock<string, ParseResponse> divCounter)
        {
            Task.Run(async () =>
            {
                while (await divCounter.OutputAvailableAsync())
                {
                    var parseResponse = await divCounter.ReceiveAsync();

                    Console.WriteLine(parseResponse);
                }
            });
        }
    }

    public class ParseResponse
    {
        public string Url { get; set; }
        public int DocumentLength { get; set; }
        public int DivCount { get; set; }

        public override string ToString()
        {
            return $"URL: {Url}, Length: {DocumentLength}, div count: {DivCount}";
        }
    }
}