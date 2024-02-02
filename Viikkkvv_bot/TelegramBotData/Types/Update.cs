using Newtonsoft.Json;

namespace Viikkkvv_bot.TelegramBotData.Types
{
    public class Update
    {
        [JsonProperty(PropertyName = "update_id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "message", Required = Required.Default)]
        public Message Message { get; set; }

        [JsonProperty(PropertyName = "callback_query", Required = Required.Default)]
        public CallbackQuery CallbackQuery { get; set; }

        [JsonProperty(PropertyName = "chat_member", Required = Required.Default)]
        public ChatMemberUpdated ChatMemberUpdated { get; set; }
    }
}
