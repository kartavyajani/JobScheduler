using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Schedulers2._0.api.Models
{
    public class CommonEntity
    {
        [Column("is_active", TypeName = "BIT")]
        [DefaultValue("true")]
        public Boolean? active { get; set; } = true;
        [Column("created_by")]
        public int? createdBy { get; set; }
        [Column("created_date")]
        public DateTime? createdDate { get; set; } = DateTime.UtcNow;
        [Column("updated_by")]
        public int? updatedBy { get; set; }
        [Column("updated_date")]
        public DateTime? updatedDate { get; set; } = DateTime.UtcNow;
        [DefaultValue("false")]
        [Column("is_deleted", TypeName = "BIT")]
        public Boolean? deleted { get; set; } = false;
        [DefaultValue("0")]
        [Column("version", TypeName = "varchar(1)")]
        public string? version { get; set; } 

        [Column("description", TypeName = "varchar(100)")]
        public string? description { get; set; }
    }
}