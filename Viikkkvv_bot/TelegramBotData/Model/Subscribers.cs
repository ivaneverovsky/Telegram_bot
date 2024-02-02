namespace Viikkkvv_bot.TelegramBotData.Model
{
    public class Subscribers
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }

        public Subscribers(string name, string email, string birthDate, string phoneNumber)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }
    }
}
