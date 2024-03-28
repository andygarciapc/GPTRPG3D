using NUnit.Framework;
using BasicSecurity;
using System.Text;

namespace BasicTests
{

    public class AesEncryptionTests
    {
        private AesEncryption aesEncryption;

        [SetUp]
        public void Setup()
        {
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");
            aesEncryption = new AesEncryption(key, iv);
        }

        [Test]
        public void EncryptDecrypt_ReturnsOriginalString()
        {
            string originalString = "TestString";
            var encrypted = aesEncryption.Encrypt(originalString);
            var decrypted = aesEncryption.Decrypt(encrypted);
            Assert.AreEqual(originalString, decrypted);
        }
    }
}
