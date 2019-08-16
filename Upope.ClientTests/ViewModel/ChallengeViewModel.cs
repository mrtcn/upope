using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.ClientTests.ViewModel
{
    public class ChallengeViewModel
    {
        HubConnection chatHubConnection;
        HubConnection challengeHubConnection;
        HubConnection gameHubConnection;
        public ChallengeViewModel(string accessToken)
        {
            // localhost for UWP/iOS or special IP for Android
            var challengeIp = "challenge.upope.com";
            var gameIp = "game.upope.com";
            var chatIp = "localhost:9100";
            //var ip = "localhost:56224";

            try
            {
                chatHubConnection = new HubConnectionBuilder()
                    .WithUrl($"http://{chatIp}/chathub", options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(accessToken);
                    }).Build();

                //challengeHubConnection = new HubConnectionBuilder()
                //    .WithUrl($"http://{challengeIp}/challengehub", options =>
                //    {
                //        options.AccessTokenProvider = () => Task.FromResult(accessToken);
                //    }).Build();

                //gameHubConnection = new HubConnectionBuilder()
                //    .WithUrl($"http://{gameIp}/gamehub", options =>
                //    {
                //        options.AccessTokenProvider = () => Task.FromResult(accessToken);
                //    }).Build();
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
        public async Task SendChatMessage(string userId, string message, int chatRoomId)
        {
            try
            {
                await chatHubConnection.SendAsync("SendMessage", chatRoomId, userId, message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task SendChallenge()
        {
            try
            {
                Thread.Sleep(5000);
                await challengeHubConnection.SendAsync("SendMessage", "user1", "message");
                await challengeHubConnection.InvokeAsync("SendMessage", "user2", "user2 message");
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }
            
        }

        public async Task ChallengeConnect()
        {
            try
            {
                await challengeHubConnection.StartAsync();

                challengeHubConnection.On<string>("ChallengeCreated", (message) =>
                {
                    Console.WriteLine("ChallengeCreated throug ChallengeHubs");
                    Console.Write(message);

                    var finalMessage = message;
                    // Update the UI
                });

                challengeHubConnection.On<string>("ChallengeRequestReceived", (message) =>
                {
                    Console.WriteLine("ChallengeRequestReceived");
                    Console.Write(message);

                    var finalMessage = message;
                    // Update the UI
                });

                challengeHubConnection.On<string>("ChallengeRequestAccepted", (message) =>
                {
                    Console.WriteLine("ChallengeRequestAccepted");
                    Console.Write(message);

                    var finalMessage = message;
                    // Update the UI
                });

                challengeHubConnection.On<string>("ChallengeRequestRejected", (message) =>
                {
                    Console.WriteLine("ChallengeRequestRejected");
                    Console.Write(message);

                    var finalMessage = message;
                    // Update the UI
                });

                challengeHubConnection.On<string>("ChallengeRequestMissed", (message) =>
                {
                    Console.WriteLine("ChallengeRequestMissed");
                    Console.Write(message);

                    var finalMessage = message;
                    // Update the UI
                });
                
            }
            catch (Exception)
            {
                // Something has gone wrong
            }
        }

        public async Task SendMessage(string user, string message)
        {
            try
            {
                await challengeHubConnection.InvokeAsync("SendMessage", user, message);
            }
            catch (Exception ex)
            {
                // send failed
            }
        }

        public async Task GameConnect()
        {
            await gameHubConnection.StartAsync();

            gameHubConnection.On<string>("GameCreated", (message) =>
            {
                Console.WriteLine("GameCreated throug GameHubs");
                Console.Write(message);

                var finalMessage = message;
                // Update the UI
            });

            gameHubConnection.On<string>("RoundEnds", (message) =>
            {
                Console.WriteLine("RoundEnds");
                Console.Write(message);

                var finalMessage = message;
                // Update the UI
            });

            gameHubConnection.On<string>("GameEnds", (message) =>
            {
                Console.WriteLine("GameEnds");
                Console.Write(message);

                var finalMessage = message;
                // Update the UI
            });

            gameHubConnection.On<string>("AskBluff", (message) =>
            {
                Console.WriteLine("AskBluff");
                Console.Write(message);

                var finalMessage = message;
                // Update the UI
            });

            gameHubConnection.On<string>("TextBluff", (message) =>
            {
                Console.WriteLine("TextBluff");
                Console.Write(message);

                var finalMessage = message;
                // Update the UI
            });
        }

        public async Task ChatConnect()
        {
            try
            {
                await chatHubConnection.StartAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            chatHubConnection.On<string>("ReceiveMessage", (message) =>
            {
                Console.WriteLine("ReceiveMessage throug ChatHubs");
                Console.Write(message);

                var finalMessage = message;
                // Update the UI
            });
        }
    }
}
