using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PPD.ConsoleApp.NetCore
{
    public static class ChannelDemo
    {
        public static void Run()
        {
            var channel = new ChannelsQueueMultiReader(3);

            Observable.Interval(TimeSpan.FromMilliseconds(200))
                .Timestamp()
                .Subscribe(message =>
                {
                    Console.WriteLine($"value {message.Value} observed at {message.Timestamp:mm:ss.fff}");
                    channel.EnqueueAsync(message).GetAwaiter().GetResult();
                });

            Console.ReadLine();
        }
    }

    public class ChannelsQueueMultiReader
    {
        private readonly ChannelWriter<Timestamped<long>> _writer;

        public ChannelsQueueMultiReader(int numberOfReaders)
        {
            var channel = Channel.CreateBounded<Timestamped<long>>(10);

            var reader = channel.Reader;

            _writer = channel.Writer;

            for (var i = 0; i < numberOfReaders; i++)
            {
                var readerId = i;
                Task.Run(
                    async () =>
                    {
                        // Wait while channel is not empty and still not completed
                        while (await reader.WaitToReadAsync())
                        {
                            var message = await reader.ReadAsync();

                            // use Thread.Sleep to mimic CPU-bound operation
                            Thread.Sleep(TimeSpan.FromSeconds(2));

                            Console.WriteLine($"value {message.Value} received at {message.Timestamp:mm:ss.fff}, handled at {DateTime.Now:mm:ss.fff} on by reader #{readerId}");
                        }
                    }
                );
            }
        }

        public async Task EnqueueAsync(Timestamped<long> message)
        {
            await _writer.WriteAsync(message);
        }

        public void Stop()
        {
            _writer.Complete();
        }
    }
}