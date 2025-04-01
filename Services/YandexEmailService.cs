using DriverServer.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace DriverServer.Services
{
    public class YandexEmailService : IEmailService
    {
        public void SendConfirmLetter(string email, string code)
        {
        }

        public void SendNotificationLetter(string email, string letter)
        {
        }
    }
}
