using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Malshinon_Project
{
    internal class IntelSubmission
    {
        public bool CheckTextSize(string text)
        {
            int len = text.Length;

            if (len >= 500)
            {
                Console.WriteLine("Text length error, try agin with shorter text");
                return false;
            }

            if (len <= 0)
            {
                Console.WriteLine("Text empty error, try again");
                return false;
            }
            
            return true;
        }

        public bool IsSpiltAble(string text)
        {
            try
            {
                string[] splitText = text.Split(' ');
                return true;
            }
            catch
            {
                Console.WriteLine("Text error, please don't make a few space in a row");
            }
            return false;
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
                Console.WriteLine(upCaseCnt >= 2 ? "" : "Text error, please enter target name with upper case");
            }
            catch
            {
                Console.WriteLine("Text error, please enter target name with one space between words");
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
            while ((!CheckTextSize(text)) || (!IsSpiltAble(text)) || (!IsContainName(text)));

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
            People peopleRow = peopleDal.GetPeopleRow(report_id);
            double textAvg = intelReport.GetAvgTextLen(report_id);
            if ((peopleRow.numReports >= 10) && (textAvg >= 100))
            {
                peopleDal.UpdateType(report_id, "potential_agent");
            }
            else if (peopleRow.numMentions > 0)
            {
                string newType = peopleRow.numMentions > 0 ? "both" : "reporter";
                peopleDal.UpdateType(report_id, newType);

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

        public void IsTypeUpdate(PeopleDAL peopleDal, int id)
        {
            People peopleRow = peopleDal.GetPeopleRow(id);

            if ((peopleRow.numReports > 0) && (peopleRow.numMentions > 0))
            {
                peopleDal.UpdateType(id,"both");
            } 
        }

        public void SubmitReport(PeopleDAL peopleDal, IntelReportDAL intelReportDal, AlertsDal alertsDal, int reported_id)
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
            IsPotentialAgent(peopleDal, intelReportDal, reported_id);
            IsTypeUpdate(peopleDal, target_id);
            alertsDal.UpdateAlerts(intelReportDal,target_id);
        }
        
    }
}
