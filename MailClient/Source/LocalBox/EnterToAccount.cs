using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailClient.Source.LocalBox
{
    class EnterToAccount
    {
        public void Enter(StartWindow startWindow, string path)
        {
            string prKey = File.ReadAllText(path + startWindow.mail.Text + "/Private.txt");
            Crypto crypto = new Crypto();
            if (!Directory.Exists(path + startWindow.mail.Text))
            {
                MessageBox.Show("Аккаунт не существует");
            }
            else
            {
                if (startWindow.password.Password == crypto.Decrypt(File.ReadAllText(path + startWindow.mail.Text + "/PFile.txt"), prKey))
                {
                    File.WriteAllText(@"/user.txt", startWindow.mail.Text);
                    ChooseMail chooseMail = new ChooseMail(startWindow.mail.Text);
                    chooseMail.Show();
                    startWindow.Close();
                }
                else
                {
                    MessageBox.Show("Данные ведены неверно");
                }
            }
        }
    }
}
