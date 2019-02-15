using System;
using System.Threading;
using Upope.ClientTests.ViewModel;

namespace Upope.ClientTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var challangeViewModel = new ChallengeViewModel();
            challangeViewModel.Connect();
            Thread.Sleep(500);
            challangeViewModel.SendMessage("User XXX", "Message 1");
            Console.ReadKey();
            
        }
    }
}
