using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project
{
    internal class IntelReport
    {
        int id;
        int reporterId;
        int targetId;
        string text;
        DateTime timestemp;

        internal IntelReport(int Id, int ReporterId, int TargetId, string Text, DateTime TimeStemp)
        {
            this.id = Id;
            this.reporterId = ReporterId;
            this.targetId = TargetId;
            this.text = Text;
            this.timestemp = TimeStemp;
        }

        internal IntelReport() { }
    }
}
