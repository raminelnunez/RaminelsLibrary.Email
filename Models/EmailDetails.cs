namespace RaminelsLibrary.Email.Models
{
    public class EmailDetails
    {
        public string Server { get; set; }

        public string Sender { get; set; }

        public string Recipients { get; set; }

        public bool DoNotSend { get; set; } = false;

    }
}
