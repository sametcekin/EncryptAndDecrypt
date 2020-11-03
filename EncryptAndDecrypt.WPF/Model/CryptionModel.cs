using System;

namespace EncryptAndDecrypt.WPF.Model
{
    public class CryptionModel
    {
        public string Tag { get; set; }
        public string Salt { get; set; }
        public string DecryptedText { get; set; }
        public string EncryptedText { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
