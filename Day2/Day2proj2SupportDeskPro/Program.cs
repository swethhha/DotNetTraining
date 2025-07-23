using System;
using System.Collections.Generic;
using Day2Proj2SupportDeskPro.Models;

namespace Day2Proj2SupportDeskPro
{
    class Program
    {
        static void Main(string[] args)
        {

            BugReport bug = new BugReport();
            bug.Id = 1;
            bug.Title = "App Crash";
            bug.Description = "App crashes on login.";
            bug.CreatedBy = "Tester";
            bug.Severity = "High";

            FeatureRequest feature = new FeatureRequest();
            feature.Id = 2;
            feature.Title = "Dark Mode";
            feature.Description = "Add dark mode to UI.";
            feature.CreatedBy = "Product Owner";
            feature.RequestedBy = "User Community";
            feature.ETA = DateTime.Now.AddDays(7);


            List<SupportTicket> tickets = new List<SupportTicket>();
            tickets.Add(bug);
            tickets.Add(feature);

            foreach (SupportTicket ticket in tickets)
            {
                Console.WriteLine("------ Ticket Details ------");
                ticket.DisplayDetails();

 
                if (ticket is IReportable)
                {
                    ((IReportable)ticket).ReportStatus();
                }


                ticket.CloseTicket();

                Console.WriteLine("------ After Closing ------");
                if (ticket is IReportable)
                {
                    ((IReportable)ticket).ReportStatus();
                }

                Console.WriteLine();
            }

            Console.WriteLine("All tickets processed.");
        }
    }
}
