using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Upope.ClientNotificationTests.ViewModels
{
    public class NotificationViewModel
    {
        HubConnection notificationHub;

        public NotificationViewModel(string accessToken)
        {
            var notificationIp = "localhost:9200";

            notificationHub = new HubConnectionBuilder()
                .WithUrl($"http://{notificationIp}/notificationhub", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(accessToken);
                }).Build();
        }


        public async Task NotificationConnect()
        {
            try
            {
                await notificationHub.StartAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            notificationHub.On<string>("ReceiveMessage", (message) =>
            {
                Console.WriteLine("ReceiveMessage throug ChatHubs");
                Console.Write(message);

                var finalMessage = message;
                // Update the UI
            });
        }
    }
}
