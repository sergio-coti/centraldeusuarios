using CentralDeUsuarios.Infra.Messages.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Helpers
{
    /// <summary>
    /// Classe para envio de emails
    /// </summary>
    public class MailHelper
    {
        private readonly MailSettings _mailSettings;

        public MailHelper(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        /// <summary>
        /// Método para envio de emails
        /// </summary>
        public void Send(string mailTo, string subject, string body)
        {
            #region Escrevendo o email

            var mailMessage = new MailMessage(_mailSettings.Email, mailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            #endregion

            #region Enviando o email

            var smtpClient = new SmtpClient(_mailSettings.Smtp, _mailSettings.Port.Value);
            smtpClient.EnableSsl= true;
            smtpClient.Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password);
            smtpClient.Send(mailMessage);

            #endregion
        }
    }
}
