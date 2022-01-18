using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface IMailService
    {
        void SendEmailAsync(string recipientEmail, string recipientFirstName, string subject, string message);
    }
}
