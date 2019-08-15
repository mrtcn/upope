using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Upope.ClientNotificationTests.ViewModels;
using Upope.ServiceBase.Handler;
using Upope.ServiceBase.Services;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.ClientNotificationTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var notificationIp = "localhost:9200";
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .AddSingleton<IHttpHandler, HttpHandler>()
                .AddSingleton<IIdentityService, IdentityService>()
                .BuildServiceProvider();

            //configure console logging
            var httpHandler = serviceProvider.GetService<IHttpHandler>();
            var identityService = serviceProvider.GetService<IIdentityService>();


            Console.WriteLine("Enter your AccessToken!");
            var accessToken = ReadLine().Replace("\r\n", "").Trim();

            var userId = await identityService.GetUserId(accessToken);

            var notificationViewModel = new NotificationViewModel(accessToken);

            Console.WriteLine("Enter UserId whom you would like to talk with!");
            var chatUserId = ReadLine().Replace("\r\n", "").Trim();

            await notificationViewModel.NotificationConnect();
        
            while (true)
            {
                Thread.Sleep(500);
            }
        }

        private static string ReadLine()
        {
            var readlineBufferSize = 1200;
            Stream inputStream = Console.OpenStandardInput(readlineBufferSize);
            byte[] bytes = new byte[readlineBufferSize];
            int outputLength = inputStream.Read(bytes, 0, readlineBufferSize);

            char[] chars = Encoding.UTF7.GetChars(bytes, 0, outputLength);

            Console.WriteLine(new string(chars));
            return new string(chars);
        }
    }
}
