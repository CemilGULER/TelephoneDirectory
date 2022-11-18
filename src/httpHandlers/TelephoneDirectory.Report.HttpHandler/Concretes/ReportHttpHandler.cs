using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Common.Http;
using TelephoneDirectory.Report.Contracts.Dto;
using TelephoneDirectory.Report.HttpHandler.Abstractions;

namespace TelephoneDirectory.Report.HttpHandler.Concretes
{
    public class ReportHttpHandler : TelephoneDirectoryHttpClientBase, IReportHttpHandler
    {
        public ReportHttpHandler(HttpClient httpClient)
            : base(httpClient)
        {

        }
        public async Task CompletedReport(CompletedReportRequest completedReportRequest, CancellationToken cancellationToken)
        {
            await PostAsync<CompletedReportRequest, bool>("api/report/completed-report", completedReportRequest, cancellationToken);
        }
    }
}
