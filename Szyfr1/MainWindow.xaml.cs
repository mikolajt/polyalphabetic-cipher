using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Szyfr1 {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void Button_Cipher_Encryption(object sender, RoutedEventArgs e) {
            if (KeyEncryption.Text.Length < 10 || KeyEncryption.Text.Length > 40 || KeyEncryption.Text.Any(char.IsPunctuation) || KeyEncryption.Text.Any(char.IsNumber) || KeyEncryption.Text.Any(char.IsWhiteSpace)) {
                MessageBox.Show("Podano zły klucz");
            }
            else if(KeyEncryption.Text.Length >= OrginalEncryption.Text.Length) {
                MessageBox.Show("Tekst musi być dłuższy od klucza");
            }
            else {
                Cipher c = new Cipher();
                List<int> right = new List<int>();
                List<int> left = new List<int>();
                right = c.GenerateDigits("r", KeyEncryption.Text);
                left = c.GenerateDigits("l", KeyEncryption.Text);
                Cipher cipher = new Cipher(OrginalEncryption.Text, KeyEncryption.Text, right, left);
                EncryptedEncryption.Text = cipher.Encryption();
            }
        }

        private void Button_Decipher_Decryption(object sender, RoutedEventArgs e) {
            if (KeyDecryption.Text.Length < 10 || KeyDecryption.Text.Length > 40 || KeyDecryption.Text.Any(char.IsPunctuation) || KeyDecryption.Text.Any(char.IsNumber) || KeyDecryption.Text.Any(char.IsWhiteSpace)) {
                MessageBox.Show("Podano zły klucz");
            }
            else if (KeyDecryption.Text.Length >= CryptedDecryption.Text.Length) {
                MessageBox.Show("Tekst musi być dłuższy od klucza");
            }
            else {
                Cipher c = new Cipher();
                List<int> right = new List<int>();
                List<int> left = new List<int>();
                right = c.GenerateDigits("r", KeyDecryption.Text);
                left = c.GenerateDigits("l", KeyDecryption.Text);
                Decipher decipher = new Decipher(CryptedDecryption.Text, KeyDecryption.Text, right, left);
                DecryptedDecryption.Text = decipher.Decode();
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Button_Min(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void Button_Help(object sender, RoutedEventArgs e) {
            string curDir = Directory.GetCurrentDirectory();
            Process.Start("help.html");
        }

        private void Button_Read_Encryption(object sender, RoutedEventArgs e) {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.ShowDialog();
            string path = ofd.FileName;
            if(File.Exists(path) && System.IO.Path.GetExtension(path) == ".txt") {
                OrginalEncryption.Text = File.ReadAllText(path);
            }
            else {
                MessageBox.Show("Wybrano błędny plik");
            }
        }

        private void Button_Read_Decryption(object sender, RoutedEventArgs e) {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.ShowDialog();
            string path = ofd.FileName;
            if (File.Exists(path) && System.IO.Path.GetExtension(path) == ".txt") {
                CryptedDecryption.Text = File.ReadAllText(path);
            }
            else {
                MessageBox.Show("Wybrano błędny plik");
            }
        }

        private void Button_Save_Encryption(object sender, RoutedEventArgs e) {
            string text = EncryptedEncryption.Text;
            var sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "Text documents (.txt)|*.txt";

            if (sfd.ShowDialog() == true) {
                File.WriteAllText(sfd.FileName, text);
            }
        }

        private void Button_Save_Decryption(object sender, RoutedEventArgs e) {
            string text = DecryptedDecryption.Text;
            var sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "Text documents (.txt)|*.txt";

            if (sfd.ShowDialog() == true) {
                File.WriteAllText(sfd.FileName, text);
            }
        }

        private void Button_Move_Encryption(object sender, RoutedEventArgs e) {
            CryptedDecryption.Text = EncryptedEncryption.Text;
            KeyDecryption.Text = KeyEncryption.Text;
        }

        private void Button_Move_Decryption(object sender, RoutedEventArgs e) {
            OrginalEncryption.Text = DecryptedDecryption.Text;
            KeyEncryption.Text = KeyDecryption.Text;
        }

        private void Button_Clear(object sender, RoutedEventArgs e) {
            KeyEncryption.Text = "";
            KeyDecryption.Text = "";
            OrginalEncryption.Text = "";
            EncryptedEncryption.Text = "";
            CryptedDecryption.Text = "";
            DecryptedDecryption.Text = "";
        }
    }
}
