using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Upope.ClientTests.ViewModel
{
    public class ChallengeViewModel
    {
        HubConnection hubConnection;
        public ChallengeViewModel(string accessToken)
        {
            // localhost for UWP/iOS or special IP for Android
            var ip = "challenge.upope.com";

            hubConnection = new HubConnectionBuilder() 
                .WithUrl($"http://{ip}/challengehubs", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(accessToken);
                })
                .Build();
        }

        public async Task Connect()
        {
            try
            {
                await hubConnection.StartAsync();

                hubConnection.On<string>("ChallengeRequestReceived", (message) =>
                {
                    Console.WriteLine("ChallengeRequestReceived" + message);
                    var finalMessage = message;
                    // Update the UI
                });

                hubConnection.On<string>("ChallengeRequestAccepted", (message) =>
                {
                    Console.WriteLine("ChallengeRequestAccepted" + message);
                    var finalMessage = message;
                    // Update the UI
                });

                hubConnection.On<string>("ChallengeRequestRejected", (message) =>
                {
                    Console.WriteLine("ChallengeRequestRejected" + message);
                    var finalMessage = message;
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
