using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viikkkvv_bot.TelegramBotData.Types;
using Viikkkvv_bot.TelegramBotData.Enums;

namespace Viikkkvv_bot.Interfaces
{
    interface ITelegramBotClient
    {
        event Action<Message> OnMessageReceived;
        event Action<Update> UpdateReceived;
        void StartBot();
        Task<Update[]> GetUpdatesAsync();
        Task<Message> SendMessageAsync(MessageType type, long chatId, string content, Dictionary<string, object> parameters = null);
        Task<Message> SubscribeChannel(MessageType type, long chatId, string content, Dictionary<string, object> parameters = null);
        Task<Message> CheckChatMember(MessageType type, long chatId);
    }
}
