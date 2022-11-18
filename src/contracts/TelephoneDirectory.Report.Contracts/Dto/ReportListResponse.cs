using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Report.Contracts.Dto
{
    public class ReportListResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }
}
