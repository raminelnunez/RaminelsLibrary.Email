using System.Net.Mail;
using System.Text;
using RaminelsLibrary.Email.Models;

namespace RaminelsLibrary.Email
{
    public static class ErrorNotifier
    {
        private static readonly List<Error> Errors = [];

        private static readonly List<string> Warnings = [];

        public static EmailDetails? EmailDetails
        {
            get
            {
                return EmailDetails ?? Email.Details;
            }

            set
            {
                EmailDetails = value;
            }
        }

        public static void AddError(Exception exception, string? description = null)
        {
            Errors.Add(
                new Error(exception, description)
            );
        }

        public static void AddWarning(string warningMessage)
        {
            Warnings.Add(warningMessage);
        }

        public static void NotifyErrors()
        {
            if (Errors.Count == 0)
                return;

            string subject = $"{AppSettings.AppName} - Errors";

            StringBuilder body = new();

            body.AppendLine(DateTime.Now.ToString());

            body.AppendLine($"Machine: {Environment.MachineName}");
            body.AppendLine($"Version: {AppSettings.AppVersion}");

            body.AppendLine("Errors:");

            foreach (Error error in Errors)
            {
                body.AppendLine($"[{error.DateTime}] {error.Exception.Message}: {error.Exception}.");

                if (error.Description is not null)
                    body.Append($"({error.Description}).");
            }

            Email.Send(EmailDetails, subject, body.ToString(), MailPriority.High);

            Errors.Clear();
        }

        public static void SendWarnings()
        {
            if (Warnings.Count == 0)
                return;

            string subject = $"{AppSettings.AppName} - Warnings";

            StringBuilder body = new();

            body.AppendLine(DateTime.Now.ToString());

            body.AppendLine($"Machine: {Environment.MachineName}");
            body.AppendLine($"Version: {AppSettings.AppVersion}");

            body.AppendLine("Warnings:");

            foreach (string warning in Warnings)
            {
                body.AppendLine(warning);
            }

            Email.Send(EmailDetails, subject, body.ToString());

            Warnings.Clear();
        }
    }
}