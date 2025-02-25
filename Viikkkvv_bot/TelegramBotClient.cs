using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Viikkkvv_bot.Interfaces;
using Viikkkvv_bot.TelegramBotData.Enums;
using Viikkkvv_bot.TelegramBotData.Types;

namespace Viikkkvv_bot
{
    class TelegramBotClient : ITelegramBotClient
    {
        public event Action<Message> OnMessageReceived;
        public event Action<Update> UpdateReceived;
        public event Action<string> LogMessage;

        private readonly string _token;
        private const string _baseUrl = "https://api.telegram.org/bot";
        private long _offset = 0;

        public TelegramBotClient(string token)
        {
            _token = token;

            OnMessageReceived += m =>
            {
                LogMessage($"Message from {m.User.FirstName} {m.User.LastName} with chat_id {m.Chat.Id} was recieved: {m.Text}");
            };
        }

        public async void StartBot()
        {
            LogMessage?.Invoke("The bot is running");
            while (true)
            {
                try
                {
                    var updates = await GetUpdatesAsync();
                    foreach (var update in updates)
                    {
                        if (update.Message == null && update.CallbackQuery == null && update.ChatMemberUpdated == null)
                            continue;

                        if (update.Message != null && update.Message.Text != null && update.Message.Text != string.Empty)
                            OnMessageReceived?.Invoke(update.Message);

                        if (update.Message != null && update.Message.Contact != null && update.Message.Contact.PhoneNumber != null && update.Message.Contact.PhoneNumber != string.Empty)
                        {
                            update.Message.Text = update.Message.Contact.PhoneNumber;
                            OnMessageReceived?.Invoke(update.Message);
                        }

                        if (update.CallbackQuery != null && update.CallbackQuery.Data == "ALREADY")
                        {
                            var message = new Message();
                            var chat = new Chat();
                            update.Message = message;
                            update.Message.Chat = chat;

                            update.Message.User = update.CallbackQuery.User;
                            update.Message.Chat.Id = update.CallbackQuery.User.Id;
                            update.Message.Text = update.CallbackQuery.Id;

                            OnMessageReceived?.Invoke(update.Message);
                        }


                        _offset = update.Id + 1;
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(10000);
                }
            }
        }

        public async Task<Update[]> GetUpdatesAsync()
        {
            var parameters = new Dictionary<string, object>
            {
                {"offset", _offset }
            };
            return await SendWebRequest<Update[]>("getUpdates", parameters);
        }
        public Task<Message> SendMessageAsync(MessageType type, long chatId, string content, Dictionary<string, object> parameters = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();

            var typeInfo = type.ToKeyValue();

            parameters.Add("chat_id", chatId);
            if (!string.IsNullOrEmpty(typeInfo.Value))
                parameters.Add(typeInfo.Value, content);

            if (content.Contains("Введите ваш номер телефона"))
            {
                KeyboardButton button = KeyboardButton.WithRequestContact("Поделиться контактом");
                ReplyKeyboardMarkup rkm = new ReplyKeyboardMarkup(button);
                rkm.ResizeKeyboard = true;

                parameters.Add("reply_markup", rkm);
            }

            if (content.Contains("Спасибо! Ваши данные были успешно записаны."))
            {
                ReplyKeyboardRemove rkr = new ReplyKeyboardRemove();
                parameters.Add("reply_markup", rkr);
            }

            LogMessage($"Message to {chatId} was sent.");

            if (typeInfo.Key == "answerCallbackQuery")
            {
                parameters.Clear();
                parameters.Add("callback_query_id", content);
                parameters.Add("text", "инфо");
                
            }

            return SendWebRequest<Message>(typeInfo.Key, parameters);
        }
        public Task<Message> SubscribeChannel(MessageType type, long chatId, string content, Dictionary<string, object> parameters = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();

            var typeInfo = type.ToKeyValue();

            parameters.Add("chat_id", chatId);
            if (!string.IsNullOrEmpty(typeInfo.Value))
                parameters.Add(typeInfo.Value, content);

            InlineKeyboardButton button = new InlineKeyboardButton("Подписаться на канал");
            InlineKeyboardButton button2 = new InlineKeyboardButton("Уже подписан на канал");

            button.Url = "https://t.me/cvpital_beatz";
            button2.CallbackData = "ALREADY";

            InlineKeyboardButton[] row1 = new InlineKeyboardButton[] { button };
            InlineKeyboardButton[] row2 = new InlineKeyboardButton[] { button2 };

            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[][] { row1, row2 };
            InlineKeyboardMarkup ikm = new InlineKeyboardMarkup(buttons);


            parameters.Add("reply_markup", ikm);

            LogMessage($"Message to {chatId} was sent.");

            return SendWebRequest<Message>(typeInfo.Key, parameters);
        }
        public Task<Message> CheckChatMember(MessageType type, long chatId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            var typeInfo = type.ToKeyValue();

            parameters.Add("chat_id", "@cvpital_beatz");
            parameters.Add("user_id", chatId);

            return SendWebRequest<Message>(typeInfo.Key, parameters);
        }

        public async Task<T> SendWebRequest<T>(string methodName, Dictionary<string, object> parameters = null)
        {
            var uri = new Uri($"{_baseUrl}{_token}/{methodName}");
            Response<T> responseObject = null;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response;

                    if (parameters == null || parameters.Count == 0)
                    {
                        response = await client.GetAsync(uri);
                    }
                    else
                    {
                        var data = JsonConvert.SerializeObject(parameters);
                        var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                        response = await client.PostAsync(uri, httpContent);
                    }
                    var resultString = await response.Content.ReadAsStringAsync();
                    responseObject = JsonConvert.DeserializeObject<Response<T>>(resultString);

                }
                catch (HttpRequestException e)
                {
                    LogMessage?.Invoke($"Unable to provide operation {methodName}. Exception occured: {e.Message}");
                    throw new HttpRequestException(e.Message);
                }
            }

            return responseObject.Result;
        }
    }
}
