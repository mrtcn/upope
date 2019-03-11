using System;
using System.Threading;
using System.Threading.Tasks;
using Upope.ClientTests.ViewModel;

namespace Upope.ClientTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter your AccessToken!"); 
            var accessToken = Console.ReadLine();

            var challengeViewModel = new ChallengeViewModel(accessToken);
            await challengeViewModel.Connect();
            Thread.Sleep(500);
            await challengeViewModel.SendChallenge();
            //challengeViewModel.SendMessage("User XXX", "Message 1");
            //var key = Console.ReadKey();
            //if (key.KeyChar == 1)
            //    Console.WriteLine(1);
            while (true)
            {
                Thread.Sleep(500);
            }

        }
    }
}
