namespace RaminelsLibrary.Email.Models
{
    public class EmailDetails
    {
        public required string Server { get; set; }

        public required string Sender { get; set; }

        public required string Recipients { get; set; }

        public bool DoNotSend { get; set; } = false;

    }
}
