using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace MailClient
{
    public partial class SentMessage : Window
    {
        List<string> AttachFiles = new List<string>();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private string eMail, pWord, where;
        string user = File.ReadAllText(@"/user.txt");

        private byte[] key = null;
        private byte[] iv = null;
        private RSAParameters? signaturePrivKey = null;
        private RSAParameters? signaturePubKey = null;

        public SentMessage(string _EMailData, string _PWData)
        {
            InitializeComponent();
            eMail = _EMailData;
            pWord = _PWData;
            attachment.Items.Clear();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0);
            timer.Start();
        }

        public SentMessage(string from, string _EMailData, string _PWData)
        {
            InitializeComponent();
            eMail = _EMailData;
            pWord = _PWData;
            to.Text = from;
            attachment.Items.Clear();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0);
            timer.Start();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void fontP_Click(object sender, RoutedEventArgs e)
        {
            if (SentText.FontSize < 25)
            {
                SentText.FontSize += 1;
                fontM.IsEnabled = true;
            }
            else
            {
                fontP.IsEnabled = false;
            }
        }

        private void fontM_Click(object sender, RoutedEventArgs e)
        {
            if (SentText.FontSize > 7)
            {
                SentText.FontSize -= 1;
                fontP.IsEnabled = true;
            }
            else
            {
                fontM.IsEnabled = false;
            }
        }

        private void Attach_Click(object sender, RoutedEventArgs e)
        {
            attachment.Items.Clear();
            OpenFileDialog FBD = new OpenFileDialog();
            if (FBD.ShowDialog() == true)
            {
                AttachFiles.Add(FBD.FileName);
                number.Content = Convert.ToInt32(number.Content) + 1;
                for (int i = 0; i < AttachFiles.Count; i++)
                {
                    attachment.Items.Add(AttachFiles[i].Substring(AttachFiles[i].LastIndexOf(@"\") + 1,
                        AttachFiles[i].Length - (AttachFiles[i].LastIndexOf(@"\") + 1)));
                }
            }
        }

        private void attachment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            attachment.Items.Remove(attachment.SelectedItem);
            number.Content = Convert.ToInt32(number.Content) - 1;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (!IsValidEmail(to.Text) || theme.Text == "" || keyY.Text == "")
            {
                SendL.Visibility = Visibility.Visible;
                Send.Visibility = Visibility.Hidden;
            }
            else
            {
                SendL.Visibility = Visibility.Hidden;
                Send.Visibility = Visibility.Visible;
            }
        }

        private string getSuffix()
        {
            string len = eMail;
            string email = len.Substring(len.LastIndexOf('@') + 1, len.Length - (len.LastIndexOf('@') + 1));
            return email;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(eMail));
            message.To.Add(new MailboxAddress(to.Text));
            message.Subject = theme.Text;

            var builder = new BodyBuilder();

            Crypto crypto = new Crypto();
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            string[] temps = crypto.ReturnEncryptRijndaelString(SentText.Text).Split(new string[] { "^&*" }, StringSplitOptions.None);

            string pbKey = keyY.Text;
                //File.ReadAllText("C:/KursachMailClient/" + user + "/Public.txt");  /*"Берём public ключ из базы"*/

            temps[1] = crypto.Encrypt(temps[1], pbKey);   /*Шифруем ключ при помощи алгоритма RSA*/

            string EncryptText = "";

            for (int i = 0; i < temps.Length; i++)    /*Формируем конечную строку*/
            {
                if (i < temps.Length - 1)
                    EncryptText += $"{temps[i]}^&*";
                else
                    EncryptText += temps[i];
            }

            builder.TextBody = EncryptText;

            string temp = "";

            if (AttachFiles.Count != 0)
            {
                for (int i = 0; i < AttachFiles.Count; i++)
                    temp = AttachFiles[i];
                    builder.Attachments.Add(temp);
            }

            //foreach (var file in AttachFiles)
            //{
            //    byte[] buff = null;
            //    FileStream fs = new FileStream(file,
            //    FileMode.Open,
            //    FileAccess.Read);
            //    BinaryReader br = new BinaryReader(fs);
            //    long numBytes = new FileInfo(file).Length;
            //    buff = br.ReadBytes((int)numBytes);
            //}

            message.Body = builder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, ex) => true;
                    client.Connect("smtp." + getSuffix(), 465, true);
                    client.Authenticate(eMail, pWord);
                    client.Send(message);

                    using (var _client = new ImapClient())
                    {
                        string IMapAddress = "imap." + getSuffix();
                        _client.ServerCertificateValidationCallback = (s, c, h, z) => true;
                        _client.Connect(IMapAddress, 993, true);
                        _client.Authenticate(eMail, pWord);
                        var folder = _client.GetFolder(SpecialFolder.Sent);
                        folder.Append(message);
                        //folder.MoveTo(message, _client.GetFolder(SpecialFolder.Trash));
                        _client.Disconnect(true);
                    }
                    
                    client.Disconnect(true);

                    MessageBoxResult dialogResult = MessageBox.Show("Отправлено. Закрыть?", "Закрыть?", 
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                    else
                    {
                        to.Clear();
                        theme.Clear();
                        SentText.Clear();
                        attachment.Items.Clear();
                        AttachFiles.Clear();
                        number.Content = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
