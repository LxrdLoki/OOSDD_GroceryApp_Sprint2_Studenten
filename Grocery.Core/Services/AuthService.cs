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
            //Vraagt klantgegevens op
            Client? client = _clientService.Get(email);

            //Controlleerd of er een klant is gevonden, en/of als de wachtwoorden overeen komen.
            if (client == null || PasswordHelper.VerifyPassword(password, client.Password)) return null;

            //Alles klopt klantgegveens worden teruggeven.
            return client;
        }
    }
}
