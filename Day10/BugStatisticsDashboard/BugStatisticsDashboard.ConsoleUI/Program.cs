using System;
using BugStatisticsDashboard.Application.Services;
using BugStatisticsDashboard.Core.Interface;
using BugStatisticsDashboard.Infrastructure.Repositories;

namespace BugStatisticsDashboard.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            IBugRepository bugRepository = new BugRepository();
            BugStatisticsService bugStatisticsService = new(bugRepository);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nBug Dashboard");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Bug Count by Status");
                Console.WriteLine("2. Bug Count by Project and Priority");
                Console.WriteLine("3. Daily Bug Report");
                Console.WriteLine("4. Top Bug Creators");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                Console.WriteLine(); // for spacing

                switch (option)
                {
                    case "1":
                        bugStatisticsService.ShowBugCountByStatus();
                        break;
                    case "2":
                        bugStatisticsService.ShowBugCountByProjectAndPriority();
                        break;
                    case "3":
                        bugStatisticsService.ShowDailyBugReport();
                        break;
                    case "4":
                        bugStatisticsService.ShowTopCreators();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
