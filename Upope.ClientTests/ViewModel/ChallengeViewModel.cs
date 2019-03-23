using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Upope.ClientTests.ViewModel
{
    public class ChallengeViewModel
    {
        HubConnection hubConnection;
        public ChallengeViewModel(string accessToken)
        {
            // localhost for UWP/iOS or special IP for Android
            //var ip = "challenge.upope.com";
            var ip = $"localhost:56224";

            try
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl($"http://{ip}/challengehubs?access_token={accessToken}", options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(accessToken);
                    }).Build();
            }
            catch(Exception ex)
            {
                var xx = ex;
            }            
        }

        public async Task SendChallenge()
        {
            try
            {
                Thread.Sleep(5000);
                await hubConnection.SendAsync("SendMessage", "user1", "message");
                await hubConnection.InvokeAsync("SendMessage", "user2", "user2 message");
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }            
        }

        public async Task Connect()
        {
            try
            {
                await hubConnection.StartAsync();

                hubConnection.On<string>("ChallengeRequestReceived", (message) =>
                {
                    Console.WriteLine("ChallengeRequestReceived");
                    Console.Write(message);

                    var finalMessage = message;
                    // Update the UI
                });

                hubConnection.On<string>("ChallengeRequestAccepted", (message) =>
                {
                    Console.WriteLine("ChallengeRequestAccepted");
                    Console.Write(message);

                    var finalMessage = message;
                    // Update the UI
                });

                hubConnection.On<string>("ChallengeRequestRejected", (message) =>
                {
                    Console.WriteLine("ChallengeRequestRejected");
                    Console.Write(message);

                    var finalMessage = message;
                    // Update the UI
                });

                hubConnection.On<string>("ChallengeRequestMissed", (message) =>
                {
                    Console.WriteLine("ChallengeRequestMissed");
                    Console.Write(message);

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
