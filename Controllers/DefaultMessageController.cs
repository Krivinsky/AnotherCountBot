using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AnotherCountBot.Controllers
{
    public class DefaultMessageController
    {
        ITelegramBotClient _telegramBotClient;

        public DefaultMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken token) 
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramBotClient.SendTextMessageAsync(
                message.Chat.Id,
                $"Получено сообщение не поддерживаемого формата",
                cancellationToken: token);
        }
    }
}
