using System;
using System.Threading;
using System.Threading.Tasks;

namespace PPD.ConsoleApp.NetCore
{
    public static class DynamicParallelismDemo
    {
        public static void Run()
        {
            var root = new Node("0")
            {
                Left = new Node("1_1")
                {
                    Left = new Node("2_1")
                    {
                        Left = new Node("3_1") {Left = new Node("4_1"), Right = new Node("4_2")},
                        Right = new Node("3_2") {Left = new Node("4_3"), Right = new Node("4_4")}
                    },
                    Right = new Node("2_2")
                    {
                        Left = new Node("3_3") {Left = new Node("4_5"), Right = new Node("4_6")},
                        Right = new Node("3_4") {Left = new Node("4_7"), Right = new Node("4_8")}
                    }
                },
                Right = new Node("1_2")
                {
                    Left = new Node("2_3")
                    {
                        Left = new Node("3_5") {Left = new Node("4_9"), Right = new Node("4_10")},
                        Right = new Node("3_6") {Left = new Node("4_11"), Right = new Node("4_12")}
                    },
                    Right = new Node("2_4")
                    {
                        Left = new Node("3_7") {Left = new Node("4_13"), Right = new Node("4_14")},
                        Right = new Node("3_8") {Left = new Node("4_15"), Right = new Node("4_16")}
                    }
                }
            };

            ProcessTree(root).Wait();
        }

        private static Task ProcessTree(Node root)
        {
            return Task.Factory.StartNew(
                () => Traverse(root),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.Default
            );
        }

        private static void Traverse(Node current)
        {
            // use Thread.Sleep to mimic expensive operation
            Thread.Sleep(TimeSpan.FromSeconds(4));
            Console.WriteLine(current.Value);

            if (current.Left != null)
            {
                Task.Factory.StartNew(
                    () => Traverse(current.Left),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Current
                );
            }

            if (current.Right != null)
            {
                Task.Factory.StartNew(
                    () => Traverse(current.Right),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Current
                );
            }
        }
    }

    public class Node
    {
        public Node(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}