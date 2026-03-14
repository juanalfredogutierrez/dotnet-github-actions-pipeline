
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace SunatIntegration.Infrastructure.Configuration
{
    public static class KeyVaultConfiguration
    {
        public static IConfigurationBuilder AddKeyVault(
        this IConfigurationBuilder builder,
        IConfiguration configuration)
        {
            var keyVaultUrl = configuration["KeyVault:VaultUrl"];

            if (!string.IsNullOrEmpty(keyVaultUrl))
            {
                builder.AddAzureKeyVault(
                    new Uri(keyVaultUrl),
                    new DefaultAzureCredential());
            }

            return builder;
        }
    }
}
