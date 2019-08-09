using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Upope.ClientTests.Models;
using Upope.ClientTests.ViewModel;
using Upope.ServiceBase.Handler;
using Upope.ServiceBase.Services;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.ClientTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var chatIp = "localhost:9100";
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

            var challengeViewModel = new ChallengeViewModel(accessToken);

            Console.WriteLine("Enter UserId whom you would like to talk with!");
            var chatUserId = ReadLine().Replace("\r\n", "").Trim();

            var createChatModel = await httpHandler.AuthPostAsync<CreateChatModel>(accessToken, chatIp, $"ChatRoom/{chatUserId}");

            await challengeViewModel.ChatConnect();

            while (true)
            {
                Console.WriteLine("Enter your message!");
                var message = ReadLine();

                await challengeViewModel.SendChatMessage(userId, message, createChatModel.ChatRoomId);
            }
            
            //await challengeViewModel.ChallengeConnect();
            //await challengeViewModel.GameConnect();
            Thread.Sleep(500);
            //await challengeViewModel.SendChallenge();
            //challengeViewModel.SendMessage("User XXX", "Message 1");
            //var key = Console.ReadKey();
            //if (key.KeyChar == 1)
            //    Console.WriteLine(1);
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
