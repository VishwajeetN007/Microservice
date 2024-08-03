using Azure.Security.KeyVault.Secrets;

namespace KeyVault.Services
{
    public class KeyVaultService : IKeyVaultService
    {

        private readonly SecretClient _secretClient;
        public KeyVaultService(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        public async Task<string> GetSecret(string secretKey)
        {
            try
            {
                KeyVaultSecret Secret = await _secretClient.GetSecretAsync(secretKey);
                return Secret.Value;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<string> SetSecret(string secretName, string secretValue)
        {
            var secret = await _secretClient.SetSecretAsync(secretName, secretValue);
            return secret.Value.Value;
        }

        public async Task<string> DeleteSecret(string secretName)
        {
            var operation = await _secretClient.StartDeleteSecretAsync(secretName);
            var secret = await operation.WaitForCompletionAsync();
            _secretClient.PurgeDeletedSecret(operation.Value.Name);
            return secret.Value.Value;
        }
    }
}
