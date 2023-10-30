using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using AnotherCountBot.Services;

namespace AnotherCountBot.Controllers
{
    public class TextMessageController
    {
        ITelegramBotClient _telegramBotClient;
        IStorage _memoryStorage;
        IService _service;


        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, IService service)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _service = service;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            switch (message.Text)
            {
                case "/start":
                    // Объект, представляющий кнопки
                    var buttons = new List<InlineKeyboardButton[]>
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData($" количество символов в тексте", "count"),
                            InlineKeyboardButton.WithCallbackData($" вычисление суммы чисел", "sum")
                        }
                    };

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                        $"<b> Этот бот может либо подсчитать количество символов в тексте либо вычислить сумму чисел</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Выберите какую функцию вы сейчас хотите.{Environment.NewLine}",
                        cancellationToken: ct, parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:
                    //логика по обработки либо суммы чисел либо количества символов

                    string functionId = _memoryStorage.GetSession(message.Chat.Id).functionId;

                    if (functionId == "count")
                    {
                        //TODO количество символов
                        int result = _service.Count(message.Text);
                        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, Convert.ToString(result));
                        break;
                    }

                    if (functionId == "sum")
                    {
                        //TODO сумма чисел
                        int result = _service.Sum(message.Text);
                        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, Convert.ToString(result));
                        break;
                    }

                    {
                        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Что то пошло не так ((((",
                            cancellationToken: ct);
                    }
                    break;
            }
        }
    }
}
