using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class PersonIdentification
    {
        internal bool IsValidName(string name)
        {
            if ((name.Length < 1) || (name == " ") || name is null)
            {
                return false;
            }
            foreach (char ch in name)
            {
                if ((!char.IsLetter(ch)) && ( ch != ' '))
                {
                    Console.WriteLine("Invalid name. Please try again.");
                    return false;
                }
            }
            return true;
        }

        internal string GetFirstName(PeopleDAL peopleDal)
        {
            string currName = "";
            do
            {
                Console.WriteLine("Please enter your first name: ");
                currName = Console.ReadLine();
            }
            while ((!IsValidName(currName)) || (!peopleDal.IsUniqueName(currName)));

            currName = currName.ToLower();
            return char.ToUpper(currName[0]) + currName.Substring(1);
        }

        internal string GetExistingFirstName(PeopleDAL peopleDal)
        {
            string currName = "";
            do
            {
                Console.WriteLine("Please enter your first name: ");
                currName = Console.ReadLine();
            }
            while (!IsValidName(currName));

            currName = currName.ToLower();
            return char.ToUpper(currName[0]) + currName.Substring(1);
        }

        internal string GetLastName()
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


        internal int ReporterLogin(PeopleDAL peopleDal)
        { 
            int reported_id = 0;
            string firstName = GetExistingFirstName(peopleDal);
            string lastName = GetLastName();

            if (!peopleDal.IsPeopleExist(firstName,lastName))
            {
                if (!peopleDal.IsUniqueName(firstName))
                {
                    Console.WriteLine("This name is already taken. Please try again with a different name.");
                    firstName = GetFirstName(peopleDal);
                    lastName = GetLastName();
                }
                peopleDal.AddPeople("reporter", firstName, lastName);
            }

            reported_id = peopleDal.GetIdByName(firstName, lastName);
            return reported_id;
        }

        internal string SecretCodeLogin(PeopleDAL peopleDal)
        {
            string firstName = GetExistingFirstName(peopleDal);
            string lastName = GetLastName();
            string secretCode;

            if (peopleDal.IsPeopleExist(firstName, lastName))
            {
                secretCode = peopleDal.GetSecretCode(firstName, lastName);
                return secretCode;
            }
            return "This name is not registered in the system";
        }
    }
}

