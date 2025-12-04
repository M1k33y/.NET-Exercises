using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.App
{
    internal class CommandLoop
    {
        public static async Task RunAsync(CommandRouter router, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (input is null)
                    continue;

                if (input.Trim().ToLower() == "exit")
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }

                await router.ExecuteAsync(input, token);
            }
        }
    }
}
