using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Data.Entities.Abstractions;

namespace TelephoneDirectory.Data.Entities
{
    /// <summary>
    /// Kişi tablosu
    /// </summary>
    public class Person : AuditEntityBase
    {
        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(64)]
        public string Surname { get; set; }
        public string Company { get; set; }

        public virtual ICollection<PersonContact> PersonContacts { get; set; }
    }
}
