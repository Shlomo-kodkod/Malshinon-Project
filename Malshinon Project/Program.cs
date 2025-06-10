using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class Program
    {
        public static void Main()
        {
            PeopleDAL pd = new PeopleDAL();
            IntelReportDAL ird = new IntelReportDAL();
            PersonIdentification pi = new PersonIdentification();
            IntelSubmission ins = new IntelSubmission();

            int id = pi.ReporterLogin(pd);
            ins.SubmitReport(pd, ird, id);





        }
    }
}
