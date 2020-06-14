using MailClient.Source.MailBox;
using MailClient.Source.Operations;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MailClient
{
    public partial class ChooseMail : Window
    {
        string Login, _Length;

        private string PWData, EMailData, SuffixData;
        string Password;

        int start_letter = 0, count_back = 0, last_index;
        public int CUnread;

        int IMapPort = 993;
        GetMailSuffix getMailSuffix = new GetMailSuffix();


        public DispatcherTimer timer = new DispatcherTimer();
        List<string> FromWho = new List<string>();
        List<string> HTML = new List<string>();
        List<string> Theme = new List<string>();
        List<string> Dates = new List<string>();
        List<string> ID = new List<string>();

        public ChooseMail(string log)
        {
            InitializeComponent();
            Login = log;
            Load();
            if (!File.Exists("C:/KursachMailClient/" + Login + "/Boxes.txt"))
            {
                File.Create("C:/KursachMailClient/" + Login + "/Boxes.txt");
            }
            if (!File.Exists("C:/KursachMailClient/" + Login + "/Data.txt"))
            {
                File.Create("C:/KursachMailClient/" + Login + "/Data.txt");
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (MSList.SelectedItems.Count > 1)
            {
                enter.IsEnabled = false;
            }
            else
            {
                enter.IsEnabled = true;
            }
        }

        public void Load()
        {
            LoadBoxes.Load(this, Login);
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0);
            timer.Start();
        }

        private void deleteMS_Click(object sender, RoutedEventArgs e)
        {
            AddMailToList ADD = new AddMailToList(Login);
            ADD.Show();
            this.Close();
        }

        public void ClearLists()
        {
            FromWho.Clear();
            Theme.Clear();
            Dates.Clear();
            ID.Clear();
            HTML.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Mails = File.ReadAllLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
            var PW = File.ReadAllLines("C:/KursachMailClient/" + Login + "/Data.txt").ToList();
            Password = PW[MSList.SelectedIndex];
            _Length = Convert.ToString(Mails[MSList.SelectedIndex]);
            LoadBoxes.CheckLettersData(this, Login);
            try
            {
                using (var client = new ImapClient())
                {
                    string IMapAddress = "imap." + getMailSuffix.GetSuffix(_Length);
                    client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                    client.Connect(IMapAddress, IMapPort, true);
                    client.Authenticate(Mails[MSList.SelectedIndex], Password);
                    LoadIncomeMessages(Mails[MSList.SelectedIndex], _Length);
                    LoadSentMessages(Mails[MSList.SelectedIndex], _Length);
                    LoadSpamMessages(Mails[MSList.SelectedIndex], _Length);
                    LoadTrashMessages(Mails[MSList.SelectedIndex], _Length);
                    PWData = Password;
                    EMailData = Login;
                    SuffixData = IMapAddress;
                    LoadBoxes.Chip(this, Login);
                }
                MainWindow main = new MainWindow(PWData, EMailData, SuffixData, Password);
                main.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                File.Delete(@"/temp.txt");
                timer.Stop();
            }
        }

        public void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Выход",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void MSList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var PW = File.ReadLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
                Password = PW[MSList.SelectedIndex];
            }
            catch
            {

            }
        }

        public void LoadIncomeMessages(string email, string suf)
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
            int ind = Mails.IndexOf(email);
            var PW = File.ReadLines("C:/KursachMailClient/" + Login + "/Data.txt").ToList();
            Password = PW[ind];
            //var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            using (var client = new ImapClient())
            {
                string IMapAddress = "imap." + getMailSuffix.GetSuffix(suf);
                client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                client.Connect(IMapAddress, IMapPort, true);
                client.Authenticate(email, Password);

                if (start_letter < 0) start_letter = 0;
                last_index = start_letter;

                var inbox = client.Inbox;

                inbox.Open(FolderAccess.ReadWrite);

                var unread = client.Inbox.Search(SearchQuery.NotSeen);
                CUnread = unread.Count;

                File.WriteAllText(@"/count.txt", Convert.ToString(CUnread));

                int count = inbox.Count;

                var ListID = File.ReadAllLines("C:/KursachMailClient/" + Login + "/" + email
                    + "/" + "Входящие" + "/ID.txt").ToList();

                if (start_letter + count <= inbox.Count)
                {
                    for (int i = ListID.Count; i < count; i++)
                    {
                        var message = inbox.GetMessage(i);

                        if (message.Subject != "")
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                                //message.WriteTo("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + 
                                //    "Входящие/Письма/" + message.MessageId + ".txt");
                            }
                        }
                        else
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                                //message.WriteTo("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" +
                                //   "Входящие/Письма/" + message.MessageId + ".txt");
                            }
                        }
                    }
                    start_letter = count + start_letter;
                }
                else
                {
                    for (int i = ListID.Count; i < count; i++)
                    {
                        var message = inbox.GetMessage(i);

                        if (message.Subject != "")
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                                //message.WriteTo("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" +
                                   //"Входящие/Письма/" + message.MessageId + ".txt");
                            }
                        }
                        else
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                                //message.WriteTo("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" +
                                //   "Входящие/Письма/" + message.MessageId + ".txt");
                            }
                        }
                    }
                    start_letter = inbox.Count - start_letter;
                }

                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Входящие" + "/Theme.txt", Theme);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Входящие" + "/FromWho.txt", FromWho);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Входящие" + "/Date.txt", Dates);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Входящие" + "/ID.txt", ID);

                string Temp;
                for (int i = 0; i < HTML.Count; i++)
                {
                    Temp = (HTML[i]);
                    //File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt", Temp);
                    using (var sw = new StreamWriter("C:/KursachMailClient/" + Login + "/" + email + "/" + "Входящие/Письма/" + ID[i] + ".txt",
                        true, System.Text.Encoding.ASCII))
                    {
                        sw.Write(Temp);
                    }
                    Temp = "";
                }

                start_letter = 0;
                count_back = 0;
                last_index = 0;
                ClearLists();
                ListID.Clear();
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                while (MSList.SelectedItems.Count > 0)
                {
                    var Mails = File.ReadLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
                    var PW = File.ReadLines("C:/KursachMailClient/" + Login + "/Data.txt").ToList();
                    Mails.RemoveAt(MSList.SelectedIndex);
                    PW.RemoveAt(MSList.SelectedIndex);
                    File.WriteAllLines("C:/KursachMailClient/" + Login + "/Boxes.txt", Mails);
                    File.WriteAllLines("C:/KursachMailClient/" + Login + "/Data.txt", PW);
                    MSList.Items.RemoveAt(MSList.SelectedIndex);
                }
                Load();
            }
            catch
            {

            }
        }

        public void LoadSentMessages(string email, string suf)
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
            int ind = Mails.IndexOf(email);
            var PW = File.ReadLines("C:/KursachMailClient/" + Login + "/Data.txt").ToList();
            Password = PW[ind];
            //var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            using (var client = new ImapClient())
            {
                string IMapAddress = "imap." + getMailSuffix.GetSuffix(suf);
                client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                client.Connect(IMapAddress, IMapPort, true);
                client.Authenticate(email, Password);

                if (start_letter < 0) start_letter = 0;
                last_index = start_letter;

                var inbox = client.GetFolder(SpecialFolder.Sent);

                inbox.Open(FolderAccess.ReadWrite);

                int count = inbox.Count;

                var ListID = File.ReadAllLines("C:/KursachMailClient/" + Login + "/" + email
                    + "/" + "Исходящие" + "/ID.txt").ToList();

                if (start_letter + count <= inbox.Count)
                {
                    for (int i = ListID.Count; i < count; i++)
                    {
                        var message = inbox.GetMessage(i);

                        if (message.Subject != "")
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.To.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                        else
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.To.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                    }
                    start_letter = count + start_letter;
                }
                else
                {
                    for (int i = ListID.Count; i < count; i++)
                    {
                        var message = inbox.GetMessage(i);

                        if (message.Subject != "")
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.To.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                        else
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.To.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                    }
                    start_letter = inbox.Count - start_letter;
                }

                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Исходящие" + "/Theme.txt", Theme);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Исходящие" + "/FromWho.txt", FromWho);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Исходящие" + "/Date.txt", Dates);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Исходящие" + "/ID.txt", ID);

                //List<string> Temp = new List<string>();

                string Temp;
                for (int i = 0; i < HTML.Count; i++)
                {
                    Temp = (HTML[i]);
                    //File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt", Temp);
                    using (var sw = new StreamWriter("C:/KursachMailClient/" + Login + "/" + email + "/" + "Исходящие/Письма/" + ID[i] + ".txt",
                        true, System.Text.Encoding.ASCII))
                    {
                        sw.Write(Temp);
                    }
                    Temp = "";
                }

                start_letter = 0;
                count_back = 0;
                last_index = 0;
                ClearLists();
                ListID.Clear();
            }
        }

        public void LoadSpamMessages(string email, string suf)
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
            int ind = Mails.IndexOf(email);
            var PW = File.ReadLines("C:/KursachMailClient/" + Login + "/Data.txt").ToList();
            Password = PW[ind];
            //var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            using (var client = new ImapClient())
            {
                string IMapAddress = "imap." + getMailSuffix.GetSuffix(suf);
                client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                client.Connect(IMapAddress, IMapPort, true);
                client.Authenticate(email, Password);

                if (start_letter < 0) start_letter = 0;
                last_index = start_letter;

                var inbox = client.GetFolder(SpecialFolder.Junk);

                inbox.Open(FolderAccess.ReadWrite);

                int count = inbox.Count;

                var ListID = File.ReadAllLines("C:/KursachMailClient/" + Login + "/" + email
                    + "/" + "Спам" + "/ID.txt").ToList();

                if (start_letter + count <= inbox.Count)
                {
                    for (int i = ListID.Count; i < count; i++)
                    {
                        var message = inbox.GetMessage(i);

                        if (message.Subject != "")
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                        else
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                    }
                    start_letter = count + start_letter;
                }
                else
                {
                    for (int i = ListID.Count; i < count; i++)
                    {
                        var message = inbox.GetMessage(i);

                        if (message.Subject != "")
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                        else
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                    }
                    start_letter = inbox.Count - start_letter;
                }

                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Спам" + "/Theme.txt", Theme);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Спам" + "/FromWho.txt", FromWho);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Спам" + "/Date.txt", Dates);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Спам" + "/ID.txt", ID);

                string Temp;
                for (int i = 0; i < HTML.Count; i++)
                {
                    Temp = (HTML[i]);
                    //File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt", Temp);
                    using (var sw = new StreamWriter("C:/KursachMailClient/" + Login + "/" + email + "/" + "Спам/Письма/" + ID[i] + ".txt",
                        true, System.Text.Encoding.ASCII))
                    {
                        sw.Write(Temp);
                    }
                    Temp = "";
                }

                start_letter = 0;
                count_back = 0;
                last_index = 0;
                ClearLists();
                ListID.Clear();
            }
        }

        public void LoadTrashMessages(string email, string suf)
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
            int ind = Mails.IndexOf(email);
            var PW = File.ReadLines("C:/KursachMailClient/" + Login + "/Data.txt").ToList();
            Password = PW[ind];

            using (var client = new ImapClient())
            {
                string IMapAddress = "imap." + getMailSuffix.GetSuffix(suf);
                client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                client.Connect(IMapAddress, IMapPort, true);
                client.Authenticate(email, Password);

                if (start_letter < 0) start_letter = 0;
                last_index = start_letter;

                var inbox = client.GetFolder(SpecialFolder.Trash);

                inbox.Open(FolderAccess.ReadWrite);

                int count = inbox.Count;

                var ListID = File.ReadAllLines("C:/KursachMailClient/" + Login + "/" + email
                    + "/" + "Корзина" + "/ID.txt").ToList();

                if (start_letter + count <= inbox.Count)
                {
                    for (int i = ListID.Count; i < count; i++)
                    {
                        var message = inbox.GetMessage(i);

                        if (message.Subject != "")
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                        else
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                    }
                    start_letter = count + start_letter;
                }
                else
                {
                    for (int i = ListID.Count; i < count; i++)
                    {
                        var message = inbox.GetMessage(i);

                        if (message.Subject != "")
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                        else
                        {
                            if (!ListID.Contains(Convert.ToString(message.MessageId)))
                            {
                                Theme.Add(message.Subject);
                                FromWho.Add(message.From.ToString());
                                Dates.Add(Convert.ToString(message.Date));
                                ID.Add(Convert.ToString(message.MessageId));
                                HTML.Add(message.Body.ToString());
                            }
                        }
                    }
                    start_letter = inbox.Count - start_letter;
                }

                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Корзина" + "/Theme.txt", Theme);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Корзина" + "/FromWho.txt", FromWho);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Корзина" + "/Date.txt", Dates);
                File.AppendAllLines("C:/KursachMailClient/" + Login + "/" + email + "/" + "Корзина" + "/ID.txt", ID);


                // List<string> Temp = new List<string>();
                string Temp;
                for (int i = 0; i < HTML.Count; i++)
                {
                    Temp = (HTML[i]);
                    //File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt", Temp);
                    using (var sw = new StreamWriter("C:/KursachMailClient/" + Login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt",
                        true, System.Text.Encoding.ASCII))
                    {
                        sw.Write(Temp);
                    }
                    Temp = "";
                }

                start_letter = 0;
                count_back = 0;
                last_index = 0;
                ClearLists();
                ListID.Clear();
            }
        }
    }
}
