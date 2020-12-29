using System;
using System.IO;
using System.Security.Cryptography;
using crypto = System.Security.Cryptography;
/// <summary>
/// reversible, single key encryption 
/// </summary>
namespace EstudosCSharp.Security.Crypto.Symmetric
{
    public static class Aes
    {
        //Encryption key used to encrypt the stream.
        //The same value must be used to encrypt and decrypt the stream.
        public static readonly byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        public static void Encrypt(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                using (crypto.Aes aes = crypto.Aes.Create())
                {
                    //Create a new instance of the default Aes implementation class  
                    // and configure encryption key.
                    aes.Key = key;

                    //Stores IV at the beginning of the file.
                    //This information will be used for decryption.
                    byte[] iv = aes.IV;
                    fileStream.Write(iv, 0, iv.Length);

                    //Create a CryptoStream, pass it the FileStream, and encrypt
                    //it with the Aes class.  
                    using (CryptoStream cryptStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    //Create a StreamWriter for easy writing to the
                    //file stream.  
                    using (StreamWriter sWriter = new StreamWriter(cryptStream))
                    {
                        sWriter.WriteLine("Hi!");
                        sWriter.WriteLine("File was encrypted!");
                    }

                }
            } catch
            {
                Console.WriteLine("Encryption failed");
                throw;
            }
        }

        public static void Decrypt(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                using (crypto.Aes aes = crypto.Aes.Create())
                {
                    //Reads IV value from beginning of the file.
                    byte[] iv = new byte[aes.IV.Length];
                    fileStream.Read(iv, 0, iv.Length);

                    fileStream.Position = 0;
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                    using (StreamReader txtreader = new StreamReader(cryptoStream))
                    {
                        var txt = txtreader.ReadLine();
                        //Convert.ToBase64String(b);
                        Console.WriteLine($"Original content line:{Environment.NewLine}{txt}");
                        Console.WriteLine("File was decrypted");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption error!");
                throw;
            }
        }
    }
}
