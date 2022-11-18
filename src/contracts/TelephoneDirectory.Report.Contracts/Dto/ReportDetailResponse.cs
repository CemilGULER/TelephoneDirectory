using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Report.Contracts.Dto
{
    public class ReportDetailResponse
    {
        public string LocationName { get; set; }
        public int LocationPersonCount { get; set; }
        public int LocationPhoneNumberCount { get; set; }
    }
}
