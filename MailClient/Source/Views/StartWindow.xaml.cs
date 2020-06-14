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
using MailClient.Source.LocalBox;

namespace MailClient
{
    public partial class StartWindow : Window
    {
        public int CUnread;

        public string path = "C:/KursachMailClient/";

        public StartWindow()
        {
            InitializeComponent();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateAccount create = new CreateAccount();
            create.Create(this, path);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnterToAccount enter = new EnterToAccount();
            enter.Enter(this, path);
        }
    }
}
