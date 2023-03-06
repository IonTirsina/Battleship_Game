using Battleship.Domain.Common;

namespace Battleship.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public User(string userName, string passwordHash)
        {
            this.UserName = userName;
            this.PasswordHash = passwordHash;
        }
    }


}
