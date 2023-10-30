using System.Text;
using AnotherCountBot.Controllers;
using AnotherCountBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace AnotherCountBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureSevices(services))   // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build();

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureSevices(IServiceCollection services)
        {

            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyBoardController>();
            services.AddTransient<DefaultMessageController>();

            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddSingleton<IService, WorkService>();

            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider 
                => new TelegramBotClient("6942178033:AAFtIh5FvhplhcNw1LBc-Sqa3y3EKBDjh04"));

            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }
    }
}