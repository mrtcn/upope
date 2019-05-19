using System;
using System.IO;
using System.Text;
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
            var accessToken = ReadLine().Replace("\r\n", "");

            var challengeViewModel = new ChallengeViewModel(accessToken);
            //await challengeViewModel.ChallengeConnect();
            await challengeViewModel.GameConnect();
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
            //Console.WriteLine(outputLength);
            char[] chars = Encoding.UTF7.GetChars(bytes, 0, outputLength);
            return new string(chars);
        }
    }
}
