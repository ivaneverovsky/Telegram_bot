using Newtonsoft.Json;

namespace Viikkkvv_bot.TelegramBotData.Types
{
    public class ChatMemberUpdated
    {
        [JsonProperty(PropertyName = "chat", Required = Required.Always)]
        public Chat Chat { get; set; }

        [JsonProperty(PropertyName = "from", Required = Required.Always)]
        public User User { get; set; }

        [JsonProperty(PropertyName = "date", Required = Required.Always)]
        public int Date { get; set; }

        [JsonProperty(PropertyName = "old_chat_member", Required = Required.Always)]
        public ChatMember OldChatMember { get; set; }

        [JsonProperty(PropertyName = "new_chat_member", Required = Required.Always)]
        public ChatMember NewChatMember { get; set; }
    }
}
