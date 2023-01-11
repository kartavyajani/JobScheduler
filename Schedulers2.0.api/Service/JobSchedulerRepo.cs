using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Schedulers2._0.api.Data;
using Schedulers2._0.api.Models;
using Schedulers2._0.api.Models.Pagination;
using Schedulers2._0.api.Repository;
using Schedulers2._0.api.Service.Utils;

namespace Schedulers2._0.api.Service
{
    public class JobSchedulerRepo : IJobScheduler


    {
        private readonly Utilities _utilities;

        private readonly DotnetDbContext _dotNetDBcontext;

        public JobSchedulerRepo(Utilities utilities, DotnetDbContext dotNetDBcontext)
        {
            _utilities = utilities;
            _dotNetDBcontext = dotNetDBcontext;
        }

        public async Task<JobSchedulerModel> createJobScheduler(JobSchedulerModel jobScheduler)
        {
            IDbContextTransaction transaction = null;
            {
                try
                {
                    var schedulerName = await _dotNetDBcontext.jobScheduler.AddAsync(jobScheduler);
                    await _dotNetDBcontext.SaveChangesAsync();
                    Console.WriteLine("mstAreaResp.Entity.areaId====>>>" + schedulerName.Entity.jobSchedulerId);
                    // await insertDataToAreaNames(mstAreaDto.listOfAreaNames!, mstAreaResp.Entity.areaId);
                    var jobName = schedulerName.Entity.jobSchedulerId + jobScheduler.jobName;
                    if (jobScheduler.active == false)
                    {
                        // Console.WriteLine("Inside false active " + JobSchedulerModel.active);

                        RecurringJob.RemoveIfExists(jobName);
                    }
                    else
                    {
                        RecurringJob.AddOrUpdate(jobName, () => Console.WriteLine("Schedule is running for job" + jobScheduler.jobName), jobScheduler.interval);
                    }

                    return jobScheduler;
                }

                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        Console.WriteLine("Error for inside ", transaction);
                        await transaction.RollbackAsync();
                    }
                    else
                    {
                        Console.WriteLine("Else is called===>>");
                    }

                    throw new Exception();
                }
                finally
                {
                    if (transaction != null)
                    {
                        await transaction.DisposeAsync();
                    }

                }
            }

        }

        public JobSchedulerModel deleteMstJobScheduleModel(int id)
        {
            JobSchedulerModel? jobSchedulerModel = _dotNetDBcontext.jobScheduler.Find(id);
            if (jobSchedulerModel != null)
            {
                jobSchedulerModel.deleted = true;
                _dotNetDBcontext.jobScheduler.Update(jobSchedulerModel);
                _dotNetDBcontext.SaveChanges();
                var jobName = id + jobSchedulerModel.jobName;
                RecurringJob.RemoveIfExists(jobName);
                return jobSchedulerModel;
            }
            else
            {
                throw new Exception();
            }
        }



        public async Task<PagedResponse<List<JobSchedulerModel>>> getAllMstJobScheduleModel(PaginationFilter paginationFilter)
        {
            var totalRecords = (from jobschedule in _dotNetDBcontext.jobScheduler where jobschedule.deleted == false select jobschedule).Count();

            var pageRespDic = _utilities.generateForPageURL(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.applicationPath);
            var resp = await (from jobschedule in _dotNetDBcontext.jobScheduler where jobschedule.deleted == false select jobschedule)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();
            // var previousPage = paginationFilter.PageNumber - 1 <= 0 ? 0 : paginationFilter.PageNumber - 1;
            //var totalRecords = await _dotNetDBcontext.mst_country.CountAsync();
            var testResp = new PagedResponse<List<JobSchedulerModel>>(resp, paginationFilter.PageNumber, paginationFilter.PageSize, pageRespDic["prevPage"], pageRespDic["nextPage"], totalRecords);
            return testResp;
        }

        public async Task<JobSchedulerModel> getbyIdMstJobScheduleModel(int id)
        {
            var resp = await (from jobscheduler in _dotNetDBcontext.jobScheduler
                              where jobscheduler.jobSchedulerId == id
                               && jobscheduler.deleted == false
                              select jobscheduler).ToListAsync();
            if (resp.Count > 0)
            {
                return resp[0];
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<JobSchedulerModel> updateMstJobScheduleModel(JobSchedulerModel JobSchedulerModel)
        {
            var jobName = JobSchedulerModel.jobSchedulerId + JobSchedulerModel.jobName;
            

            var resp = _dotNetDBcontext.jobScheduler.Update(JobSchedulerModel);

            _dotNetDBcontext.SaveChanges();

            if (JobSchedulerModel.active == false)
            {
                Console.WriteLine("Inside false active " + JobSchedulerModel.active);

                RecurringJob.RemoveIfExists(jobName);
            }
            else
            {
                RecurringJob.AddOrUpdate(jobName, () => Console.WriteLine("Schedule is running for job" + JobSchedulerModel.jobName), JobSchedulerModel.interval);
            }
            return JobSchedulerModel;
        }
    }
}