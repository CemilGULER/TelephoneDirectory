using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Report.Contracts.Dto;

namespace TelephoneDirectory.Service.Abstractions
{
    public interface IReportService
    {
        Task ReportRequest( CancellationToken cancellationToken);
        Task<List<ReportListResponse>> ReportList(CancellationToken cancellationToken);
        Task CompletedReport(Guid id, CancellationToken cancellationToken);
        Task<List<ReportDetailResponse>> ReportDetail(Guid id,CancellationToken cancellationToken);
    }
}
