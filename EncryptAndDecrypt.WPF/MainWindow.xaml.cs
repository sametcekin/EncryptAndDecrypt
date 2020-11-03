using EncryptAndDecrypt.WPF.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
            CryptionType.SelectedIndex = 0;
            IfFolderOrFileNotExist(folderPath, new string[] { jsonPath, txtPath });
        }

        private readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CryptionFiles");
        private readonly string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "CryptionFiles", "crypted.json");
        private readonly string txtPath = Path.Combine(Directory.GetCurrentDirectory(), "CryptionFiles", "crypted.txt");
        void IfFolderOrFileNotExist(string pathFolder, string[] pathFile)
        {
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            foreach (var file in pathFile)
            {
                if (!File.Exists(file))
                {
                    using StreamWriter sw = File.CreateText(file);
                }
            }
        }
        private void CryptionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CryptionType.Text == "Encryption")
            {
                firstText.Text = "Encrypted text";
                secondText.Text = "Decrypted text";
            }
            else
            {
                firstText.Text = "Decrypted text";
                secondText.Text = "Encrypted text";
            }
        }
        private void SaltOrDecrypted_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxDecrypted.Text))
            {
                if (CryptionType.Text == "Encryption")
                {
                    textBoxEncrypted.Text = SecurityHelper.Encrypt(textBoxDecrypted.Text, textBoxSalt.Text);
                }
                else
                {
                    try
                    {
                        textBoxEncrypted.Text = SecurityHelper.Decrypt(textBoxDecrypted.Text, textBoxSalt.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("It can not be decrypted!");
                    }
                }

            }
        }
        private void buttonSaveAsTxt_Click(object sender, RoutedEventArgs e)
        {
            string txtData = $"Tag: { textBoxTag.Text }\n" +
                              $"CreatedDate: { DateTime.Now}\n" +
                              $"Salt: {textBoxSalt.Text}\n" +
                              $"{firstText.Text}: { textBoxDecrypted.Text }\n" +
                              $"{secondText.Text}: { textBoxEncrypted.Text }\n------";

            File.AppendAllText(txtPath, string.Format(txtData + "\n"));
            MessageBox.Show("Successfully written!");
        }
        private void buttonSaveAsJSON_Click(object sender, RoutedEventArgs e)
        {
            CryptionModel model = new CryptionModel
            {
                Tag = textBoxTag.Text,
                Salt = textBoxSalt.Text,
                DecryptedText = textBoxDecrypted.Text,
                EncryptedText = textBoxEncrypted.Text
            };
            string jsonDATA = JsonConvert.SerializeObject(model);

            File.AppendAllText(jsonPath, jsonDATA);
        }
    }
}
