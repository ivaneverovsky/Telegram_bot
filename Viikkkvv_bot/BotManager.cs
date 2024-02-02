using System.Collections.Generic;
using Viikkkvv_bot.Interfaces;
using Viikkkvv_bot.TelegramBotData.Enums;
using Viikkkvv_bot.TelegramBotData.Model;
using Viikkkvv_bot.TelegramBotData.Types;

namespace Viikkkvv_bot
{
    class BotManager
    {
        Storage _stor = new Storage();

        Dictionary<long, QueryType> _botWaitsForQuery;
        List<string> Subscriber = new List<string>();

        ITelegramBotClient _client;

        public BotManager(ITelegramBotClient client)
        {
            _botWaitsForQuery = new Dictionary<long, QueryType>();

            _client = client;

            client.OnMessageReceived += ProcessMessage;
        }

        public async void ProcessMessage(Message message)
        {
            var chatId = message.Chat.Id;

            if (_botWaitsForQuery.ContainsKey(chatId))
            {
                switch (_botWaitsForQuery[chatId])
                {
                    case QueryType.Name:
                        await _client.SendMessageAsync(MessageType.TextMessage, chatId, BotAnswers.Name());
                        _botWaitsForQuery[chatId] = QueryType.Email;

                        break;

                    case QueryType.Email:
                        Subscriber.Add(message.Text);
                        await _client.SendMessageAsync(MessageType.TextMessage, chatId, BotAnswers.Email());
                        _botWaitsForQuery[chatId] = QueryType.BirthDate;

                        break;

                    case QueryType.BirthDate:
                        Subscriber.Add(message.Text);
                        await _client.SendMessageAsync(MessageType.TextMessage, chatId, BotAnswers.BirthDate());
                        _botWaitsForQuery[chatId] = QueryType.PhoneNumber;

                        break;

                    case QueryType.PhoneNumber:
                        Subscriber.Add(message.Text);
                        await _client.SendMessageAsync(MessageType.TextMessage, chatId, BotAnswers.PhoneNumber());
                        _botWaitsForQuery[chatId] = QueryType.ThankYou;

                        break;

                    case QueryType.ThankYou:
                        Subscriber.Add(message.Text);
                        await _client.SendMessageAsync(MessageType.TextMessage, chatId, BotAnswers.ThankYou());
                        _botWaitsForQuery[chatId] = QueryType.Channel;

                        //add subscriber to Storage
                        var subs = new Subscribers(Subscriber[0], Subscriber[1], Subscriber[2], Subscriber[3]);
                        _stor.AddSubscriber(subs);
                        Subscriber.Clear();

                        await _client.SubscribeChannel(MessageType.TextMessage, chatId, BotAnswers.SubscribeChannel());

                        break;

                    case QueryType.Channel:
                        //await _client.CheckChatMember(MessageType.ChatMember, chatId);

                        await _client.SendMessageAsync(MessageType.AnswerCallback, chatId, message.Text);

                        _botWaitsForQuery.Remove(chatId);
                        
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (message.Text.ToLower().Trim())
                {
                    case "/start":
                    case "/info":
                        {
                            await _client.SendMessageAsync(MessageType.TextMessage, chatId, BotAnswers.InfoMessage());
                            break;
                        }
                    case "/harakiri":
                        {
                            await _client.SendMessageAsync(MessageType.TextMessage, chatId, BotAnswers.Harakiri());
                            _botWaitsForQuery[chatId] = QueryType.Name;

                            await _client.SendMessageAsync(MessageType.TextMessage, chatId, BotAnswers.Name());
                            _botWaitsForQuery[chatId] = QueryType.Email;
                            break;
                        }
                }
            }
        }
    }
}
