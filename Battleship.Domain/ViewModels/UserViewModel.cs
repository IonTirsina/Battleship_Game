using Battleship.Domain.Entities;

namespace Battleship.Domain.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            UserName = user.UserName;
        }
        public string UserName { get; private set; }
    }
}
