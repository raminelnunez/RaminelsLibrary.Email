using RaminelsLibrary.Email.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace RaminelsLibrary.Email
{

    public partial class Email(EmailDetails details,
        string subject, 
        string body, 
        MailPriority mailPriority = 0, 
        string[]? attachments = null)
    {

        public string Subject = subject;

        public string Body = body;

        public MailPriority MailPriority = mailPriority;

        public string[]? Attachments = attachments;

        public void Send()
        {
            Send(emailDetails: details,
                subject: Subject,
                body: Body,
                mailPriority: MailPriority,
                attachments: Attachments);
        }
    }

    public partial class Email
    {
        public static EmailDetails? Details { get; set; }

        public static void Send(string subject, string body, MailPriority mailPriority = 0, string[]? attachments = null)
        {
            if (Details is null || Details.DoNotSend)
                return;

            Send(
                server: Details.Server,
                sender: Details.Sender,
                recipients: Details.Recipients,
                subject: subject,
                body: body,
                mailPriority: mailPriority,
                attachments: attachments);
        }

        public static void Send(
            EmailDetails emailDetails,
            string subject, string body,
            MailPriority mailPriority = 0,
            string[]? attachments = null)
        {
            if (emailDetails.DoNotSend)
                return;

            Send(
                server: emailDetails.Server,
                sender: emailDetails.Sender,
                recipients: emailDetails.Recipients,
                subject: subject,
                body: body,
                mailPriority: mailPriority,
                attachments: attachments);
        }


        public static void Send(
            string server,
            string sender,
            string recipients,
            string subject,
            string body,
            MailPriority mailPriority = 0,
            string[]? attachments = null)
        {
            try
            {
                MailMessage mail = new(sender, recipients)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,
                    Priority = mailPriority,
                };

                if (attachments is not null)
                {
                    foreach (string item in attachments)
                    {
                        mail.Attachments.Add(new Attachment(item));
                    }
                }

                using SmtpClient smtp = new(server)
                {
                    UseDefaultCredentials = true
                };

                smtp.Send(mail);

                mail.Dispose();
                smtp.Dispose();
            }
            catch
            {

            }
        }

        public static bool IsValidEmailAddress(string emailAddress)
        {
            return ValidEmailAddressRegex().IsMatch(emailAddress);
        }

        [GeneratedRegex(@"^([a-zA-Z0-9][a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]*[a-zA-Z0-9])@((?!-)(?!.*--)(?!.*\.$)[A-Za-z0-9-]+(?<!-)(\.[A-Za-z0-9-]+(?<!-))+)$")]
        private static partial Regex ValidEmailAddressRegex();
    }
}
