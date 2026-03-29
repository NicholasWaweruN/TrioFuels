using System;
using System.Security.Cryptography;
using System.Text;

public class Secrets 
{
	private static readonly string key = "t6r5e4543gftyre45e4532gyugytrdre4ff32b7f6a55796bcf406e3c4c880084228b247551fe94aa792366a65e4e8d0897b3395"; // Use a secure key

	public static string Encrypt(string textToEncrypt)
	{
		using var aes = Aes.Create();
		var encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(key), aes.IV);
		using var memoryStream = new System.IO.MemoryStream();
		using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
		{
			using var streamWriter = new System.IO.StreamWriter(cryptoStream);
			streamWriter.Write(textToEncrypt);
		}
		var iv = aes.IV;
		var encrypted = memoryStream.ToArray();
		var result = new byte[iv.Length + encrypted.Length];
		Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
		Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);
		return Convert.ToBase64String(result);
	}

	public static string Decrypt(string encryptedText)
	{
		var fullCipher = Convert.FromBase64String(encryptedText);
		using var aes = Aes.Create();
		var iv = new byte[aes.BlockSize / 8];
		var cipher = new byte[fullCipher.Length - iv.Length];
		Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
		Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);
		var decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(key), iv);
		using var memoryStream = new System.IO.MemoryStream(cipher);
		using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
		using var streamReader = new System.IO.StreamReader(cryptoStream);
		return streamReader.ReadToEnd();
	}
}
