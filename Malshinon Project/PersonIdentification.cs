using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class PersonIdentification
    {
        public bool IsValidName(string name)
        {
            if ((name.Length < 1) || (name == " "))
            {
                return false;
            }
            foreach(char ch in name)
            {
                if (! char.IsLetter(ch))
                {
                    Console.WriteLine("Invalid name pelease try again");
                    return false;
                }
            }
            return true;
        }

        public string GetFirsName(PeopleDAL peopleDal)
        {
            string currName = "";
            do
            {
                Console.WriteLine("Please enter your first name: ");
                currName = Console.ReadLine();
            }
            while ((!IsValidName(currName)) && (peopleDal.IsUniqueName(currName)));

            return char.ToUpper(currName[0]) + currName.Substring(1);
        }

        public string GetLastName()
        {
            string currName = "";
            do
            {
                Console.WriteLine("Please enter your last name: ");
                currName = Console.ReadLine();
            }
            while (!IsValidName(currName));

            return char.ToUpper(currName[0]) + currName.Substring(1);
        }

        public void IsTypeUpdate(PeopleDAL peopleDal, int id)
        {
            string currType = peopleDal.GetPeopleRow(id).type;

            if ((currType != "both") && (currType != "potential_agent"))
            {
                peopleDal.UpdateType(id, "both");
            }
        }

        public int ReporterLogin(PeopleDAL peopleDal)
        {
            string firstName = GetFirsName(peopleDal);
            string lastName = GetLastName();
            int reported_id = 0;

            if (peopleDal.IsPeopleExsist(firstName,lastName))
            {
                reported_id = peopleDal.GetIdByname(firstName, lastName);
                IsTypeUpdate(peopleDal, reported_id);
            }

            else if (! peopleDal.IsPeopleExsist(firstName,lastName))
            {
                peopleDal.AddPeople("reporter", firstName, lastName);
                reported_id = peopleDal.GetIdByname(firstName, lastName);
            }
            return reported_id;
        }
    }
}

