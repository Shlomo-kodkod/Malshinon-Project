using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class Menu
    {
        private static string[] menuOptions = new string[] { "0", "1" };
        private static PeopleDAL peopleDal = new PeopleDAL();
        private static IntelReportDAL intelReporterDal = new IntelReportDAL();
        private static PersonIdentification personIdentification = new PersonIdentification();
        private static IntelSubmission intelSubmission = new IntelSubmission();
        public void DisplayMenu()
        {
            Console.WriteLine(
                """
                   ____ ___  ___  ____  __  __
                  / __ `__ \/ _ \/ __ \/ / / /
                 / / / / / /  __/ / / / /_/ / 
                /_/ /_/ /_/\___/_/ /_/\__,_/ 

                """ + "\n" +
                "[0] Exit \n" +
                "[1] Submit new report"
                );
        }

        public bool IsValidChoice(string choice)
        {
            if (!menuOptions.Contains(choice))
            {
                Console.WriteLine("Invalid value please try again ");
            }
            return menuOptions.Contains(choice);
        }

        public int GetChoice()
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

        public void MakeChoice(int choice)
        {
            switch(choice)
            {
                case 1:
                    int reporter_id = personIdentification.ReporterLogin(peopleDal);
                    intelSubmission.SubmitReport(peopleDal, intelReporterDal, reporter_id);
                    break;

            }
        }

        public void RunMenu()
        {
            int choice = -1;
            do
            {
                DisplayMenu();
                choice = GetChoice();
                MakeChoice(choice);
            }
            while (choice != 0);
        }
    }
}
