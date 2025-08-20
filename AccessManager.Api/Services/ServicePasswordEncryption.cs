using System.Security.Cryptography;

namespace AccessManager.Api.Services
{
    public class ServicePasswordEncryption : IServiceEncryptionPassword
    {
        // Tamanho do Hash final (ex: 256 bits = 32 bytes)
        private const int HashSize = 32;
        // Tamanho do Salt (ex: 128 bits = 16 bytes)
        private const int SaltSize = 16;
        // Número de iterações. Mais iterações = mais seguro porém mais lento.
        private const int Iterations = 10000;

        public string EncryptPassword(string password)
        {
            // 1. Gera um salt aleatório (único para cada senha)
            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            // 2. Gera o hash da senha usando PBKDF2
            // A variavel pbkdf2 é uma instância de Rfc2898DeriveBytes, e por si só não é um hash, mas sim uma classe que gera um hash baseado em PBKDF2.
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);

            // 3. Combina o salt e o hash em um único array de bytes para armazenar
            var hashToStore = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashToStore, 0, SaltSize);
            Array.Copy(hash, 0, hashToStore, SaltSize, HashSize);

            // 4. Converte para Base64 para armazenar como string no banco
            return Convert.ToBase64String(hashToStore);
        }

        public bool ValidatePassword(string openPassword, string encryptedPassword)
        {
            // 1. Converte a string armazenada de volta para bytes
            var hashBytes = Convert.FromBase64String(encryptedPassword);

            // 2. Extrai o salt do início do array
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // 3. Gera o hash da senha aberta usando o mesmo salt e iterações para comparação com a já armazenada
            var pbkdf2 = new Rfc2898DeriveBytes(openPassword, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // 4. Compara cada byte do hash gerado com o hash armazenado
            for (int i = 0; i < HashSize; i++)
            {
                // Se algum byte for diferente, a senha está errada
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
