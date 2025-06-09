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
            PeopleDAL p = new PeopleDAL();
            People w = p.GetPeopleRow(1);
            Console.WriteLine(w.secretCode);
            //p.UpdateType(1, "target");
            //p.UpdateReportNum(1);
            //Console.WriteLine(p.IsPeopleExsist("hh","mpomo"));
            //p.AddPeople("reporter", "isreal","kkkkk");
        }
    }
}
