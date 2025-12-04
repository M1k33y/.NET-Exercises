
using MiniDeepThought.src.Domain;
using MiniDeepThought.src.Services;
using MiniDeepThought.src.Strategies;
using MiniDeepThought.src.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiniDeepThought
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("=== Mini Deep Thought ===");
            Console.WriteLine();

            var store = new JobStore();
            var runner = new JobRunner(store, new IAnswerStrategy[]
            {
                new TrivialStrategy(),
                new SlowCountStrategy(),
                new RandomGuessStrategy()
            });

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1) Submit Question");
                Console.WriteLine("2) List Jobs");
                Console.WriteLine("3) View Result by JobId");
                Console.WriteLine("4) Cancel Running Job");
                Console.WriteLine("5) Exit");
                Console.Write("Select: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await SubmitQuestionAsync(runner, store);
                        break;

                    case "2":
                        ListJobs(store);
                        break;

                    case "3":
                        ViewResult(store);
                        break;

                    case "4":
                        CancelRunningJob(runner);
                        break;

                    case "5":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Try again.");
                        break;
                }
            }
        }

        private static async Task SubmitQuestionAsync(JobRunner runner, JobStore store)
        {
            string question = ConsoleHelpers.PromptString(
                "Enter your Ultimate Question (1-200 chars): ",
                1, 200);

            string algorithm = ConsoleHelpers.PromptAlgorithm();

            var job = new Job(question, algorithm);
            store.Add(job);

            Console.WriteLine($"Job queued: {job.JobId}");
            Console.WriteLine("Starting computation...");
            Console.WriteLine("Press 'C' to cancel.");

            // Run job + show progress UI
            var updateTask = Task.Run(async () =>
            {
                while (runner.IsRunning)
                {
                    if (ConsoleHelpers.KeyPressed(ConsoleKey.C))
                    {
                        Console.WriteLine("Cancellation requested...");
                        runner.Cancel();
                    }

                    if (runner.CurrentJob != null)
                    {
                        Console.Write($"\rProgress: {runner.CurrentJob.Progress}%   ");
                    }

                    await Task.Delay(100);
                }
            });

            await runner.RunJobAsync(job);
            await updateTask;

            Console.WriteLine();
            Console.WriteLine($"Job finished with status: {job.Status}");
        }

        private static void ListJobs(JobStore store)
        {
            Console.WriteLine();
            Console.WriteLine("JobId | Status | Algorithm | CreatedUtc | Progress");
            Console.WriteLine("-------------------------------------------------------");

            foreach (var j in store.ListAll())
            {
                Console.WriteLine(
                    $"{j.JobId} | {j.Status} | {j.AlgorithmKey} | {j.CreatedUtc:u} | {j.Progress}%");
            }
        }

        private static void ViewResult(JobStore store)
        {
            Guid id = ConsoleHelpers.PromptGuid("Enter JobId: ");
            var job = store.GetById(id);

            if (job == null)
            {
                Console.WriteLine("Job not found.");
                return;
            }

            if (job.Status != JobStatus.Completed)
            {
                Console.WriteLine($"Job is not completed. Current status: {job.Status}");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("{");
            Console.WriteLine($"  \"jobId\": \"{job.JobId}\",");
            Console.WriteLine($"  \"answer\": \"{job.Result?.Answer}\",");
            Console.WriteLine($"  \"summary\": \"{job.Result?.Summary}\",");
            Console.WriteLine($"  \"durationMs\": {job.Result?.DurationMs}");
            Console.WriteLine("}");
        }

        private static void CancelRunningJob(JobRunner runner)
        {
            if (!runner.IsRunning)
            {
                Console.WriteLine("No job is currently running.");
                return;
            }

            Console.WriteLine("Canceling current job...");
            runner.Cancel();
        }
    }
}
