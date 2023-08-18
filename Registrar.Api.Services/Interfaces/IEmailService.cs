using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string recipientMail, string subject, string mailContent, string senderName = null);
    }
}
