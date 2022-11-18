using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Contact.Contracts.Dto
{
    public class LocationDetailResponse
    {
        public string LocationName { get; set; }
        public int LocationPersonCount { get; set; }
        public int LocationPhoneNumberCount { get; set; }
    }
}
