using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class Alerts
    {
        internal int id;
        internal int targetId;
        internal DateTime createAt;
        internal string reason;

        internal Alerts(int Id, int TargetId, DateTime CreatAt, string Reason)
        {
            this.id = Id;
            this.targetId = TargetId;
            this.createAt = CreatAt;
            this.reason = Reason;
        }
    }
}
