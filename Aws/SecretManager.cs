using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text;

namespace Secret.Aws
{
    public class SecretManager : ISecretManager
    {
        private readonly string _secretName;
        private readonly string _region;
        private readonly string _versionStage;

        public SecretManager()
        {
            _secretName = Environment.GetEnvironmentVariable("SecretName");
            _region = Environment.GetEnvironmentVariable("SecretRegion");
            _versionStage = Environment.GetEnvironmentVariable("SecretVersionStage");
        }

        public string GetSecret() 
        {
            var client = new AmazonSecretsManagerClient
            (
                awsAccessKeyId: Environment.GetEnvironmentVariable("AwsAccessKeyId"),
                awsSecretAccessKey: Environment.GetEnvironmentVariable("AwsSecretAccessKey"),
                region: RegionEndpoint.USEast1
            );
            
            var request = new GetSecretValueRequest
            {
                SecretId = _secretName,
                VersionStage = _versionStage
            };

            var response = client.GetSecretValueAsync(request).Result;

            if (response.SecretString != null)
            {
                return response.SecretString;
            }

            var memoryStream = response.SecretBinary;
            var reader = new StreamReader(memoryStream);
            var decodedBinarySecret = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            return decodedBinarySecret;
        }
    }
}