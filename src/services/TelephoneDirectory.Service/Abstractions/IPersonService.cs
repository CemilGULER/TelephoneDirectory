using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Contact.Contracts.Dto;
using TelephoneDirectory.Report.Contracts.Dto;

namespace TelephoneDirectory.Service.Abstractions
{
    public  interface IPersonService
    {
        Task<Guid> AddPerson(AddPersonRequest addPersonRequest, CancellationToken cancellationToken);
        Task RemovePerson(Guid id, CancellationToken cancellationToken);
        Task<List<SearchPersonResponse>> SearchPerson(SearchPersonRequest searchPerson, CancellationToken cancellationToken);
        Task<PersonDetailResponse> PersonDetail(Guid id, CancellationToken cancellationToken);
        Task<List<LocationDetailResponse>> LocationDetail( CancellationToken cancellationToken);
    }
}
