using System.Windows;
using System.Windows.Input;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailClient
{
    public partial class MainWindow : Window
    {
        public string _PWData { get; set; }
        public string _EMailData { get; set; }
        public string _SuffixData { get; set; }
        int position = 0;
        string ForFunctions, password;
        int countMes, uid;

        List<string> FromWho = new List<string>();
        List<string> Theme = new List<string>();
        List<string> Dates = new List<string>();

        public MainWindow(string pWData, string eMailData, string suffixData, string psw)
        {
            InitializeComponent();
            _PWData = pWData;
            _EMailData = eMailData;
            _SuffixData = suffixData;
            password = psw;
            LoadedData();
            LoadMessages();
        }

        public void LoadedData()
        {
            Chip.Content = File.ReadAllText(@"/temp.txt");
            Chip.Icon = Convert.ToString(Chip.Content).Substring(0, 1).ToUpper();
            File.Delete(@"/temp.txt");
            ForFunctions = Convert.ToString(Chip.Content);
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

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SentMessage sm = new SentMessage(Convert.ToString(Chip.Content), _PWData);
            sm.Show();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            LoadMessages();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            LoadSentMessages();
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            LoadTrashMessages();
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            LoadSpamMessages();
        }

        private void CheckingPagesCount(int Count)
        {
            if (Count <= 10)
            {
                LPage.IsEnabled = false;
                NPage.IsEnabled = false;
            }
            else
            {
                NPage.IsEnabled = true;
            }
        }

        public void LoadMessages()
        {
            MSList.Items.Clear();
            try
            {
                var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Theme.txt").ToList();
                var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/FromWho.txt").ToList();
                var Data = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Date.txt").ToList();

                countMes = Themes.Count;

                number.Content = File.ReadAllText(@"/count.txt");

                CheckingPagesCount(countMes);

                Themes.Reverse();
                From.Reverse();
                Data.Reverse();

                for (int i = position; i < position + 10; i++)
                {
                    MSList.Items.Add(new LetterConstruction { Name = From[i], Description = Themes[i], Date = Data[i].Substring(0, Data[i].LastIndexOf(' ')) });
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        public void LoadSentMessages()
        {
            MSList.Items.Clear();
            try
            {
                var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/Theme.txt").ToList();
                var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/FromWho.txt").ToList();
                var Data = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/Date.txt").ToList();

                countMes = Themes.Count;

                CheckingPagesCount(countMes);

                Themes.Reverse();
                From.Reverse();
                Data.Reverse();

                for (int i = position; i < position + 10; i++)
                {
                    MSList.Items.Add(new LetterConstruction { Name = From[i], Description = Themes[i], Date = Data[i].Substring(0, Data[i].LastIndexOf(' ')) });
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        public void LoadTrashMessages()
        {
            MSList.Items.Clear();
            try
            {
                var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Корзина/Theme.txt").ToList();
                var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Корзина/FromWho.txt").ToList();
                var Data = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Корзина/Date.txt").ToList();

                countMes = Themes.Count;

                CheckingPagesCount(countMes);

                Themes.Reverse();
                From.Reverse();
                Data.Reverse();

                for (int i = position; i < position + 10; i++)
                {
                    MSList.Items.Add(new LetterConstruction { Name = From[i], Description = Themes[i], Date = Data[i].Substring(0, Data[i].LastIndexOf(' ')) });
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        public void LoadSpamMessages()
        {
            MSList.Items.Clear();
            try
            {
                var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/Theme.txt").ToList();
                var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/FromWho.txt").ToList();
                var Data = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/Date.txt").ToList();

                countMes = Themes.Count;

                CheckingPagesCount(countMes);

                Themes.Reverse();
                From.Reverse();
                Data.Reverse();

                for (int i = position; i < position + 10; i++)
                {
                    MSList.Items.Add(new LetterConstruction { Name = From[i], Description = Themes[i], Date = Data[i].Substring(0, Data[i].LastIndexOf(' ')) });
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        private void NPage_Click(object sender, RoutedEventArgs e)
        {
            if (countMes % 10 != 0)
            {
                if (Convert.ToInt32(IPage.Content) < countMes / 10)
                {
                    position += 10;
                    IPage.Content = Convert.ToString(Convert.ToInt32(IPage.Content) + 1);
                    LPage.IsEnabled = true;
                    if (M1.IsChecked == true)
                    {
                        LoadMessages();
                    }
                    if (M2.IsChecked == true)
                    {
                        LoadSentMessages();
                    }
                    if (M3.IsChecked == true)
                    {
                        LoadSpamMessages();
                    }
                    if (M4.IsChecked == true)
                    {
                        LoadTrashMessages();
                    }
                }
                else
                {
                    NPage.IsEnabled = false;
                }
            }
            else
            {
                if (Convert.ToInt32(IPage.Content) == countMes / 10)
                {
                    NPage.IsEnabled = false;
                }
            }
        }

        private void LPage_Click(object sender, RoutedEventArgs e)
        {
            IPage.Content = Convert.ToString(Convert.ToInt32(IPage.Content) - 1);
            NPage.IsEnabled = true;
            if (IPage.Content.ToString() == "1")
            {
                LPage.IsEnabled = false;
            }
            position -= 10;
            if (M1.IsChecked == true)
            {
                LoadMessages();
            }
            if (M2.IsChecked == true)
            {
                LoadSentMessages();
            }
            if (M3.IsChecked == true)
            {
                LoadSpamMessages();
            }
            if (M4.IsChecked == true)
            {
                LoadTrashMessages();
            }
        }

        public void ClearCash()
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите очистить кэш сообщений?", "Очистка кэша",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Theme.Clear();
                FromWho.Clear();
                Dates.Clear();
                Directory.Delete("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content, true);
                LoadMessages();
            }
        }

        private void deleteAll_Click(object sender, RoutedEventArgs e)
        {
            ClearCash();
        }

        private void deleteMS_Click(object sender, RoutedEventArgs e)
        {
            ChooseMail SW = new ChooseMail(File.ReadAllText(@"/user.txt"));
            string pochta = Convert.ToString(Chip.Content);
            try
            {
                if (M1.IsChecked == true)
                {
                    using (var _client = new ImapClient())
                    {
                        string IMapAddress = _SuffixData;
                        _client.ServerCertificateValidationCallback = (s, c, h, f) => true;
                        _client.Connect(IMapAddress, 993, true);
                        _client.Authenticate(pochta, _PWData);

                        while (MSList.SelectedItems.Count > 0)
                        {
                            var inb = _client.Inbox;
                            var tr = _client.GetFolder(SpecialFolder.Trash);

                            var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Theme.txt").ToList();
                            var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/FromWho.txt").ToList();
                            var Data = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Date.txt").ToList();
                            var ID = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/ID.txt").ToList();

                            uid = Themes.Count - ((Convert.ToInt32(IPage.Content) - 1) * 10) - MSList.SelectedIndex - 1;

                            char strelochka = '>';
                            string email = From[uid].Substring(From[uid].LastIndexOf('<') + 1, From[uid].Length - From[uid].LastIndexOf('<') - 1);
                            email = email.TrimEnd(strelochka);

                            string MessageID = "C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Письма/" + ID[uid] + ".txt";

                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress(email));
                            message.To.Add(new MailboxAddress(pochta));
                            message.Subject = Themes[uid];
                            var builder = new BodyBuilder();
                            builder.TextBody = MessageID;
                            message.Body = builder.ToMessageBody();

                            tr.Open(FolderAccess.ReadWrite);
                            tr.Append(message);
                            tr.Close();

                            inb.Open(FolderAccess.ReadWrite);

                            List<string> TThemes = new List<string>(); TThemes.Add(Themes[uid]);
                            List<string> TFrom = new List<string>(); TFrom.Add(From[uid]);
                            List<string> TData = new List<string>(); TData.Add(Data[uid]);
                            List<string> TID = new List<string>(); TID.Add(ID[uid]);

                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/Theme.txt", TThemes);
                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/FromWho.txt", TFrom);
                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/Date.txt", TData);
                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/ID.txt", TID);

                            Themes.RemoveAt(uid);
                            From.RemoveAt(uid);
                            Data.RemoveAt(uid);
                            ID.RemoveAt(uid);

                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Входящие" + "/Theme.txt", Themes);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Входящие" + "/FromWho.txt", From);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Входящие" + "/Date.txt", Data);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Входящие" + "/ID.txt", ID);

                            //MSList.Items.Clear();
                            MSList.Items.RemoveAt(MSList.SelectedIndex);
                            //LoadMessages();

                            inb.AddFlags(uid, MessageFlags.Deleted, false);
                            inb.Expunge();
                            inb.Close();
                        }
                        _client.Disconnect(true);
                        SW.LoadIncomeMessages(ForFunctions, ForFunctions);
                        LoadMessages();
                    }
                }
                if (M2.IsChecked == true)
                {
                    using (var _client = new ImapClient())
                    {
                        string IMapAddress = _SuffixData;
                        _client.ServerCertificateValidationCallback = (s, c, h, f) => true;
                        _client.Connect(IMapAddress, 993, true);
                        _client.Authenticate(pochta, _PWData);

                        while (MSList.SelectedItems.Count > 0)
                        {
                            var inb = _client.GetFolder(SpecialFolder.Sent);
                            var tr = _client.GetFolder(SpecialFolder.Trash);

                            var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/Theme.txt").ToList();
                            var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/FromWho.txt").ToList();
                            var Data = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/Date.txt").ToList();
                            var ID = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/ID.txt").ToList();

                            int uid = Themes.Count - ((Convert.ToInt32(IPage.Content) - 1) * 10) - MSList.SelectedIndex - 1;

                            char strelochka = '>';
                            string email = From[uid].Substring(From[uid].LastIndexOf('<') + 1, From[uid].Length - From[uid].LastIndexOf('<') - 1);
                            email = email.TrimEnd(strelochka);

                            string MessageID = "C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Письма/" + ID[uid] + ".txt";

                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress(email));
                            message.To.Add(new MailboxAddress(pochta));
                            message.Subject = Themes[uid];
                            var builder = new BodyBuilder();
                            builder.TextBody = MessageID;
                            message.Body = builder.ToMessageBody();

                            tr.Open(FolderAccess.ReadWrite);
                            tr.Append(message);
                            tr.Close();

                            inb.Open(FolderAccess.ReadWrite);

                            List<string> TThemes = new List<string>(); TThemes.Add(Themes[uid]);
                            List<string> TFrom = new List<string>(); TFrom.Add(From[uid]);
                            List<string> TData = new List<string>(); TData.Add(Data[uid]);
                            List<string> TID = new List<string>(); TID.Add(ID[uid]);

                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/Theme.txt", TThemes);
                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/FromWho.txt", TFrom);
                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/Date.txt", TData);
                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/ID.txt", TID);

                            Themes.RemoveAt(uid);
                            From.RemoveAt(uid);
                            Data.RemoveAt(uid);
                            ID.RemoveAt(uid);

                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Исходящие" + "/Theme.txt", Themes);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Исходящие" + "/FromWho.txt", From);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Исходящие" + "/Date.txt", Data);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Исходящие" + "/ID.txt", ID);

                            //MSList.Items.Clear();
                            MSList.Items.RemoveAt(MSList.SelectedIndex);
                            //LoadSentMessages();

                            inb.AddFlags(uid, MessageFlags.Deleted, false);
                            inb.Expunge();
                            inb.Close();
                        }
                        _client.Disconnect(true);
                        SW.LoadSentMessages(ForFunctions, ForFunctions);
                        LoadSentMessages();
                    }
                }
                if (M3.IsChecked == true)
                {
                    using (var _client = new ImapClient())
                    {
                        string IMapAddress = _SuffixData;
                        _client.ServerCertificateValidationCallback = (s, c, h, f) => true;
                        _client.Connect(IMapAddress, 993, true);
                        _client.Authenticate(pochta, _PWData);

                        while (MSList.SelectedItems.Count > 0)
                        {
                            var inb = _client.GetFolder(SpecialFolder.Junk);
                            var tr = _client.GetFolder(SpecialFolder.Trash);

                            var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/Theme.txt").ToList();
                            var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/FromWho.txt").ToList();
                            var Data = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/Date.txt").ToList();
                            var ID = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/ID.txt").ToList();

                            int uid = Themes.Count - ((Convert.ToInt32(IPage.Content) - 1) * 10) - MSList.SelectedIndex - 1;

                            char strelochka = '>';
                            string email = From[uid].Substring(From[uid].LastIndexOf('<') + 1, From[uid].Length - From[uid].LastIndexOf('<') - 1);
                            email = email.TrimEnd(strelochka);

                            string MessageID = "C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Письма/" + ID[uid] + ".txt";

                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress(email));
                            message.To.Add(new MailboxAddress(pochta));
                            message.Subject = Themes[uid];
                            var builder = new BodyBuilder();
                            builder.TextBody = MessageID;
                            message.Body = builder.ToMessageBody();

                            tr.Open(FolderAccess.ReadWrite);
                            tr.Append(message);
                            tr.Close();

                            inb.Open(FolderAccess.ReadWrite);

                            List<string> TThemes = new List<string>(); TThemes.Add(Themes[uid]);
                            List<string> TFrom = new List<string>(); TFrom.Add(From[uid]);
                            List<string> TData = new List<string>(); TData.Add(Data[uid]);
                            List<string> TID = new List<string>(); TID.Add(ID[uid]);

                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/Theme.txt", TThemes);
                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/FromWho.txt", TFrom);
                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/Date.txt", TData);
                            File.AppendAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/ID.txt", TID);

                            Themes.RemoveAt(uid);
                            From.RemoveAt(uid);
                            Data.RemoveAt(uid);
                            ID.RemoveAt(uid);

                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Спам" + "/Theme.txt", Themes);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Спам" + "/FromWho.txt", From);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Спам" + "/Date.txt", Data);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Спам" + "/ID.txt", ID);

                            //MSList.Items.Clear();
                            MSList.Items.RemoveAt(MSList.SelectedIndex);
                            //LoadSpamMessages();

                            inb.AddFlags(uid, MessageFlags.Deleted, false);
                            inb.Expunge();
                            inb.Close();
                        }
                        _client.Disconnect(true);
                        SW.LoadSpamMessages(ForFunctions, ForFunctions);
                        LoadSpamMessages();
                    }
                }
                if (M4.IsChecked == true)
                {
                    using (var _client = new ImapClient())
                    {
                        string IMapAddress = _SuffixData;
                        _client.ServerCertificateValidationCallback = (s, c, h, f) => true;
                        _client.Connect(IMapAddress, 993, true);
                        _client.Authenticate(pochta, _PWData);

                        while (MSList.SelectedItems.Count > 0)
                        {
                            var inb = _client.GetFolder(SpecialFolder.Trash);

                            var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Корзина/Theme.txt").ToList();
                            var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Корзина/FromWho.txt").ToList();
                            var Data = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Корзина/Date.txt").ToList();
                            var ID = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Корзина/ID.txt").ToList();

                            int uid = Themes.Count - ((Convert.ToInt32(IPage.Content) - 1) * 10) - MSList.SelectedIndex - 1;

                            inb.Open(FolderAccess.ReadWrite);

                            Themes.RemoveAt(uid);
                            From.RemoveAt(uid);
                            Data.RemoveAt(uid);
                            ID.RemoveAt(uid);

                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/Theme.txt", Themes);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/FromWho.txt", From);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/Date.txt", Data);
                            File.WriteAllLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/" + "Корзина" + "/ID.txt", ID);

                            //MSList.Items.Clear();
                            MSList.Items.RemoveAt(MSList.SelectedIndex);
                            //LoadTrashMessages();

                            inb.AddFlags(uid, MessageFlags.Deleted, false);
                            inb.Expunge();
                            inb.Close();
                        }
                        _client.Disconnect(true);
                        SW.LoadTrashMessages(ForFunctions, ForFunctions);
                        LoadTrashMessages();
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void renew_Click(object sender, RoutedEventArgs e)
        {
            ChooseMail SW = new ChooseMail(File.ReadAllText(@"/user.txt"));
            if (M1.IsChecked == true)
            {
                SW.LoadIncomeMessages(ForFunctions, ForFunctions);
                LoadMessages();
            }
            if (M2.IsChecked == true)
            {
                SW.LoadSentMessages(ForFunctions, ForFunctions);
                LoadSentMessages();
            }
            if (M3.IsChecked == true)
            {
                SW.LoadSpamMessages(ForFunctions, ForFunctions);
                LoadSpamMessages();
            }
            if (M4.IsChecked == true)
            {
                SW.LoadTrashMessages(ForFunctions, ForFunctions);
                LoadTrashMessages();
            }
        }

        private void MSList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //List<string> Themes = new List<string>();
            //List<string> From = new List<string>();
            //string to = ForFunctions;

            //if (M1.IsChecked == true)
            //{
            //    var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Theme.txt").ToList();
            //    var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/FromWho.txt").ToList();
            //    string theme = Themes[MSList.SelectedIndex - 1];
            //    string from = From[MSList.SelectedIndex- 1];
            //    to = ForFunctions;
            //    MessageBox.Show(theme + " " + from + " " + to);
            //    MessageView MV = new MessageView(theme, from, to);
            //    MV.Show();
            //}
            //if (M2.IsChecked == true)
            //{
            //    Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/Theme.txt").ToList();
            //    From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/FromWho.txt").ToList();
            //    //to = ForFunctions;
            //}
            //if (M3.IsChecked == true)
            //{
            //    Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/Theme.txt").ToList();
            //    From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/FromWho.txt").ToList();
            //    to = ForFunctions;
            //}
            //if (M4.IsChecked == true)
            //{
            //    Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Корзина/Theme.txt").ToList();
            //    From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Корзина/FromWho.txt").ToList();
            //    //to = ForFunctions;
            //}
            //string theme = Themes[MSList.SelectedIndex];
            //string from = From[MSList.SelectedIndex];
            //MessageBox.Show(theme + " " + from + " " + to);
            //MessageView MV = new MessageView(theme, from, to);
            //MV.Show();
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            ChooseMail CM = new ChooseMail(File.ReadAllText(@"/user.txt"));
            CM.Show();
            this.Close();
        }

        private void to_faw_Click(object sender, RoutedEventArgs e)
        {
            string path = "C:/KursachMailClient/" + _EMailData + "/" + Chip.Content;
            if (MSList.SelectedItems.Count == 1)
            {
                if (M1.IsChecked == true)
                {
                    var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Theme.txt").ToList();
                    var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/FromWho.txt").ToList();
                    var Date = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Date.txt").ToList();
                    Themes.Reverse();
                    From.Reverse();
                    Date.Reverse();
                    string date = Date[MSList.SelectedIndex];
                    string theme = Themes[MSList.SelectedIndex];
                    string to = ForFunctions;
                    string from = From[MSList.SelectedIndex];
                    int box = 0;
                    uid = Themes.Count - ((Convert.ToInt32(IPage.Content) - 1) * 10) - MSList.SelectedIndex - 1;
                    if (M1.IsChecked == true)
                    {
                        box = 1;
                    }
                    if (M2.IsChecked == true)
                    {
                        box = 2;
                    }
                    if (M3.IsChecked == true)
                    {
                        box = 3;
                    }
                    if (M4.IsChecked == true)
                    {
                        box = 4;
                    }
                    MessageView MV = new MessageView(theme, from, to, date, box, uid, path, password);
                    MV.Show();
                }
                if (M2.IsChecked == true)
                {
                    var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/Theme.txt").ToList();
                    var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/FromWho.txt").ToList();
                    var Date = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Исходящие/Date.txt").ToList();
                    Themes.Reverse();
                    From.Reverse();
                    Date.Reverse();
                    string date = Date[MSList.SelectedIndex];
                    string theme = Themes[MSList.SelectedIndex];
                    string to = From[MSList.SelectedIndex];
                    string from = ForFunctions;
                    int box = 0;
                    uid = Themes.Count - ((Convert.ToInt32(IPage.Content) - 1) * 10) - MSList.SelectedIndex - 1;
                    if (M1.IsChecked == true)
                    {
                        box = 1;
                    }
                    if (M2.IsChecked == true)
                    {
                        box = 2;
                    }
                    if (M3.IsChecked == true)
                    {
                        box = 3;
                    }
                    if (M4.IsChecked == true)
                    {
                        box = 4;
                    }
                    MessageView MV = new MessageView(theme, from, to, date, box, uid, path, password);
                    MV.Show();
                }
                if (M3.IsChecked == true)
                {
                    var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/Theme.txt").ToList();
                    var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/FromWho.txt").ToList();
                    var Date = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Спам/Date.txt").ToList();
                    Themes.Reverse();
                    From.Reverse();
                    Date.Reverse();
                    string date = Date[MSList.SelectedIndex];
                    string theme = Themes[MSList.SelectedIndex - 1];
                    string from = From[MSList.SelectedIndex - 1];
                    string to = ForFunctions;
                    int box = 0;
                    uid = Themes.Count - ((Convert.ToInt32(IPage.Content) - 1) * 10) - MSList.SelectedIndex - 1;
                    if (M1.IsChecked == true)
                    {
                        box = 1;
                    }
                    if (M2.IsChecked == true)
                    {
                        box = 2;
                    }
                    if (M3.IsChecked == true)
                    {
                        box = 3;
                    }
                    if (M4.IsChecked == true)
                    {
                        box = 4;
                    }
                    MessageView MV = new MessageView(theme, from, to, date, box, uid, path, password);
                    MV.Show();
                }
                if (M4.IsChecked == true)
                {
                    //var Themes = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/Theme.txt").ToList();
                    //var From = File.ReadLines("C:/KursachMailClient/" + _EMailData + "/" + Chip.Content + "/Входящие/FromWho.txt").ToList();
                    //string theme = Themes[MSList.SelectedIndex - 1];
                    //string from = From[MSList.SelectedIndex - 1];
                    //MessageBox.Show(theme + " " + from + " " + to);
                    //MessageView MV = new MessageView(theme, from, to, date);
                    //MV.Show();
                }
            }
            else
            {
                MessageBox.Show("Нужно выбрать одно сообщение");
            }
        }
    }
}

