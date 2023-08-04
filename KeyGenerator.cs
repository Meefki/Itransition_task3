using System.Security.Cryptography;

namespace task3;

internal class KeyGenerator
{
    public byte[] Create(int size)
    {
        using (var generator = RandomNumberGenerator.Create())
        {
            var salt = new byte[size];
            generator.GetBytes(salt);
            return salt;
        }
    }
}
