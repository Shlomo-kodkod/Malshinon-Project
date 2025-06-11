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
            foreach (char ch in name)
            {
                if ((!char.IsLetter(ch)) && ( ch != ' '))
                {
                    Console.WriteLine("Invalid name pelease try again");
                    return false;
                }
            }
            return true;
        }

        public string GetFirstName(PeopleDAL peopleDal)
        {
            string currName = "";
            do
            {
                Console.WriteLine("Please enter your first name: ");
                currName = Console.ReadLine();
            }
            while ((!IsValidName(currName)) || (!peopleDal.IsUniqueName(currName)));

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


        public int ReporterLogin(PeopleDAL peopleDal)
        {
            string firstName = GetFirstName(peopleDal);
            string lastName = GetLastName();
            int reported_id = 0;


            if (! peopleDal.IsPeopleExsist(firstName,lastName))
            {
                peopleDal.AddPeople("reporter", firstName, lastName);
                reported_id = peopleDal.GetIdByname(firstName, lastName);
            }
            return reported_id;
        }

        public string SecretCodeLogin(PeopleDAL peopleDal)
        {
            string firstName = GetFirstName(peopleDal);
            string lastName = GetLastName();
            string secretCode;

            if (peopleDal.IsPeopleExsist(firstName, lastName))
            {
                secretCode = peopleDal.GetSecretCode(firstName, lastName);
                return secretCode;
            }
            return "This name is not registered in the system";
        }
    }
}

