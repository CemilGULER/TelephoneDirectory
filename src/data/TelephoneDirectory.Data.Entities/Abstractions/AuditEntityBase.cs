using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Data.Entities.Abstractions
{
    /// <summary>
    /// Tüm entitiylerin türetildiği base entiti 
    /// </summary>
    public abstract class AuditEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        [DefaultValue(typeof(DateTime), "current_time()")]
        public DateTime CreatedAt { get; set; }

        [StringLength(64)]
        public string? CreatedBy { get; set; }

        [DefaultValue(typeof(DateTime), "current_time()")]
        public DateTime ModifiedAt { get; set; }

        [StringLength(64)]
        public string? ModifiedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        [StringLength(64)]
        public string? DeletedBy { get; set; }
    }
}
