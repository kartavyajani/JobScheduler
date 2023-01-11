using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Schedulers2._0.api.Models
{
     [Table("job_scheduler")]
    public class JobSchedulerModel : CommonEntity
    {
        [Key]
        [Column("jobScheduler_Id")]
        public int jobSchedulerId { get; set; }

        [Column("job_Name")]
        public string? jobName { get; set; }

        [Column("job_Url")]
        public string? jobUrl { get; set; }

        [Column("job")]
        public string? job { get; set; }



        [Column("interval")]
        public string? interval { get; set; }

        [Column("job_Id")]
        public int jobId { get; set; }
    }
}