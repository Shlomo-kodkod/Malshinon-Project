using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class AnalysisMenu
    {
        public void PrintError(int listLen)
        {

            if (listLen == 0)
            {
                Console.WriteLine("No information was found in the system");
            }
        }
        public void DisplayPotentialAgents(PeopleDAL peopleDal, IntelReportDAL intelReportDal)
        {
            List<People> potentialAgent = peopleDal.GetAllPotentialAgents();
            PrintError(potentialAgent.Count);

            foreach(People p in potentialAgent)
            {
                int id = peopleDal.GetIdByname(p.firstName, p.lastName);
                double avg = intelReportDal.GetAvgTextLen(id);
                Console.WriteLine(p.firstName + " " + p.lastName + "\n" +
                    p.numReports + "\n" +
                    avg + "\n");
            }
        }

        public void DisplayDangerousTargets(PeopleDAL peopleDal, IntelReportDAL intelReportDal)
        {
            List<People> dangerousTarget = peopleDal.GetAllDangerousTargets();
            PrintError(dangerousTarget.Count);

            foreach (People p in dangerousTarget)
            {
                Console.WriteLine(p.firstName + " " + p.lastName + "\n" +
                    p.numMentions + "\n");
            }
        }

        public void DisplayActiveAlerts(AlertsDal alertsDal)
        {
            List<Alerts> activeAlerts = alertsDal.GetAllAlerts();
            PrintError(activeAlerts.Count);

            foreach(Alerts a in activeAlerts)
            {
                Console.WriteLine(a.targetId + "\n" +
                    a.creatAt + "\n" +
                    a.reason + "\n");
            }
        }






    }
}
