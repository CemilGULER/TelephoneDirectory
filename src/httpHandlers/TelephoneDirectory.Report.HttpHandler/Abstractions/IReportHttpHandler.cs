using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Report.Contracts.Dto;

namespace TelephoneDirectory.Report.HttpHandler.Abstractions
{
    public interface IReportHttpHandler
    {
        Task CompletedReport(CompletedReportRequest completedReportRequest, CancellationToken cancellationToken);
    }
}
