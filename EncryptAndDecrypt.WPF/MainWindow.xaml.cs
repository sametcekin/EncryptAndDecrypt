using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EncryptAndDecrypt.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CryptionType.Items.Add("Encryption");
            CryptionType.Items.Add("Decryption");
        }

        private void CryptionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CryptionType.SelectedValue.ToString())
            {
                case "Encryption":
                    firstText.Text = "Decrypted text";
                    secondText.Text = "Encrypted text";
                    break;
                default:
                    firstText.Text = "Encrypted text";
                    secondText.Text = "Decrypted text";
                    break;
            }
        }

        private void textBox1Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textDecrypted.Text))
            {
                if (CryptionType.SelectedValue.ToString() == "Encryption")
                {
                    textEncrypted.Text = SecurityHelper.Encrypt(textDecrypted.Text,textBoxSalt.Text);
                }
                else
                {
                    try
                    {
                        textEncrypted.Text = SecurityHelper.Decrypt(textDecrypted.Text, textBoxSalt.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("It can not be decrypted!");
                    }
                }

            }
        }
        
        #region txt File
        string saveToLocal;
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (CryptionType.SelectedValue.ToString() == "Encryption")
                saveToLocal = $"Tag: { textTag.Text }\nCreatedDate: { DateTime.Now}\nSalt: {textBoxSalt.Text}\nDecyrptedText: { textDecrypted.Text }\nEncryptedText: { textEncrypted.Text }\n------";
            else
                saveToLocal = $"Tag: { textTag.Text }\nCreatedDate: { DateTime.Now}\nSalt: {textBoxSalt.Text}\nEncryptedText: { textDecrypted.Text }\nDecryptedText: { textEncrypted.Text}\n";

            string appPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"cryption.txt");

            if (File.Exists(appPath))
            {
                File.AppendAllText(appPath, string.Format(saveToLocal+ "\n"));
                MessageBox.Show("Successfully written!");
            }
           
        }
        #endregion
        
        #region json File
        private readonly string jsonPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "crypted.json");
        bool IfExistJSONFile()
        {
            if (!File.Exists(jsonPath))
            {
                using StreamWriter sw = File.CreateText(jsonPath);
                return true;
            }
            return false;
        }
        private void buttonSaveAsJSON_Click(object sender, RoutedEventArgs e)
        {
            if (IfExistJSONFile())
            {
                CryptionModel model = new CryptionModel
                {
                    Tag = textTag.Text,
                    Salt=textBoxSalt.Text,
                    EncryptedText = textEncrypted.Text
                };
                string jsonDATA = JsonConvert.SerializeObject(model);
                
                //write string to file
                File.WriteAllText(jsonPath, jsonDATA);
            }
        }
        #endregion
    }
}
