using Newtonsoft.Json;

namespace Viikkkvv_bot.TelegramBotData.Types
{
    public class ChatMember
    {
        [JsonProperty(PropertyName = "status", Required = Required.Always)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "user", Required = Required.Always)]
        public User User { get; set; }
    }
}
