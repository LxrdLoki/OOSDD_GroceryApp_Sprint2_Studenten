
namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        private string? _emailAddress;
        private string? _password;

        public string? Password
        {
            get => _password;
            set
            {
                // Could add validation here
                _password = value!;
            }
        }

        public string? EmailAddress
        {
            get => _emailAddress;
            set
            {
                // Could add validation here
                _emailAddress = value!;
            }
        }
        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            // Could add a check here to ensure emailAddress and password are not null or empty
            if (string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Fields cannot be null or empty", nameof(emailAddress));
            }

            Password = password;
            EmailAddress = emailAddress;

        }
    }
}
