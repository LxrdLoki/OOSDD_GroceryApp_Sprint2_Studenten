
namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        private string _emailAddress;
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                // Kan hier eventueel validatie aan toevoegen
                _password = value;
            }
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                // Kan hier eventueel validatie aan toevoegen
                _emailAddress = value;
            }
        }
        public Client(int id, string  name, string emailAddress, string password) : base(id, name)
        {
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}
