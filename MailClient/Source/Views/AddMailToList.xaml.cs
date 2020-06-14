using System.Windows;
using System.Windows.Input;
using MailClient.Source.LocalBox;

namespace MailClient
{
    public partial class AddMailToList : Window
    {
        public string Login { get; set; }

        public AddMailToList(string login)
        {
            InitializeComponent();
            Login = login;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMailToAccount addMailToAccount = new AddMailToAccount();
            addMailToAccount.AddMail(this);
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
