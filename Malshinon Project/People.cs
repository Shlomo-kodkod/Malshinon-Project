using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class People
    {
        static internal string[] types = new string[] { "reporter", "target", "both", "potential_agent" };
        internal int id;
        internal string firstName;
        internal string lastName;
        internal string secretCode;
        internal string type;
        internal int numReports = 0;
        internal int numMentions = 0;

        internal People(string FirstName, string LastName, string SecretCode)
        {
            Guid g = Guid.NewGuid();
            this.firstName = FirstName;
            this.lastName = LastName;
            this.secretCode = Convert.ToBase64String(g.ToByteArray());
            this.type = types[0];

        }


        internal People(string FirstName, string LastName, string SecretCode, string NewType)
        {
            Guid g = Guid.NewGuid();
            this.firstName = FirstName;
            this.lastName = LastName;
            this.secretCode = Convert.ToBase64String(g.ToByteArray());
            this.type = NewType;

        }

    }
}
