using MailClient.Source.Operations;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailClient.Source.LocalBox
{
    class AddMailToAccount
    {
        public void AddMail(AddMailToList addMailToList)
        {
            try
            {
                using (var client = new ImapClient())
                {
                    GetMailSuffix getMailSuffix = new GetMailSuffix();
                    string IMapAddress = "imap." + getMailSuffix.GetSuffix(addMailToList.mail.Text);
                    client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                    client.Connect(IMapAddress, 993, true);
                    client.Authenticate(addMailToList.mail.Text, addMailToList.password.Password);
                    client.Disconnect(true);

                    List<string> TMail = new List<string>(); TMail.Add(addMailToList.mail.Text);
                    List<string> TPassword = new List<string>(); TPassword.Add(addMailToList.password.Password);

                    File.AppendAllLines("C:/KursachMailClient/" + addMailToList.Login + "/Boxes.txt", TMail);
                    File.AppendAllLines("C:/KursachMailClient/" + addMailToList.Login + "/Data.txt", TPassword);

                    ChooseMail CM = new ChooseMail(File.ReadAllText(@"/user.txt"));
                    CM.Show();

                    addMailToList.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
