using System;
using System.Text;

namespace Viikkkvv_bot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            TelegramBotClient tg = new TelegramBotClient("6875248759:AAFa37YuNXKNC7yktFy9iWJAIUMYGwZGWwQ");
            tg.LogMessage += m => Console.WriteLine(m);

            BotManager bm = new BotManager(tg);

            tg.StartBot();
            Console.ReadLine();
        }
    }
}
