using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Upope.ClientTests.ViewModel
{
    public class ChallengeViewModel
    {
        HubConnection hubConnection;
        public ChallengeViewModel()
        {
            // localhost for UWP/iOS or special IP for Android
            var ip = "localhost";

            hubConnection = new HubConnectionBuilder()
                .WithUrl($"http://{ip}:58836/challangehub", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult("");
                })
                .Build();
        }

        public async Task Connect()
        {
            try
            {
                await hubConnection.StartAsync();

                hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    var finalMessage = $"{user} says {message}";
                    // Update the UI
                });
            }
            catch (Exception ex)
            {
                // Something has gone wrong
            }
        }

        public async Task SendMessage(string user, string message)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessage", user, message);
            }
            catch (Exception ex)
            {
                // send failed
            }
        }
    }

}
