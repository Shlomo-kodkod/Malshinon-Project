using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class Alerts
    {
        public int id;
        public int targetId;
        public DateTime creatAt;
        public string reason;

        public Alerts(int Id, int TargetId, DateTime CreatAt, string Reason)
        {
            this.id = Id;
            this.targetId = TargetId;
            this.creatAt = CreatAt;
            this.reason = Reason;
        }
    }
}
