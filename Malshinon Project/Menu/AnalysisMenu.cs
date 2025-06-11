using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class AnalysisMenu
    {
        internal void PrintError(int listLen)
        {

            if (listLen == 0)
            {
                Console.WriteLine("No information was found in the system");
            }
        }
        internal void DisplayPotentialAgents(PeopleDAL peopleDal, IntelReportDAL intelReportDal)
        {
            List<People> potentialAgent = peopleDal.GetAllPotentialAgents();
            PrintError(potentialAgent.Count);

            foreach(People p in potentialAgent)
            {
                int id = peopleDal.GetIdByname(p.firstName, p.lastName);
                double avg = intelReportDal.GetAvgTextLen(id);
                Console.WriteLine(
                    "_______________________________________\n" +
                    "Name: " + p.firstName + " " + p.lastName + "\n" +
                    "Repors number: " + p.numReports + "\n" +
                    "Average text length: " + avg + "\n" +
                    "---------------------------------------"
                    );
            }
        }
        internal void DisplayDangerousTargets(PeopleDAL peopleDal, IntelReportDAL intelReportDal)
        {
            List<People> dangerousTarget = peopleDal.GetAllDangerousTargets();
            PrintError(dangerousTarget.Count);

            foreach (People p in dangerousTarget)
            {
                Console.WriteLine(
                    "_______________________________________\n" +
                    "Name: " + p.firstName + " " + p.lastName + "\n" +
                    "Mentions number: " + p.numMentions + "\n" +
                    "---------------------------------------"
                    );
            }
        }

        internal void DisplayActiveAlerts(AlertsDal alertsDal)
        {
            List<Alerts> activeAlerts = alertsDal.GetAllAlerts();
            PrintError(activeAlerts.Count);

            foreach(Alerts a in activeAlerts)
            {
                Console.WriteLine(
                    "_______________________________________\n" + 
                    "ID: " + a.targetId + "\n" +
                    "CREATION TIME: " + a.creatAt + "\n" +
                    "ALERT REASON: " + a.reason + "\n" +
                    "---------------------------------------"
                    );

            }
        }
    }
}
