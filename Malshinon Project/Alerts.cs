using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class Alerts
    {
        int id;
        int targetId;
        DateTime creatAt;
        string reason;

        public Alerts(int Id, int TargetId, DateTime CreatAt, string Reason)
        {
            this.id = Id;
            this.targetId = TargetId;
            this.creatAt = CreatAt;
            this.reason = Reason;
        }
    }
}
