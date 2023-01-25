using STS_ConsoleApp;
using System;
using System.Windows.Forms;

namespace CrudBucket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Listar();
            label2.Text = "";
        }       

        private async void button1_Click(object sender, EventArgs e)
        {
            string bucketName = textBox1.Text;

            var bucket = new CreateBucket();            
            await bucket.CreateBucketAsync(bucketName);
            label2.Text = "Bucket criado com sucesso!";
            Listar();

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private async void Listar()
        {
            listBox1.Items.Clear();
            var bucket = new CreateBucket();
            var list = await bucket.ListBucketAsyncRefactor();

            foreach (var item in list)
            {
                listBox1.Items.Add(item);
            }
            
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string bucketName = listBox1.SelectedItem.ToString();

            var bucket = new CreateBucket();
            await bucket.DeleteBucketAsync(bucketName);
            label2.Text = "Bucket deletado com sucesso!";
            Listar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Listar();
        }
    }
}
