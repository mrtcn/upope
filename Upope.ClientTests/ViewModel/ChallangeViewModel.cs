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
                .WithUrl($"https://{ip}:44324/challangehubs", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiYWU2MjM4OS1jOTgxLTRjMTctOGU1Yy01ZmNhZWVmOTMwYjciLCJ1bmlxdWVfbmFtZSI6Im11cmF0Y2FudHVuYTciLCJqdGkiOiI3ZTc3Yzg2MC02ODY4LTQ3MGMtOGJlNS1hYzVhNDhlOWI4ODciLCJpYXQiOiIyMi8wMi8yMDE5IDE0OjEwOjE0IiwibmJmIjoxNTUwODQ0NjE0LCJleHAiOjE1NTA5MzEwMTEsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTUwODAiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDQ0MyJ9.nsYf7gaTgkavenF1CCCCYGuV_16h8V-NnIqo43FvpCw");
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
                    var finalMessage = message;
                    // Update the UI
                });

                hubConnection.On<string>("ChallengeRequestAccepted", (message) =>
                {
                    var finalMessage = message;
                    // Update the UI
                });

                hubConnection.On<string>("ChallengeRequestRemoved", (message) =>
                {
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
