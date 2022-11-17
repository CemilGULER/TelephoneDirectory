using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Data.Entities.Abstractions;
using TelephoneDirectory.Data.Entities.Enum;

namespace TelephoneDirectory.Data.Entities
{
    /// <summary>
    /// Kişi iletişim bilgisinin tutulduğu tablodur
    /// </summary>
    public class PersonContact : AuditEntityBase
    {
        public Guid PersonId { get; set; }
        public ContactType? ContactType { get; set; }
        public string ContactDetail { get; set; }

        public virtual Person Person { get; set; }
    }

    public class PersonContactEntityConfiguration : IEntityTypeConfiguration<PersonContact>
    {
        public void Configure(EntityTypeBuilder<PersonContact> builder)
        {
            builder.HasOne(x => x.Person)
                 .WithMany(x => x.PersonContacts)
                 .HasForeignKey(x => x.PersonId)
                 .OnDelete(DeleteBehavior.SetNull);

          
        }
    }
}
