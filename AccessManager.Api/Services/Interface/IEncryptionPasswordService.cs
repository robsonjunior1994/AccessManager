namespace AccessManager.Api.Services.Interface
{
    public interface IEncryptionPasswordService
    {
        public string EncryptPassword(string openPassword);
        public bool ValidatePassword(string encryptedPassword, string openPassword);
    }
}
