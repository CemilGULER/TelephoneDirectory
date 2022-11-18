using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Contact.Contracts.Enum;

namespace TelephoneDirectory.Contact.Contracts.Dto
{
    public class AddPersonContactRequest
    {
        public Guid PersonId { get; set; }
        public ContactType? ContactType { get; set; }
        public string? ContactDetail { get; set; }
    }
}
