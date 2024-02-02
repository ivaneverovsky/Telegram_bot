using Newtonsoft.Json;

namespace Viikkkvv_bot.TelegramBotData.Types
{
    public class CallbackQuery
    {
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "from", Required = Required.Always)]
        public User User { get; set; }

        [JsonProperty(PropertyName = "chat_instance", Required = Required.Always)]
        public string Instance { get; set; }

        [JsonProperty(PropertyName = "data", Required = Required.Always)]
        public string Data { get; set; }
    }
}
