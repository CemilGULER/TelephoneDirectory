using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Data.Entities.Abstractions;

namespace TelephoneDirectory.Data.Entities
{
    public class Report : AuditEntityBase
    {
        public bool? ReportStatus { get; set; }
    }
}
