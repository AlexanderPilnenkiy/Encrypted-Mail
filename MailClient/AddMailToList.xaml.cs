using System.Windows;
using System.Windows.Input;
using MailKit.Net.Imap;
using MailKit;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Windows.Threading;
using MailKit.Search;

namespace MailClient
{
    /// <summary>
    /// Логика взаимодействия для AddMailToList.xaml
    /// </summary>
    public partial class AddMailToList : Window
    {
        string log;
        public AddMailToList(string login)
        {
            InitializeComponent();
            log = login;
        }

        private string getSuffix()
        {
            string len = mail.Text;
            string email = len.Substring(len.LastIndexOf('@') + 1, len.Length - (len.LastIndexOf('@') + 1));
            return email;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var client = new ImapClient())
                {
                    string IMapAddress = "imap." + getSuffix();
                    client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                    client.Connect(IMapAddress, 993, true);
                    client.Authenticate(mail.Text, password.Password);
                    client.Disconnect(true);

                    List<string> TMail = new List<string>();        TMail.Add(mail.Text);
                    List<string> TPassword = new List<string>();    TPassword.Add(password.Password);

                    File.AppendAllLines("C:/KursachMailClient/" + log + "/Boxes.txt", TMail);
                    File.AppendAllLines("C:/KursachMailClient/" + log + "/Data.txt", TPassword);

                    ChooseMail CM = new ChooseMail(File.ReadAllText(@"/user.txt"));
                    CM.Show();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Выход",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
