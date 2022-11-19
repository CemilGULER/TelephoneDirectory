using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Data.Access.Context;
using TelephoneDirectory.Mq.Service.Abstractions;
using TelephoneDirectory.Report.Contracts.Dto;
using TelephoneDirectory.Service.Abstractions;

namespace TelephoneDirectory.Service.Concretes
{
    public class ReportService : IReportService
    {
        private readonly IRabbitProducer rabbitProducer;
        private readonly TelephoneDirectoryDbContext dbContext;
        private readonly IMapper mapper;
        public ReportService(TelephoneDirectoryDbContext dbContext,
                             IRabbitProducer rabbitProducer,
                             IMapper mapper)
        {
            this.rabbitProducer = rabbitProducer;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<Guid> ReportRequest(CancellationToken cancellationToken)
        {
            var report = new Data.Entities.Report
            {
                CreatedAt = DateTime.UtcNow,
                ReportStatus = false
            };
            await dbContext.Reports.AddAsync(report, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            rabbitProducer.BasicPublish("report-request", report.Id.ToString(), cancellationToken);
            return report.Id;
        }
        public async Task<List<ReportListResponse>> ReportList(CancellationToken cancellationToken)
        {
            var reportList = dbContext.Reports.Where(x => x.IsDeleted == false);
            return await reportList.Select(rpr => mapper.Map<ReportListResponse>(rpr)).ToListAsync(cancellationToken);
        }
        public async Task CompletedReport(CompletedReportRequest completedReportRequest, CancellationToken cancellationToken)
        {
            var report = await dbContext.Reports.SingleAsync(x => x.Id == completedReportRequest.Id, cancellationToken);
            report.ReportStatus = true;
            report.ReportPath = completedReportRequest.ReportPath;
            report.ReportFilName = completedReportRequest.ReportFilName;
            dbContext.Reports.Update(report);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<CompletedReportResponse> ReportDetail(Guid id, CancellationToken cancellationToken)
        {
            var reportDetail = await dbContext.Reports.FirstAsync(x => x.Id == id, cancellationToken);
            if (reportDetail.ReportStatus == false)
                throw new Exception("Henüz rapor hazırlanmadı");
            return new CompletedReportResponse
            {
                ReportFilName = reportDetail.ReportFilName,
                ReportPath = reportDetail.ReportPath
            };
        }
    }
}
