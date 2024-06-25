using Microsoft.Extensions.Configuration;
using RaminelsLibrary.Email.Models;

namespace RaminelsLibrary.Email
{
    public static class AppSettingsExtension
    {
        public static EmailDetails GetEmailDetails(this RaminelsLibrary.AppSettings appSettings, IConfigurationSection emailDetails)
        {
            string? server = emailDetails?.GetValue<string?>("Server");
            string? sender = emailDetails?.GetValue<string?>("Sender");
            string? recipients = emailDetails?.GetValue<string?>("Recipients");

            if (server is null || sender is null || recipients is null)
                throw new Exception("Email Not Configured: If Email Server, Sender and Recipients are not configured, you will miss error & warning notifications. "
                    + "If you want to disable emails, configure value Email.DoNotSend = true in appsettings.json");

            bool doNotSend = emailDetails?.GetValue<bool?>("DoNotSend") ?? throw new Exception("value DoNotSend has not been configured. " +
                    "Email.DoNotSend value determines whether an email should be sent for warnings, errors and other notifications" +
                    "Set value as false to receive email notifications");

            return new EmailDetails()
            {
                Server = server,
                Sender = sender,
                Recipients = recipients,
                DoNotSend = doNotSend
            };
        }
    }
}
