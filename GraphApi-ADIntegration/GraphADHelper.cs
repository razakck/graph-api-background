using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace GraphApi_ADIntegration
{
    public class GraphADHelper : IGraphADHelper
    {

        // Settings object
        private readonly Settings _settings;
        // App-ony auth token credential
        private readonly ClientSecretCredential? _clientSecretCredential;
        // Client configured with app-only authentication
        private readonly GraphServiceClient? _appClient;
        public GraphADHelper(IOptions<Settings?> settings)
        {
            _settings = settings.Value;

            if (_clientSecretCredential == null)
            {
                _clientSecretCredential = new ClientSecretCredential(
                    _settings.TenantId, _settings.ClientId, _settings.ClientSecret);
            }

            if (_appClient == null)
            {
                _appClient = new GraphServiceClient(_clientSecretCredential,
                    // Use the default scope, which will request the scopes
                    // configured on the app registration
                    new[] { "https://graph.microsoft.com/.default" });
            }

        }
        

        public async Task<List<User>?> ListUsersAsync()
        {
            try
            {

          
            _ = _appClient ??
       throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

            return (await _appClient.Users.GetAsync((config) =>
            {
                // Only request specific properties
                config.QueryParameters.Select = new[] { "displayName", "id", "mail" };
                // Get at most 25 results
                config.QueryParameters.Top = 25;
                // Sort by display name
                config.QueryParameters.Orderby = new[] { "displayName" };
            }))?.Value; 
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
