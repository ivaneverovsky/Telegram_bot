using System.Collections.Generic;
using Viikkkvv_bot.TelegramBotData.Model;

namespace Viikkkvv_bot
{
    internal class Storage
    {
        private List<Subscribers> _subscribers = new List<Subscribers>();
        public List<Subscribers> Subscribers { get { return _subscribers; } }
        public void AddSubscriber(Subscribers subscribers)
        {
            _subscribers.Add(subscribers);
        }
    }
}
