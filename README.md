# JobScheduler
Just clone this repository 
for pagination there is seprate folder of pagination and utlities 
for rollback mechanism refer to   IDbContextTransaction transaction = null;
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
and added job using hangfire you can use all the controller and the main use of this there is custom job module for it 
and can also open hangfire daschboard https://localhost:7126/hangfire/recurring
