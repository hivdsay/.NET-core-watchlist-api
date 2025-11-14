
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace WatchList.Core.Tools.Concrete.PasswordHashing;

public class PasswordHash
{
    public static string HashHMACHex(string message)
    {
        byte[] hash = HashHMAC(HexDecode(PasswordKeys.hashMacKey), StringEncode(message));
        return HashEncode(hash);
    }

    public static string HashSHAHex(string message)
    {
        byte[] hash = HashSHA(HexDecode(PasswordKeys.innerKey), HexDecode(PasswordKeys.outerKey), StringEncode(message));
        return HashEncode(hash);
    }

    private static byte[] HashSHA(byte[] innerKey, byte[] outerKey, byte[] message)
    {
        var hash = new SHA256Managed();

        byte[] innerData = new byte[innerKey.Length + message.Length];
        Buffer.BlockCopy(innerKey, 0, innerData, 0, innerKey.Length);
        Buffer.BlockCopy(message, 0, innerData, innerKey.Length, message.Length);
        byte[] innerHash = hash.ComputeHash(innerData);

        byte[] data = new byte[outerKey.Length + innerHash.Length];
        Buffer.BlockCopy(outerKey, 0, data, 0, outerKey.Length);
        Buffer.BlockCopy(innerHash, 0, data, outerKey.Length, innerHash.Length);
        byte[] result = hash.ComputeHash(data);
        return result;
    }
    private static byte[] StringEncode(string text)
    {
        var encoding = new ASCIIEncoding();
        return encoding.GetBytes(text);
    }

    private static string HashEncode(byte[] hash)
    {
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    private static byte[] HexDecode(string hex)
    {
        var bytes = new byte[hex.Length / 2];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);
        }
        return bytes;
    }

    private static byte[] HashHMAC(byte[] key, byte[] message)
    {
        var hash = new HMACSHA256(key);
        return hash.ComputeHash(message);
    }
}

