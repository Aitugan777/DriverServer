namespace DriverServer.Models
{
    public class ConfirmRequest
    {
        public int Code { get; set; }

        public User User { get; set; }
    }
}
