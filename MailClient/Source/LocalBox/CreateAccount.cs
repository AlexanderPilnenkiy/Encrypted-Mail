using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailClient.Source.LocalBox
{
    class CreateAccount
    {
        public void Create(StartWindow startWindow, string path)
        {
            Crypto crypto = new Crypto();
            if (!Directory.Exists(path + startWindow.mail.Text))
            {
                Directory.CreateDirectory(path + startWindow.mail.Text);
                var keys = crypto.GenerateKeys(Crypto.RSAKeySize.Key2048);
                File.WriteAllText(path + startWindow.mail.Text + "/PFile.txt", crypto.Encrypt(startWindow.password.Password, keys.PrivateKey));
                if (!File.Exists(path + startWindow.mail.Text + "/Public.txt"))
                {
                    File.WriteAllText(path + startWindow.mail.Text + "/Public.txt", keys.PublicKey);
                }
                if (!File.Exists(path + startWindow.mail.Text + "/Private.txt"))
                {
                    File.WriteAllText(path + startWindow.mail.Text + "/Private.txt", keys.PrivateKey);
                }
                MessageBox.Show("Аккаунт создан");
            }
            else
            {
                MessageBox.Show("Логин занят");
            }
        }
    }
}
