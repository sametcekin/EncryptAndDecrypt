using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptAndDecrypt.WPF
{
    public class CryptionModel
    {
        public string Tag { get; set; }
        public string Salt { get; set; }
        public string EncryptedText { get; set; }
    }
}
