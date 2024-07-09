namespace XUnitCompleteExample.Identity.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int _saltBytesNumber = 64;

        public HashAlgorithm HashAlgorithm { get => SHA512.Create(); }

        public bool HashCompare(byte[] Hash_1, byte[] Hash_2)
        {
            int diff = Hash_1.Length ^ Hash_2.Length;
            int i = 0;

            while (i < Hash_1.Length && i < Hash_2.Length)
            {
                diff = diff | (Hash_1[i] ^ Hash_2[i]);
                i += 1;
            }

            return diff == 0;
        }

        public HashWithSaltResult HashWithSalt(string password, byte[] saltBytes, HashAlgorithm hashAlgo)
        {
            byte[] passwordAsBytes = System.Text.Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            // Return New HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes))
            return new HashWithSaltResult(saltBytes, digestBytes);
        }

        //DA FARE.
        // Sostituire RNGCryptoServiceProvider con RandomNumberGenerator.
        public byte[] CreateSalt()
        {
            byte[] salt = new byte[_saltBytesNumber];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetNonZeroBytes(salt);
            }
            return salt;
        }
    }
}