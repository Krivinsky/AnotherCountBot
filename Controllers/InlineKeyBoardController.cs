using AnotherCountBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AnotherCountBot.Controllers
{
    public class InlineKeyBoardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramBotClient;

        public InlineKeyBoardController(
            IStorage memoryStorage,
            ITelegramBotClient telegramBotClient)
        {
            _memoryStorage = memoryStorage;
            _telegramBotClient = telegramBotClient;
        }


        /// <summary>
        /// Обрабатываем нажатия на кнопки  из Telegram Bot API
        /// </summary>
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery == null) {  return; }

            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).functionId = callbackQuery.Data;

            // Генерим информационное сообщение
            string functionText = callbackQuery.Data switch
            {
                "count" => " количество символов в тексте",
                "sum" => " вычисление суммы чисел",
                _ => string.Empty
            };

            await _telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбрана функция - {functionText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.",
                cancellationToken: ct,
                parseMode: ParseMode.Html);
        }
    }
}
