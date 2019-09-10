using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPD.WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void fillOnceButton_Click(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear();

            var result = await FillBucket();

            resultsListBox.Items.Add(result);
        }

        private async void fillMultipleButton_Click(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear();

            for (var i = 0; i < 10; i++)
            {
                var result = await FillBucket();

                resultsListBox.Items.Add(result);
            }
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