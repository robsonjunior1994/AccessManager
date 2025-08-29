using AccessManager.Api.Common;
using AccessManager.Api.DTOs;
using AccessManager.Api.Models;
using AccessManager.Api.Repository.Interface;

namespace AccessManager.Api.Services.Interface
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionPasswordService _encryptionPasswordService;
        public UserService(IUserRepository userRepository, 
            IEncryptionPasswordService encryptionPasswordService)
        {
            _userRepository = userRepository;
            _encryptionPasswordService = encryptionPasswordService;
        }
        public Result<User> Create(UserDTO userDTO)
        {
            var user = _userRepository.GetByEmail(userDTO.Email);
            if (user != null)
                return Result<User>.Failure("User already exist", ErrorCode.USER_ALREADY_EXISTS);

            var newCompany = new Company
            {
                Name = userDTO.CompanyDTO.Name,
                Number = userDTO.CompanyDTO.Number,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = false,
                Users = new List<User>()
            };

            var newUser = new User
            {

                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = _encryptionPasswordService.EncryptPassword(userDTO.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = false,
                Roles = new List<string> { Roles.admin.ToString() },
                Company = newCompany
            };

            newCompany.Users.Add(newUser);

            try
            {
                _userRepository.Create(newUser);
                return Result<User>.Success(newUser);
            }
            catch (Exception ex)
            {
                // CRIAR SERVIÇO DE LOG
                return Result<User>.Failure("An error occurred while creating the user.", ErrorCode.DATABASE_ERROR);
            }
        }
    }
}
