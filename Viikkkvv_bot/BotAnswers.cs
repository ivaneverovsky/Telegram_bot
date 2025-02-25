namespace Viikkkvv_bot
{
    public static class BotAnswers
    {
        public static string InfoMessage()
        {
            return string.Format(@"Привет! Это тестовый бот для сбора данных о моих подписчиках.

Команды:
/share - поделиться со мной своими персональными данными.
/list - посмотреть данные моих подписчиков.
/delete - удалить все данные.
");
        }

        public static string Harakiri() => $"Сейчас я попрошу ввести ваши данные.";
        public static string Name() => $"Введите ваше имя";
        public static string Email() => $"Введите ваш email";
        public static string BirthDate() => $"Введите вашу дату рождения в формате ДД.ММ.ГГГГ";
        public static string PhoneNumber() => $"Введите ваш номер телефона в международном формате. Пример: +79991112233";
        public static string ThankYou() => $"Спасибо! Ваши данные были успешно записаны.";
        public static string SubList() => $"Данные подписчиков";
        public static string Deleted() => $"Данные подписчиков удалены";
        //public static string SubscribeChannel() => $"Пожалуйста, подпишитесь на канал";
        //public static string Subscribed() => $"От души, дядь";
    }
}
