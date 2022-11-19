using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirectory.Report.Contracts.Dto;
using TelephoneDirectory.Service.Abstractions;

namespace TelephoneDirectory.Report.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;

        }
        /// <summary>
        /// Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor  talebi
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("report-request")]
        public async Task<IActionResult> ReportRequest(CancellationToken cancellationToken)
        {
            var reportRequestId= await reportService.ReportRequest(cancellationToken);
            return Ok(reportRequestId);
        }

        /// <summary>
        ///Sistemin oluşturduğu raporların listelenmesi 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("requested-report-list")]
        public async Task<IActionResult> ReportList(CancellationToken cancellationToken)
        {
            return Ok(await reportService.ReportList(cancellationToken));
        }

        /// <summary>
        /// Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("report-detail")]
        public async Task<IActionResult> ReportDetail(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await reportService.ReportDetail(id, cancellationToken));
        }
        /// <summary>
        /// BackgroundService'lerde oluşturduktan sonra rapor detaylarının setlendiği servis
        /// </summary>
        /// <param name="completedReportRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [HttpPost("completed-report")]
        public async Task<IActionResult> CompletedReport([FromBody]CompletedReportRequest completedReportRequest, CancellationToken cancellationToken)
        {
            await reportService.CompletedReport(completedReportRequest, cancellationToken);
            return Ok();
        }
    }
}
