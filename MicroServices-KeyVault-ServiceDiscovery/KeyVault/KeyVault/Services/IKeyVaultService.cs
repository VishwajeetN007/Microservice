namespace KeyVault.Services
{
    public interface IKeyVaultService
    {
        Task<string> GetSecret(string secretKey);
        Task<string> SetSecret(string secretName, string secretValue);
        Task<string> DeleteSecret(string secretName);
    }
}
