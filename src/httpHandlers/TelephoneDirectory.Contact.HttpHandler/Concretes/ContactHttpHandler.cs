using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Common.Http;
using TelephoneDirectory.Contact.HttpHandler.Abstractions;
using TelephoneDirectory.Report.Contracts.Dto;

namespace TelephoneDirectory.Contact.HttpHandler.Concretes
{
    public class ContactHttpHandler : TelephoneDirectoryHttpClientBase, IContactHttpHandler
    {
        public ContactHttpHandler(HttpClient httpClient)
            : base(httpClient)
        {

        }
        public Task<List<CompletedReportResponse>> LocationDetail(CancellationToken cancellationToken)
        {
            return GetAsync<List<CompletedReportResponse>>($"api/person/location-detail", cancellationToken);
        }
    }
}
