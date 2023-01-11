using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Schedulers2._0.api.Models;
using Schedulers2._0.api.Models.Pagination;

namespace Schedulers2._0.api.Repository
{
    public interface IJobScheduler
    {
        public Task<JobSchedulerModel> createJobScheduler(JobSchedulerModel jobScheduler);
        public Task<PagedResponse<List<JobSchedulerModel>>> getAllMstJobScheduleModel(PaginationFilter paginationFilter);
  
        public Task<JobSchedulerModel> updateMstJobScheduleModel(JobSchedulerModel JobSchedulerModel);

        public Task<JobSchedulerModel> getbyIdMstJobScheduleModel(int id);

        public JobSchedulerModel deleteMstJobScheduleModel(int id);

    }
}