using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class Menu
    {
        private static string[] menuOptions = new string[] { "0", "1", "2" , "3", "4", "5"};
        private static PeopleDAL peopleDal = new PeopleDAL();
        private static IntelReportDAL intelReporterDal = new IntelReportDAL();
        private static PersonIdentification personIdentification = new PersonIdentification();
        private static IntelSubmission intelSubmission = new IntelSubmission();
        private static AlertsDal alertsDal = new AlertsDal();
        private static AnalysisMenu analysisMenu = new AnalysisMenu();
        internal void DisplayMenu()
        {
            //Thread.Sleep(1000);
            //Console.Clear();
            Console.WriteLine(
                """
                   ____ ___  ___  ____  __  __
                  / __ `__ \/ _ \/ __ \/ / / /
                 / / / / / /  __/ / / / /_/ / 
                /_/ /_/ /_/\___/_/ /_/\__,_/ 
            
                *****************************
                [0] Exit
                [1] Submit new report
                [2] Get your secret code
                [3] Display potential agents
                [4] Display dangerous targets
                [5] Display active alerts
                *****************************
                """ + "\n" 
                );
        }

        internal bool IsValidChoice(string choice)
        {
            if (!menuOptions.Contains(choice))
            {
                Console.WriteLine("Invalid value please try again. ");
            }
            return menuOptions.Contains(choice);
        }

        internal int GetChoice()
        {
            string choice = "-1";
            do
            {
                Console.WriteLine("Enter your choice:");
                choice = Console.ReadLine();
            }
            while (!IsValidChoice(choice));

            return int.Parse(choice);
        }

        internal void MakeChoice(int choice)
        {
            switch(choice)
            {
                case 1:
                    int reporter_id = personIdentification.ReporterLogin(peopleDal);
                    intelSubmission.SubmitReport(peopleDal, intelReporterDal, alertsDal,reporter_id);
                    break;
                case 2:
                    Console.WriteLine(personIdentification.SecretCodeLogin(peopleDal));
                    break;
                case 3:
                    analysisMenu.DisplayPotentialAgents(peopleDal,intelReporterDal);
                    break;
                case 4:
                    analysisMenu.DisplayDangerousTargets(peopleDal,intelReporterDal);
                    break;
                case 5:
                    analysisMenu.DisplayActiveAlerts(alertsDal);
                    break;

            }
        }
        internal void RunMenu()
        {
            int choice = -1;
            do
            {
                DisplayMenu();
                choice = GetChoice();
                MakeChoice(choice);
            }
            while (choice != 0);

            Console.WriteLine("Thank you for using Malshinon. Goodbye!");
        }
    }
}
