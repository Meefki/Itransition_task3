using Chilkat;

namespace task3;

internal class HmacSha3Generator
{
    private readonly int _blockSize;

    public HmacSha3Generator(int blockSize)
    {
        _blockSize = blockSize;
    }

    public string Create(byte[] key, string message) // https://en.wikipedia.org/wiki/HMAC#Implementation
    {
        key = ComputeBlockSizedKey(key);

        byte[] ipad = new byte[_blockSize];
        byte[] opad = new byte[_blockSize];

        for (int i = 0; i < _blockSize; i++)
        {
            ipad[i] = 0x36;
            opad[i] = 0x5c;
        }

        byte[] ikeypad = XORByteArray(ipad, key);
        byte[] okeypad = XORByteArray(opad, key);

        string hashStr = "";
        using (var crypt = new Crypt2() { HashAlgorithm = "sha3-256" })
        {
            string ikeypadStr = BitConverter.ToString(ikeypad).Replace("-", "");
            string okeypadStr = BitConverter.ToString(okeypad).Replace("-", "");

            string ikeypadHash = BitConverter.ToString(crypt.HashString(ikeypadStr + message)).Replace("-", "");

            byte[] hash = crypt.HashString(okeypadStr + ikeypadHash);
            hashStr = BitConverter.ToString(hash).Replace("-", "");
        }

        return hashStr;
    }

    private byte[] XORByteArray(byte[] left, byte[] right)
    {
        byte[] val = new byte[left.Length];
        for (int i = 0; i < left.Length; i++)
            val[i] = (byte)(left[i] ^ right[i]);

        return val;
    }

    private byte[] ComputeBlockSizedKey(byte[] key)
    {
        if (key.Length > _blockSize)
        {
            using (var crypt = new Crypt2() { CryptAlgorithm = "sha3-256" })
                key = crypt.HashBytes(key);
        }

        if (key.Length < _blockSize)
        {
            byte[] val = new byte[_blockSize];
            for (int i = 0; i < _blockSize; i++)
            {
                if (i <= key.Length)
                    val[i] = key[i];
                else
                    val[i] = 0x00;
            }

            key = val;
        }

        return key;
    }
}
