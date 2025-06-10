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

            return currName;
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

            return currName;
        }

        public void ReporterLogin(PeopleDAL peopleDal)
        {
            string firstName = GetFirsName(peopleDal);
            string lastName = GetLastName();

            if (! peopleDal.IsPeopleExsist(firstName,lastName))
            {

            }
        }
    }
}

