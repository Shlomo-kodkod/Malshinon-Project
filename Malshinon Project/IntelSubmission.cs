using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class IntelSubmission
    {
        public bool IsEmptyText(string text)
        {
            return text.Length <= 0;
        }

        public bool IsContainName(string text)
        {
            int upCaseCnt = 0;
            try
            {
                string[] splitText = text.Split(' ');
                foreach (string word in splitText)
                {
                    if (char.IsUpper(word[0]))
                    {
                        upCaseCnt++;
                    }
                    else if ((!char.IsUpper(word[0])) && (upCaseCnt >= 2))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public string GetReportText()
        {
            string text = "";
            do
            {
                Console.WriteLine("Enter your report: ");
                text = Console.ReadLine();
            }
            while ((!IsEmptyText(text)) && (!IsContainName(text)));

            return text;
        }

        public string[] ExtractNameFromText(string text)
        {
            string[] splitText = text.Split(' ');
            List<string> fullName = new List<string>();

            foreach(string word in splitText)
            {
                if (char.IsUpper(word[0]))
                {
                    fullName.Add(word);
                }
                else if ((!(char.IsUpper(word[0])) && fullName.Count >= 2))
                {
                    break;
                }
            }
            int len = fullName.Count();
            return new string[] { string.Join(" ", fullName.Take(len - 1)), fullName[len - 1] };
        }

        


        public void IsPotentialAgent(PeopleDAL peopleDal, IntelReportDAL intelReport, int report_id)
        {
            int reportNum = peopleDal.GetPeopleRow(report_id).numReports;
            double textAvg = intelReport.GetAvgTextLen(report_id);
            if ((reportNum >= 10) && (textAvg >= 100))
            {
                peopleDal.UpdateType(report_id, "potential_agent");
            }
        }

        public void IsPotentialThreat(PeopleDAL peopleDal,string[] target_name)
        {
            int target_id = peopleDal.GetIdByname(target_name[0], target_name[1]);
            int mentionNum = peopleDal.GetPeopleRow(target_id).numMentions;
            if (mentionNum >= 20)
            {
                Console.WriteLine("Is Potential Threat !");
            }
        }

        public void SubmitReport(PeopleDAL peopleDal, IntelReportDAL intelReportDal, int reported_id)
        {
            string text = GetReportText();
            string[] fullName = ExtractNameFromText(text);
            if (! peopleDal.IsPeopleExsist(fullName[0], fullName[1]))
            {
                peopleDal.AddPeople("target", fullName[0], fullName[1]);
            }
            int target_id = peopleDal.GetIdByname(fullName[0], fullName[1]);
            intelReportDal.AddReport(reported_id, target_id, text);
            peopleDal.UpdateReportNum(reported_id);
            peopleDal.UpdateReportMentions(target_id);
            IsPotentialThreat(peopleDal, fullName);
        }
        
    }
}
