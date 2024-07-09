namespace XUnitCompleteExample.Identity.Interfaces
{
    public interface IPasswordHasher
    {
        HashAlgorithm HashAlgorithm { get; }
        HashWithSaltResult HashWithSalt(string password, byte[] saltBytes, HashAlgorithm hashAlgo);
        bool HashCompare(byte[] Hash_1, byte[] Hash_2);
        byte[] CreateSalt();
    }
}