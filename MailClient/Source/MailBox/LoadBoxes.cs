using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Source.MailBox
{
    class LoadBoxes
    {
        public static void Load(ChooseMail chooseMail, string Login)
        {
            if (File.Exists("C:/KursachMailClient/" + Login + "/Boxes.txt") == true)
            {
                var Mails = File.ReadLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
                for (int i = 0; i < Mails.Count; i++)
                {
                    chooseMail.MSList.Items.Add(new LetterConstruction { Name = Mails[i] });
                }
            }
        }

        public static void Chip(ChooseMail chooseMail, string Login)
        {
            var Mails = File.ReadLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
            File.WriteAllText(@"/temp.txt", Convert.ToString(Convert.ToString(Mails[chooseMail.MSList.SelectedIndex])));
        }

        public static void CheckLettersData(ChooseMail chooseMail, string Login)
        {
            List<string> Boxes = new List<string> { "Входящие", "Исходящие", "Спам", "Корзина" };
            var Mails = File.ReadLines("C:/KursachMailClient/" + Login + "/Boxes.txt").ToList();
            foreach (string _box in Boxes)
            {
                if (!Directory.Exists("C:/KursachMailClient/" + Login + "/" + Convert.ToString(Mails[chooseMail.MSList.SelectedIndex]) +
                    "/" + _box + "/Письма"))
                {
                    Directory.CreateDirectory("C:/KursachMailClient/" + Login + "/" + Convert.ToString(Mails[chooseMail.MSList.SelectedIndex]) +
                        "/" + _box + "/Письма");
                }
                if (!File.Exists("C:/KursachMailClient/" + Login + "/" + Mails[chooseMail.MSList.SelectedIndex] + "/" + _box + "/ID.txt"))
                {
                    File.Create("C:/KursachMailClient/" + Login + "/" + Mails[chooseMail.MSList.SelectedIndex] + "/" + _box + "/ID.txt");
                }
            }
        }
    }
}
