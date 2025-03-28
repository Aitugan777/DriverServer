namespace DriverServer.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Отправить код подтверждения
        /// </summary>
        /// <param name="email">Почта</param>
        /// <param name="code">Код подтверждения</param>
        public void SendConfirmLetter(string email, string code);

        /// <summary>
        /// Отправить уведомление
        /// </summary>
        /// <param name="email">Почта</param>
        /// <param name="letter">Сообщение</param>
        public void SendNotificationLetter(string email, string letter);
    }
}
