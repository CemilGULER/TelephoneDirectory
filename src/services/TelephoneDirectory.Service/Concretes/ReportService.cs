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
        public async Task ReportRequest(CancellationToken cancellationToken)
        {
            var report = new Data.Entities.Report
            {
                CreatedAt = DateTime.Now,
                ReportStatus = false
            };
            await dbContext.Reports.AddAsync(report, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            rabbitProducer.BasicPublish("report-request", report.Id.ToString(), cancellationToken);
        }
        public async Task<List<ReportListResponse>> ReportList(CancellationToken cancellationToken)
        {
            var reportList = dbContext.Reports.Where(x => x.IsDeleted == false);
            return await reportList.Select(rpr => mapper.Map<ReportListResponse>(rpr)).ToListAsync(cancellationToken);
        }
        public async Task CompletedReport(Guid id, CancellationToken cancellationToken)
        {
            var report = await dbContext.Reports.SingleAsync(x => x.Id == id, cancellationToken);
            report.ReportStatus = true;
            dbContext.Reports.Update(report);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<ReportDetailResponse>> ReportDetail(Guid id, CancellationToken cancellationToken)
        {
            var reportDetail = await dbContext.Reports.FirstAsync(x => x.Id == id, cancellationToken);
            if (reportDetail.ReportStatus == false)
                throw new Exception("Henüz rapor hazırlanmadı");
            var query = dbContext.
                   PersonContacts.
                   Where(x => x.IsDeleted == false && x.ContactType == Data.Entities.Enum.ContactType.Location).
                   GroupBy(x => x.ContactDetail).
                   Select(g => new ReportDetailResponse
                   {
                       LocationName = g.Key,

                   });
            return await query.ToListAsync(cancellationToken);
        }
    }
}
