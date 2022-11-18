using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Report.Contracts.Dto
{
    public class CompletedReportRequest
    {
        public Guid Id { get; set; }
        public string? ReportPath { get; set; }
        public string? ReportFilName { get; set; }
    }
}
