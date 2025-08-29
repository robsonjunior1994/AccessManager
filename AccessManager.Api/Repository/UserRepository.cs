using AccessManager.Api.Models;
using AccessManager.Api.Repository.Interface;

namespace AccessManager.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        public bool Create(User user)
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
