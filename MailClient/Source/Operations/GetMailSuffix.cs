using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Source.Operations
{
    class GetMailSuffix
    {
        public string GetSuffix(string address)
        {
            string Suffix = address.Substring(address.LastIndexOf('@') + 1, 
                address.Length - (address.LastIndexOf('@') + 1));
            return Suffix;
        }
    }
}
