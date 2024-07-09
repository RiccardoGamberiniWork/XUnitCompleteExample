namespace XUnitCompleteExample.Identity.Models
{
    public class HashWithSaltResult
    {
        public byte[] Salt { get; }
        public byte[] Digest { get; set; }

        public HashWithSaltResult(byte[] saltP, byte[] digestP)
        {
            Salt = saltP;
            Digest = digestP;
        }
    }
}
