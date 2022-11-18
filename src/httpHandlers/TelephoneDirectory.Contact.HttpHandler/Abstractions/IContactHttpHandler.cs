using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Report.Contracts.Dto;

namespace TelephoneDirectory.Contact.HttpHandler.Abstractions
{
    public interface IContactHttpHandler
    {
        Task<List<CompletedReportResponse>> LocationDetail(CancellationToken cancellationToken);
    }
}
