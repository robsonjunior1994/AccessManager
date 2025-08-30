using AccessManager.Api.Models;

namespace AccessManager.Api.Repository.Interface
{
    public interface IUserRepository
    {
        public Task Create(User user);
        public Task<User> GetByEmail(string email);

    }
}
