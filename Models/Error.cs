namespace RaminelsLibrary.Email.Models
{
    public class Error(Exception exception, string? description = null)
    {
        public Exception Exception = exception;

        public string? Description = description;

        public DateTime DateTime = DateTime.Now;
    }
}
