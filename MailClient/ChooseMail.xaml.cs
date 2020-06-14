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
    /// <summary>
    /// Логика взаимодействия для ChooseMail.xaml
    /// </summary>
    public partial class ChooseMail : Window
    {
        string login, len;

        private string PWData;
        private string EMailData;
        private string SuffixData;
        string psw;

        int start_letter = 0, count_back = 0, last_index;
        public int CUnread;

        int IMapPort = 993;

        DispatcherTimer timer = new DispatcherTimer();
        List<string> FromWho = new List<string>();
        List<string> HTML = new List<string>();
        List<string> Theme = new List<string>();
        List<string> Dates = new List<string>();
        List<string> ID = new List<string>();

        public ChooseMail(string log)
        {
            InitializeComponent();
            login = log;
            Load();
            if (!File.Exists("C:/KursachMailClient/" + login + "/Boxes.txt"))
            {
                File.Create("C:/KursachMailClient/" + login + "/Boxes.txt");
            }
            if (!File.Exists("C:/KursachMailClient/" + login + "/Data.txt"))
            {
                File.Create("C:/KursachMailClient/" + login + "/Data.txt");
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
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
            if (File.Exists("C:/KursachMailClient/" + login + "/Boxes.txt") == true)
            {
                var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
                for (int i = 0; i < Mails.Count; i++)
                {
                    MSList.Items.Add(new MyItem { Name = Mails[i] });
                }
            }

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0);
            timer.Start();
        }

        private void deleteMS_Click(object sender, RoutedEventArgs e)
        {
            AddMailToList ADD = new AddMailToList(login);
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

        public void chip()
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            File.WriteAllText(@"/temp.txt", Convert.ToString(Convert.ToString(Mails[MSList.SelectedIndex])));
        }

        public void CreateID()
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            {
                if (!File.Exists("C:/KursachMailClient/" + login + "/" + Mails[MSList.SelectedIndex] + "/" + "Входящие" + "/ID.txt"))
                {
                    File.Create("C:/KursachMailClient/" + login + "/" + Mails[MSList.SelectedIndex] + "/" + "Входящие" + "/ID.txt");
                }
                if (!File.Exists("C:/KursachMailClient/" + login + "/" + Mails[MSList.SelectedIndex] + "/" + "Исходящие" + "/ID.txt"))
                {
                    File.Create("C:/KursachMailClient/" + login + "/" + Mails[MSList.SelectedIndex] + "/" + "Исходящие" + "/ID.txt");
                }
                if (!File.Exists("C:/KursachMailClient/" + login + "/" + Mails[MSList.SelectedIndex] + "/" + "Спам" + "/ID.txt"))
                {
                    File.Create("C:/KursachMailClient/" + login + "/" + Mails[MSList.SelectedIndex] + "/" + "Спам" + "/ID.txt");
                }
                if (!File.Exists("C:/KursachMailClient/" + login + "/" + Mails[MSList.SelectedIndex] + "/" + "Корзина" + "/ID.txt"))
                {
                    File.Create("C:/KursachMailClient/" + login + "/" + Mails[MSList.SelectedIndex] + "/" + "Корзина" + "/ID.txt");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Mails = File.ReadAllLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            var PW = File.ReadAllLines("C:/KursachMailClient/" + login + "/Data.txt").ToList();
            psw = PW[MSList.SelectedIndex];
            len = Convert.ToString(Mails[MSList.SelectedIndex]);
            CreateDirectories();
            CreateID();
            try
            {
                using (var client = new ImapClient())
                {
                    string IMapAddress = "imap." + getSuffix(len);
                    client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                    client.Connect(IMapAddress, IMapPort, true);
                    client.Authenticate(Mails[MSList.SelectedIndex], psw);
                    LoadIncomeMessages(Mails[MSList.SelectedIndex], len);
                    LoadSentMessages(Mails[MSList.SelectedIndex], len);
                    LoadSpamMessages(Mails[MSList.SelectedIndex], len);
                    LoadTrashMessages(Mails[MSList.SelectedIndex], len);
                    PWData = psw;
                    EMailData = login;
                    SuffixData = IMapAddress;
                    chip();
                }
                MainWindow main = new MainWindow(PWData, EMailData, SuffixData, psw);
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

        public string getSuffix(string emails)
        {
            string email = emails.Substring(emails.LastIndexOf('@') + 1, emails.Length - (emails.LastIndexOf('@') + 1));
            return email;
        }

        private void MSList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var PW = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
                psw = PW[MSList.SelectedIndex];
            }
            catch
            {

            }
        }

        public void CreateDirectories()
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();

            if (!Directory.Exists("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex])))
            {
                Directory.CreateDirectory("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]));
            }
            if (!Directory.Exists("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Входящие"))
            {
                Directory.CreateDirectory("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Входящие");
            }
            if (!Directory.Exists("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Входящие/Письма"))
            {
                Directory.CreateDirectory("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Входящие/Письма");
            }
            if (!Directory.Exists("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Исходящие"))
            {
                Directory.CreateDirectory("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Исходящие");
            }
            if (!Directory.Exists("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Исходящие/Письма"))
            {
                Directory.CreateDirectory("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Исходящие/Письма");
            }
            if (!Directory.Exists("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Спам"))
            {
                Directory.CreateDirectory("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Спам");
            }
            if (!Directory.Exists("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Спам/Письма"))
            {
                Directory.CreateDirectory("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Спам/Письма");
            }
            if (!Directory.Exists("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Корзина"))
            {
                Directory.CreateDirectory("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Корзина");
            }
            if (!Directory.Exists("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Корзина/Письма"))
            {
                Directory.CreateDirectory("C:/KursachMailClient/" + login + "/" + Convert.ToString(Mails[MSList.SelectedIndex]) + "/" + "Корзина/Письма");
            }
        }

        public void LoadIncomeMessages(string email, string suf)
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            int ind = Mails.IndexOf(email);
            var PW = File.ReadLines("C:/KursachMailClient/" + login + "/Data.txt").ToList();
            psw = PW[ind];
            //var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            using (var client = new ImapClient())
            {
                string IMapAddress = "imap." + getSuffix(suf);
                client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                client.Connect(IMapAddress, IMapPort, true);
                client.Authenticate(email, psw);

                if (start_letter < 0) start_letter = 0;
                last_index = start_letter;

                var inbox = client.Inbox;

                inbox.Open(FolderAccess.ReadWrite);

                var unread = client.Inbox.Search(SearchQuery.NotSeen);
                CUnread = unread.Count;

                File.WriteAllText(@"/count.txt", Convert.ToString(CUnread));

                int count = inbox.Count;

                var ListID = File.ReadAllLines("C:/KursachMailClient/" + login + "/" + email
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

                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Входящие" + "/Theme.txt", Theme);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Входящие" + "/FromWho.txt", FromWho);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Входящие" + "/Date.txt", Dates);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Входящие" + "/ID.txt", ID);

                string Temp;
                for (int i = 0; i < HTML.Count; i++)
                {
                    Temp = (HTML[i]);
                    //File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt", Temp);
                    using (var sw = new StreamWriter("C:/KursachMailClient/" + login + "/" + email + "/" + "Входящие/Письма/" + ID[i] + ".txt",
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
                    var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
                    var PW = File.ReadLines("C:/KursachMailClient/" + login + "/Data.txt").ToList();
                    Mails.RemoveAt(MSList.SelectedIndex);
                    PW.RemoveAt(MSList.SelectedIndex);
                    File.WriteAllLines("C:/KursachMailClient/" + login + "/Boxes.txt", Mails);
                    File.WriteAllLines("C:/KursachMailClient/" + login + "/Data.txt", PW);
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
            var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            int ind = Mails.IndexOf(email);
            var PW = File.ReadLines("C:/KursachMailClient/" + login + "/Data.txt").ToList();
            psw = PW[ind];
            //var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            using (var client = new ImapClient())
            {
                string IMapAddress = "imap." + getSuffix(suf);
                client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                client.Connect(IMapAddress, IMapPort, true);
                client.Authenticate(email, psw);

                if (start_letter < 0) start_letter = 0;
                last_index = start_letter;

                var inbox = client.GetFolder(SpecialFolder.Sent);

                inbox.Open(FolderAccess.ReadWrite);

                int count = inbox.Count;

                var ListID = File.ReadAllLines("C:/KursachMailClient/" + login + "/" + email
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

                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Исходящие" + "/Theme.txt", Theme);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Исходящие" + "/FromWho.txt", FromWho);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Исходящие" + "/Date.txt", Dates);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Исходящие" + "/ID.txt", ID);

                //List<string> Temp = new List<string>();

                string Temp;
                for (int i = 0; i < HTML.Count; i++)
                {
                    Temp = (HTML[i]);
                    //File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt", Temp);
                    using (var sw = new StreamWriter("C:/KursachMailClient/" + login + "/" + email + "/" + "Исходящие/Письма/" + ID[i] + ".txt",
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
            var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            int ind = Mails.IndexOf(email);
            var PW = File.ReadLines("C:/KursachMailClient/" + login + "/Data.txt").ToList();
            psw = PW[ind];
            //var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            using (var client = new ImapClient())
            {
                string IMapAddress = "imap." + getSuffix(suf);
                client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                client.Connect(IMapAddress, IMapPort, true);
                client.Authenticate(email, psw);

                if (start_letter < 0) start_letter = 0;
                last_index = start_letter;

                var inbox = client.GetFolder(SpecialFolder.Junk);

                inbox.Open(FolderAccess.ReadWrite);

                int count = inbox.Count;

                var ListID = File.ReadAllLines("C:/KursachMailClient/" + login + "/" + email
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

                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Спам" + "/Theme.txt", Theme);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Спам" + "/FromWho.txt", FromWho);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Спам" + "/Date.txt", Dates);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Спам" + "/ID.txt", ID);

                string Temp;
                for (int i = 0; i < HTML.Count; i++)
                {
                    Temp = (HTML[i]);
                    //File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt", Temp);
                    using (var sw = new StreamWriter("C:/KursachMailClient/" + login + "/" + email + "/" + "Спам/Письма/" + ID[i] + ".txt",
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
            var Mails = File.ReadLines("C:/KursachMailClient/" + login + "/Boxes.txt").ToList();
            int ind = Mails.IndexOf(email);
            var PW = File.ReadLines("C:/KursachMailClient/" + login + "/Data.txt").ToList();
            psw = PW[ind];

            using (var client = new ImapClient())
            {
                string IMapAddress = "imap." + getSuffix(suf);
                client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                client.Connect(IMapAddress, IMapPort, true);
                client.Authenticate(email, psw);

                if (start_letter < 0) start_letter = 0;
                last_index = start_letter;

                var inbox = client.GetFolder(SpecialFolder.Trash);

                inbox.Open(FolderAccess.ReadWrite);

                int count = inbox.Count;

                var ListID = File.ReadAllLines("C:/KursachMailClient/" + login + "/" + email
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

                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина" + "/Theme.txt", Theme);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина" + "/FromWho.txt", FromWho);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина" + "/Date.txt", Dates);
                File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина" + "/ID.txt", ID);


                // List<string> Temp = new List<string>();
                string Temp;
                for (int i = 0; i < HTML.Count; i++)
                {
                    Temp = (HTML[i]);
                    //File.AppendAllLines("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt", Temp);
                    using (var sw = new StreamWriter("C:/KursachMailClient/" + login + "/" + email + "/" + "Корзина/Письма/" + ID[i] + ".txt",
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
