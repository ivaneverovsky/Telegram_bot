namespace Viikkkvv_bot
{
    public static class BotAnswers
    {
        public static string InfoMessage()
        {
            return string.Format(@"Привет! Этот бот поможет мне лучше узнать о вас, моих любимых подписчиках.

Команды:
/harakiri - отдать мне свои персональные данные, которыми я буду распоряжаться как мне угодно ха-ха.

P.S. Пока на этом всё, больше я команд не придумал.");
        }

        public static string Harakiri() => $"Сейчас я попрошу ввести ваши данные.";
        public static string Name() => $"Введите ваше имя";
        public static string Email() => $"Введите ваш email";
        public static string BirthDate() => $"Введите вашу дату рождения в формате ДД.ММ.ГГГГ";
        public static string PhoneNumber() => $"Введите ваш номер телефона в международном формате. Пример: +79991112233";
        public static string ThankYou() => $"Спасибо! Ваши данные были успешно записаны.";
        public static string SubscribeChannel() => $"Пожалуйста, подпишитесь на канал";
        public static string Subscribed() => $"От души, дядь";
    }
}
