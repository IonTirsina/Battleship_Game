using Battleship.Domain.Common;

namespace Battleship.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }

        public User(string UserName)
        {
            this.UserName = UserName;
        }
    }


}
