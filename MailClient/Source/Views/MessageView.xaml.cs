using System;
using System.Web;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MailKit.Net.Smtp;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MailClient
{
    public partial class MessageView : Window
    {
        int WBox, MesUid;
        string ToBox, psw;
        List<string> Files = new List<string>();
        string temp = "";
        string Decr, sup, sup1, sup2, united_str;
        const string delimiter = "^&*";
        public string path = "C:/KursachMailClient/";
        string user = File.ReadAllText(@"/user.txt");

        private byte[] key = null;
        private byte[] iv = null;
        private RSAParameters? signaturePrivKey = null;
        private RSAParameters? signaturePubKey = null;

        Crypto crypto = new Crypto();

        public MessageView(string theme, string from, string to, string date, int box, int uid, string path, string password)
        {
            InitializeComponent();
            IsTheme.Text = theme;
            Chip.Content = from;
            Chip.Icon = Convert.ToString(Chip.Content).Substring(0, 1).ToUpper();
            Chip2.Content = to;
            Chip2.Icon = Convert.ToString(Chip2.Content).Substring(0, 1).ToUpper();
            dateTime.Content = date.Substring(0, date.LastIndexOf(' '));
            WBox = box;
            MesUid = uid;
            ToBox = path;
            psw = password;
            LoadMessageBody();
        }

        private void LoadMessageBody()
        {
            try
            {
                string MessageID;
                if (WBox == 1)
                {
                    try
                    {
                        LoadAttachments();

                        var ID = File.ReadLines(ToBox + "/Входящие/ID.txt").ToList();
                        MessageID = ID[MesUid] + ".txt";

                        if (attachment.Items.Count > 0)
                        {
                            sup = File.ReadAllLines(ToBox + "/Входящие/Письма/" + MessageID).Skip(5).First();
                        }
                        else
                        {
                            sup = File.ReadAllLines(ToBox + "/Входящие/Письма/" + MessageID).Skip(2).First();
                        }

                        string textForOutput = sup;
                        string[] temp = textForOutput.Split(new string[] { "^&*" }, StringSplitOptions.None);
                        string prKey = File.ReadAllText(path + user + "/Private.txt");

                        temp[1] = crypto.Decrypt(temp[1], prKey);
                        string DecryptText = "";

                        for (int i = 0; i < temp.Length; i++)    /*Формируем конечную строку*/
                            if (i < temp.Length - 1)
                                DecryptText += $"{temp[i]}^&*";
                            else
                                DecryptText += temp[i];

                        textForOutput = crypto.ReturnDecryptRijndaelString(DecryptText);
                        /*Электронноцифровая подпись*/

                        SentText.Text = textForOutput;

                        united_str = "";

                        //LoadAttachments();
                    }
                    catch (Exception ex)
                    {
                        //System.Windows.MessageBox.Show(ex.Message);
                    }
                }
                if (WBox == 2)
                {
                    var ID = File.ReadLines(ToBox + "/Исходящие/ID.txt").ToList();
                    MessageID = ID[MesUid] + ".txt";
                    string sup = File.ReadAllLines(ToBox + "/Исходящие/Письма/" + MessageID).Skip(2).First();

                    string textForOutput = sup;
                    string[] temp = textForOutput.Split(new string[] { "^&*" }, StringSplitOptions.None);
                    string prKey = File.ReadAllText(path + user + "/Private.txt");

                    temp[1] = crypto.Decrypt(temp[1], prKey);
                    string DecryptText = "";
                    for (int i = 0; i < temp.Length; i++)    /*Формируем конечную строку*/
                        if (i < temp.Length - 1)
                            DecryptText += $"{temp[i]}^&*";
                        else
                            DecryptText += temp[i];
                    textForOutput = crypto.ReturnDecryptRijndaelString(DecryptText);
                    /*Электронноцифровая подпись*/

                    SentText.Text = textForOutput;

                    sup = "";

                    LoadAttachments();
                }
                if (WBox == 3)
                {
                    var ID = File.ReadLines(ToBox + "/Спам/ID.txt").ToList();
                    MessageID = ID[MesUid] + ".txt";
                    string sup = File.ReadAllLines(ToBox + "/Спам/Письма/" + MessageID).Skip(2).First();

                    string textForOutput = sup;
                    string[] temp = textForOutput.Split(new string[] { "^&*" }, StringSplitOptions.None);
                    string prKey = File.ReadAllText(path + user + "/Private.txt");

                    temp[1] = crypto.Decrypt(temp[1], prKey);
                    string DecryptText = "";
                    for (int i = 0; i < temp.Length; i++)    /*Формируем конечную строку*/
                        if (i < temp.Length - 1)
                            DecryptText += $"{temp[i]}^&*";
                        else
                            DecryptText += temp[i];
                    textForOutput = crypto.ReturnDecryptRijndaelString(DecryptText);
                    /*Электронноцифровая подпись*/

                    SentText.Text = textForOutput;

                    sup = "";

                    LoadAttachments();
                }
                if (WBox == 4)
                {
                    //ТУДУ
                }
            }
            catch
            {
                IsTheme.Text = "Ай-яй-яй. И не стыдно смотреть чужие письма?";
                SentText.Text = "Шифры-то не совпадают";
            }
        }

        public string getSuffix(string emails)
        {
            string email = emails.Substring(emails.LastIndexOf('@') + 1, emails.Length - (emails.LastIndexOf('@') + 1));
            return email;
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            SentMessage SM = new SentMessage(Convert.ToString(Chip.Content), Convert.ToString(Chip2.Content), psw);
            SM.Show();
            this.Close();
        }

        private void attachment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (var client = new ImapClient())
            {
                string IMapAddress = "imap." + getSuffix(Convert.ToString(Chip2.Content));
                client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                client.Connect(IMapAddress, 993, true);
                client.Authenticate(Convert.ToString(Chip2.Content), psw);

                if (WBox == 1)
                {
                    try
                    {
                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadWrite);

                        var message = inbox.GetMessage(MesUid);
                        FolderBrowserDialog FBD = new FolderBrowserDialog();

                        if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string index = Files[attachment.SelectedIndex];
                            using (var stream = File.Create(System.IO.Path.Combine(FBD.SelectedPath, index)))
                            {
                                foreach (var attachments in message.Attachments)
                                {
                                    if (index == attachments.ContentDisposition.FileName)
                                    {
                                        if (attachments is MessagePart)
                                        {
                                            var part = (MessagePart)attachments;
                                            part.Message.WriteTo(stream);
                                        }
                                        else
                                        {
                                            var part = (MimePart)attachments;
                                            part.Content.DecodeTo(stream);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                }
                if (WBox == 2)
                {
                    var inbox = client.GetFolder(SpecialFolder.Sent);
                    inbox.Open(FolderAccess.ReadWrite);

                    var message = inbox.GetMessage(MesUid);
                    FolderBrowserDialog FBD = new FolderBrowserDialog();

                    if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string index = Files[attachment.SelectedIndex];
                        using (var stream = File.Create(System.IO.Path.Combine(FBD.SelectedPath, index)))
                        {
                            foreach (var attachments in message.Attachments)
                            {
                                if (index == attachments.ContentDisposition.FileName)
                                {
                                    if (attachments is MessagePart)
                                    {
                                        var part = (MessagePart)attachments;
                                        part.Message.WriteTo(stream);
                                    }
                                    else
                                    {
                                        var part = (MimePart)attachments;
                                        part.Content.DecodeTo(stream);
                                    }
                                }
                            }
                        }
                    }
                }
                if (WBox == 3)
                {
                    var inbox = client.GetFolder(SpecialFolder.Junk);
                    inbox.Open(FolderAccess.ReadWrite);

                    var message = inbox.GetMessage(MesUid);
                    FolderBrowserDialog FBD = new FolderBrowserDialog();

                    if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string index = Files[attachment.SelectedIndex];
                        using (var stream = File.Create(System.IO.Path.Combine(FBD.SelectedPath, index)))
                        {
                            foreach (var attachments in message.Attachments)
                            {
                                if (index == attachments.ContentDisposition.FileName)
                                {
                                    if (attachments is MessagePart)
                                    {
                                        var part = (MessagePart)attachments;
                                        part.Message.WriteTo(stream);
                                    }
                                    else
                                    {
                                        var part = (MimePart)attachments;
                                        part.Content.DecodeTo(stream);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void LoadAttachments()
        {
            if (WBox == 1)
            {
                try
                {
                    using (var client = new ImapClient())
                    {
                        string IMapAddress = "imap." + getSuffix(Convert.ToString(Chip2.Content));
                        client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                        client.Connect(IMapAddress, 993, true);
                        client.Authenticate(Convert.ToString(Chip2.Content), psw);

                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadWrite);
                        inbox.AddFlags(MesUid, MessageFlags.Seen, true);

                        var message = inbox.GetMessage(MesUid);

                        foreach (var attachments in message.Attachments)
                        {
                            Files.Add(attachments.ContentDisposition.FileName);
                        }

                        for (int i = 0; i < Files.Count; i++)
                        {
                            attachment.Items.Add(Files[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
            if (WBox == 2)
            {
                using (var client = new ImapClient())
                {
                    string IMapAddress = "imap." + getSuffix(Convert.ToString(Chip2.Content));
                    client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                    client.Connect(IMapAddress, 993, true);
                    client.Authenticate(Convert.ToString(Chip2.Content), psw);

                    var inbox = client.GetFolder(SpecialFolder.Sent);
                    inbox.Open(FolderAccess.ReadWrite);

                    var message = inbox.GetMessage(MesUid);

                    foreach (var attachments in message.Attachments)
                    {
                        Files.Add(attachments.ContentDisposition.FileName);
                    }

                    for (int i = 0; i < Files.Count; i++)
                    {
                        attachment.Items.Add(Files[i]);
                    }
                }
            }
            if (WBox == 3)
            {
                using (var client = new ImapClient())
                {
                    string IMapAddress = "imap." + getSuffix(Convert.ToString(Chip2.Content));
                    client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                    client.Connect(IMapAddress, 993, true);
                    client.Authenticate(Convert.ToString(Chip2.Content), psw);

                    var inbox = client.GetFolder(SpecialFolder.Junk);
                    inbox.Open(FolderAccess.ReadWrite);

                    var message = inbox.GetMessage(MesUid);

                    foreach (var attachments in message.Attachments)
                    {
                        Files.Add(attachments.ContentDisposition.FileName);
                    }

                    for (int i = 0; i < Files.Count; i++)
                    {
                        attachment.Items.Add(Files[i]);
                    }
                }
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
