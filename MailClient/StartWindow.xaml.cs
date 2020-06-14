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
    public partial class StartWindow : Window
    {
        List<string> FromWho = new List<string>();
        List<string> Theme = new List<string>();
        List<string> Dates = new List<string>();
        List<string> ID = new List<string>();

        DispatcherTimer timer = new DispatcherTimer();

        private string PWData;
        private string EMailData;
        private string SuffixData;

        int start_letter = 0, count_back = 0, last_index;
        public int CUnread;

        int IMapPort = 993;
        public string path = "C:/KursachMailClient/";

        public StartWindow()
        {
            InitializeComponent();
            File.Delete(@"/user.txt");
            File.Create(@"/user.txt");
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

        private void CreateDirectories()
        {
            string prKey = File.ReadAllText(path + mail.Text + "/Private.txt");
            Crypto crypto = new Crypto();
            if (!Directory.Exists(path + mail.Text))
            {
                MessageBox.Show("Аккаунт не существует");
            }
            else
            {
                if (password.Password == crypto.Decrypt(File.ReadAllText(path + mail.Text + "/PFile.txt"), prKey))
                {
                    //MessageBox.Show(crypto.Decrypt(File.ReadAllText(path + mail.Text + "/PFile.txt"), prKey));
                    File.WriteAllText(@"/user.txt", mail.Text);
                    ChooseMail main = new ChooseMail(mail.Text);
                    //MainWindow main = new MainWindow(PWData, EMailData, SuffixData);
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Данные ведены неверно");
                }
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            //grid_cd.Visibility = Visibility.Visible;
        }

        private void Button_GotFocus(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0);
            timer.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Crypto crypto = new Crypto();
            if (!Directory.Exists(path + mail.Text))
            {
                Directory.CreateDirectory(path + mail.Text);
                var keys = crypto.GenerateKeys(Crypto.RSAKeySize.Key2048);
                File.WriteAllText(path + mail.Text + "/PFile.txt", crypto.Encrypt(password.Password, keys.PrivateKey));
                if (!File.Exists(path + mail.Text + "/Public.txt"))
                {
                    File.WriteAllText(path + mail.Text + "/Public.txt", keys.PublicKey);
                }
                if (!File.Exists(path + mail.Text + "/Private.txt"))
                {
                    File.WriteAllText(path + mail.Text + "/Private.txt", keys.PrivateKey);
                }
                MessageBox.Show("Аккаунт создан");
            }
            else
            {
                MessageBox.Show("Логин занят");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateDirectories();
        }
    }
}
