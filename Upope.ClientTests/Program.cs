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
            Console.WriteLine("Hello World!");

            Console.WriteLine("Enter your accessToken");
            var accessToken = Console.ReadLine();

            var challangeViewModel = new ChallengeViewModel(accessToken);


            await challangeViewModel.Connect();
            Thread.Sleep(500);
            //challangeViewModel.SendMessage("User XXX", "Message 1");
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
