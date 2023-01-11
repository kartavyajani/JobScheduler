using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedulers2._0.api.Models;
using Schedulers2._0.api.Models.Pagination;
using Schedulers2._0.api.Repository;

namespace Schedulers2._0.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSchedulerController : ControllerBase
    {
        private readonly IJobScheduler _ijobScheduler;

        public JobSchedulerController(IJobScheduler ijobScheduler)
        {
            _ijobScheduler = ijobScheduler;
        }

        [HttpPost("jobSchedule")]
        [AllowAnonymous]
        public async Task<JobSchedulerModel> createJobScheduler(JobSchedulerModel jobScheduler)
        {
            if (jobScheduler.jobSchedulerId != 0)
            {

                var resp = await _ijobScheduler.updateMstJobScheduleModel(jobScheduler);
                return resp;
            }
            else
            {
                var resp = await _ijobScheduler.createJobScheduler(jobScheduler);
                return resp;
            }




        }

        [HttpGet("jobScheduler")]
        [AllowAnonymous]
        public async Task<PagedResponse<List<JobSchedulerModel>>> getAllMstCountry(int pageNumber, int pageSize)
        {

            var paginationFilter = new PaginationFilter(pageNumber, pageSize, HttpContext.Request.Path);
            // var resp = await _mediatR.Send(new ApplicationQuerry(paginationFilter));
            var resp = await _ijobScheduler.getAllMstJobScheduleModel(paginationFilter);
            return resp;

        }
        [HttpGet("jobScheduler/{id}")]
        [AllowAnonymous]
        public async Task<JobSchedulerModel> getbyIdMstJobScheduleModel(int id)

        {

            var resp = await _ijobScheduler.getbyIdMstJobScheduleModel(id);
            return resp;

        }
        [HttpDelete("jobScheduler/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> deleteMstJobScheduleModel(int id)
        {
            var resp = _ijobScheduler.deleteMstJobScheduleModel(id);
            return Ok(resp);

        }
    }
}