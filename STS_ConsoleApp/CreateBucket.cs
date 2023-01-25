using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace STS_ConsoleApp
{
    public class CreateBucket
    {
        private static string bucketName;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.SAEast1;
        private static IAmazonS3 s3Client;

        public CreateBucket()
        {
            s3Client = new AmazonS3Client(Constants.UID, Constants.Secret, bucketRegion);
        }


        public async Task CreateBucketAsyncRefactor(string _bucketName)
        {           
            AmazonS3Client client = new AmazonS3Client(Constants.UID, Constants.Secret, bucketRegion);
                
            PutBucketRequest request = new PutBucketRequest()
            {
                BucketName = _bucketName,
                UseClientRegion = true
            };
            PutBucketResponse response = await client.PutBucketAsync(request);           
        }

        public async Task CreateBucketAsync(string _bucketName)
        {
            try
            {
                bucketName = _bucketName;
                if (!await AmazonS3Util.DoesS3BucketExistV2Async(s3Client, bucketName))
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    PutBucketResponse putBucketResponse = await s3Client.PutBucketAsync(putBucketRequest);
                }
                
                string bucketLocation = await FindBucketLocationAsync(s3Client);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Erro encontrado no servidor. Mensagem:'{0}' ao escrever um objeto", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Desconhecido encontrado no servidor. Mensagem:'{0}' ao escrever um objeto", e.Message);
            }
        }       
        
        static async Task<string> FindBucketLocationAsync(IAmazonS3 client)
        {
            string bucketLocation;
            var request = new GetBucketLocationRequest()
            {
                BucketName = bucketName
            };
            GetBucketLocationResponse response = await client.GetBucketLocationAsync(request);
            bucketLocation = response.Location.ToString();
            Console.WriteLine("Bucket criado\n\n");
            return bucketLocation;
        }

        public async Task<string[]> ListBucketAsyncRefactor()
        {
                           
            AmazonS3Client client = new AmazonS3Client(Constants.UID, Constants.Secret, bucketRegion);
                
            ListBucketsRequest request = new ListBucketsRequest();
                
            ListBucketsResponse response = await client.ListBucketsAsync(request);

            string[] listaBuckets = response.Buckets.Select(x => {
                Console.WriteLine("Bucket {0}, Criado em {1}", x.BucketName, x.CreationDate);
                return x.BucketName;
            }).ToArray();

            return listaBuckets;
        }

        public async Task<string[]> ListBucketAsync()
        {
            string[] listaBucket;
            

            try
            {
                var response = await s3Client.ListBucketsAsync();                
                Console.WriteLine("Proprietário dos Buckets - {0}", response.Owner.DisplayName);
                int i = 0;
                listaBucket = new string[response.Buckets.Count];
                foreach (S3Bucket bucket in response.Buckets)
                {
                    listaBucket[i] = bucket.BucketName;
                    i++;
                    Console.WriteLine("Bucket {0}, Criado em {1}", bucket.BucketName, bucket.CreationDate);
                }
                Console.WriteLine("\n\n");

                
            }
            catch (AmazonS3Exception e)
            {
                listaBucket = new string[0];
                Console.WriteLine("Erro encontrado no servidor. Mensagem:'{0}' ao escrever um objeto", e.Message);                
            }
            catch (Exception e)
            {
                listaBucket = new string[0];
                Console.WriteLine("Desconhecido encontrado no servidor. Mensagem:'{0}' ao escrever um objeto", e.Message);                
            }
            return listaBucket;
        }

        public async Task DeleteBucketAsyncRefactor(string _bucketName)
        {
            AmazonS3Client client = new AmazonS3Client(Constants.UID, Constants.Secret, bucketRegion);

            DeleteBucketRequest request = new DeleteBucketRequest()
            {
                BucketName = _bucketName
            };

            await client.DeleteBucketAsync(request);
        }

        public async Task DeleteBucketAsync(string _bucketName)
        {
            try
            {
                bucketName = _bucketName;
                DeleteBucketRequest request = new DeleteBucketRequest
                {
                    BucketName = bucketName
                };

                DeleteBucketResponse response = await s3Client.DeleteBucketAsync(request);
                Console.WriteLine("Bucket deletado: '{0}'", bucketName);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Erro encontrado no servidor. Mensagem:'{0}' ao escrever um objeto", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Desconhecido encontrado no servidor. Mensagem:'{0}' ao escrever um objeto", e.Message);
            }
        }

        
    }
}
