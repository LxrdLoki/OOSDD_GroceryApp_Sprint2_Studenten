using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;
        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }
        public Client? Login(string email, string password)
        {
            // Gets client data
            Client? client = _clientService.Get(email);

            // Checks if client was found, and if password matches
            if (client == null || PasswordHelper.VerifyPassword(password, client.Password!)) return null;

            // If everything is correct, Client is returned
            return client;
        }
    }
}
