using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFinder.Service.Factories
{
    internal static class EmailListFactory
    {
        internal static List<string> Create(string firstName, string lastName, string domain)
        {
            var listOfEmails = new List<string>
            {
                $"{firstName}.{lastName}{domain}",
                $"{firstName}{lastName}{domain}",
                $"{firstName}-{lastName}{domain}",
                $"{firstName[0]}.{lastName}{domain}",
                $"{firstName[0]}{lastName}{domain}",
                $"{firstName[0]}-{lastName}{domain}",
                $"{lastName}.{firstName[0]}{domain}",
                $"{lastName}{firstName[0]}{domain}",
                $"{lastName}-{firstName[0]}{domain}",
                $"{firstName}.{lastName[0]}{domain}",
                $"{firstName}{lastName[0]}{domain}",
                $"{firstName}-{lastName[0]}{domain}",
                $"{lastName}.{firstName}{domain}",
                $"{lastName}{firstName}{domain}",
                $"{lastName}-{firstName}{domain}",
                $"{lastName[0]}.{firstName}{domain}",
                $"{lastName[0]}{firstName}{domain}",
                $"{lastName[0]}-{firstName}{domain}"
            };

            return listOfEmails;
        }
    }
}
