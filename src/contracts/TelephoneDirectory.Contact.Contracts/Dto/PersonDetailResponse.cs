using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Contact.Contracts.Dto
{
    public class PersonDetailResponse: PersonInfo
    {
        public List<PersonContactInfo> PersonContacts { get; set; }
    }
}
