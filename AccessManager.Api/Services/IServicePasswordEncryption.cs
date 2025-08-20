namespace AccessManager.Api.Services
{
    public interface IServiceEncryptionPassword
    {
        public string EncryptPassword(string openPassword);
        public bool ValidatePassword(string encryptedPassword, string openPassword);
    }
}
