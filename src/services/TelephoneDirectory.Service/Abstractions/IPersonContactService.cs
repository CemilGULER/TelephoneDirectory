using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Contact.Contracts.Dto;

namespace TelephoneDirectory.Service.Abstractions
{
    public interface IPersonContactService
    {
        Task<Guid> AddPersonContact(AddPersonContactRequest addPersonContactRequest, CancellationToken cancellationToken);
        Task RemovePersonContact(Guid id, CancellationToken cancellationToken);
    }
}
