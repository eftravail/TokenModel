using Thinktecture.IdentityModel;

namespace Generator
{
    static internal class Keys
    {
        static internal byte[] CreateRandomKey(int length)
        {
            return CryptoRandom.CreateRandomKey(length);
        }

        static internal string CreateRandomKeyString(int length)
        {
            return CryptoRandom.CreateRandomKeyString(length);
        }
    }
}
