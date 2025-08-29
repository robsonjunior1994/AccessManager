using AccessManager.Api.Models;

namespace AccessManager.Api.Repository.Interface
{
    public interface IUserRepository
    {
        public bool Create(User user);
        public User GetByEmail(string email);

    }
}
