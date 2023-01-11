using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedulers2._0.api.Models;

namespace Schedulers2._0.api.Data
{
    public class DotnetDbContext : DbContext
    {
        public DotnetDbContext()
        {
        }
            public DotnetDbContext(DbContextOptions options) : base(options)
        {
        } 

          public DbSet<JobSchedulerModel> jobScheduler { get; set; }
    }
}