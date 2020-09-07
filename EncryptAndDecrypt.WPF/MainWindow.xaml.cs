using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            if(CryptionType.SelectedValue.ToString() == "Encryption")
            {
                firstText.Text = "Decrypted text";
                secondText.Text = "Encrypted text";
            }
            else
            {
                firstText.Text = "Encrypted text";
                secondText.Text = "Decrypted text";
            }
        }

        private void textBox1Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1Text.Text))
            {
                if (CryptionType.SelectedValue.ToString() == "Encryption")
                {
                    textBox2Text.Text = SecurityHelper.Encrypt(textBox1Text.Text,textBoxSalt.Text);
                }
                else
                {
                    try
                    {
                        textBox2Text.Text = SecurityHelper.Decrypt(textBox1Text.Text, textBoxSalt.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("It cannot be Decrypted!");
                    }
                }

            }
        }

        string saveToLocal;
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (CryptionType.SelectedValue.ToString() == "Encryption")
                saveToLocal = $"Tag: { textTag.Text }\nCreatedDate: { DateTime.Now}\nSalt: {textBoxSalt.Text}\nDecyrptedText: { textBox1Text.Text }\nEncryptedText: { textBox2Text.Text }\n------";
            else
                saveToLocal = $"Tag: { textTag.Text }\nCreatedDate: { DateTime.Now}\nSalt: {textBoxSalt.Text}\nEncryptedText: { textBox1Text.Text }\nDecryptedText: { textBox2Text.Text}\n";

            string appPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"cryption.txt");

            if (File.Exists(appPath))
            {
                File.AppendAllText(appPath, string.Format(saveToLocal+ "\n"));
                MessageBox.Show("Successfully written!");
            }
           
        }
    }
}
