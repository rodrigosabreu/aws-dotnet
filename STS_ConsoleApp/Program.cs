using System;

namespace STS_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            var bucket = new CreateBucket();
            Console.WriteLine("Criando Bucket");
            bucket.CreateBucketAsyncRefactor("teste-202301251555").Wait();

            Console.WriteLine("\n\nListando Buckets");
            bucket.ListBucketAsyncRefactor().Wait();

            Console.WriteLine("\n\nExcluindo Buckets");
            bucket.DeleteBucketAsyncRefactor("teste-202301251555").Wait();            

            Console.WriteLine("\n\nListando Buckets");
            bucket.ListBucketAsyncRefactor().Wait();
        }
    }
}
