using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class People
    {
        internal int id;
        internal string firstName;
        internal string lastName;
        internal string secretCode;
        internal string type;
        internal int numReports;
        internal int numMentions;

        internal People(int Id, string FirstName, string LastName, string SecretCode, string NewType, int NumReports, int NumMentions)
        {
            this.id = Id;
            this.firstName = FirstName;
            this.lastName = LastName;
            this.secretCode = SecretCode;
            this.type = NewType;
            this.numReports = NumReports;
            this.numMentions = NumMentions;
        }

    }
}
