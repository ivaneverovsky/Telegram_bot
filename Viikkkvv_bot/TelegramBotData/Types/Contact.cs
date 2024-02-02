using Newtonsoft.Json;

namespace Viikkkvv_bot.TelegramBotData.Types
{
    public class Contact
    {
        [JsonProperty(PropertyName = "phone_number", Required = Required.Always)]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "first_name", Required = Required.Always)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name", Required = Required.Default)]
        public string LastName { get; set; }
    }
}
