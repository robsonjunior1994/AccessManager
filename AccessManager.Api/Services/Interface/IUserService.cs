using AccessManager.Api.Common;
using AccessManager.Api.DTOs;
using AccessManager.Api.Models;

namespace AccessManager.Api.Services.Interface
{
    public interface IUserService
    {
        public Task<Result<User>> Create(UserDTO userDTO);
    }
}
