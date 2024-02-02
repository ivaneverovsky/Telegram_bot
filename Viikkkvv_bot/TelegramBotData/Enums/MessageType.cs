using System;
using System.Collections.Generic;

namespace Viikkkvv_bot.TelegramBotData.Enums
{
    public enum MessageType
    {
        TextMessage,
        ChatMember,
        AnswerCallback
    }

    public static class MessageTypeExtension
    {
        public static KeyValuePair<string, string> ToKeyValue(this MessageType type)
        {
            switch (type)
            {
                case MessageType.TextMessage:
                    return new KeyValuePair<string, string>("sendMessage", "text");
                case MessageType.ChatMember:
                    return new KeyValuePair<string, string>("getChatMember", "text");
                case MessageType.AnswerCallback:
                    return new KeyValuePair<string, string>("answerCallbackQuery", "text");
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
