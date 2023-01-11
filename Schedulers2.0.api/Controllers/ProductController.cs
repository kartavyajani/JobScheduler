using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hangfire;
using Microsoft.AspNetCore.Mvc;

using Schedulers2._0.api.Models;
namespace Schedulers2._0.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
      HangfireModel _HangfireModel= new HangfireModel();
        //  private readonly HangfireModel _HangfireModel ;

        // public ProductController(HangfireModel hangfireModel)
        // {
        //     _HangfireModel = hangfireModel;
        // }

        [HttpGet]
        [Route("login")]
        public String Login()
        {
            //Fire - and - Forget Job - this job is executed only once
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Welcome to Shopping World!"));
            return $"Job ID: {jobId}. Welcome mail sent to the user!";
        }
        [HttpGet]
        [Route("productcheckout")]
        public String CheckoutProduct()
        {
            //Delayed Job - this job executed only once but not immedietly after some time.
            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("You checkout new product into your checklist!"), TimeSpan.FromSeconds(20));
            return $"Job ID: {jobId}. You added one product into your checklist successfully!";
        }
        [HttpGet]
        [Route("productpayment")]
        public String ProductPayment()
        {
            //Fire and Forget Job - this job is executed only once
            var parentjobId = BackgroundJob.Enqueue(() => Console.WriteLine("You have done your payment suceessfully!"));
            //Continuations Job - this job executed when its parent job is executed.
            BackgroundJob.ContinueJobWith(parentjobId, () => Console.WriteLine("Product receipt sent!"));
            return "You have done payment and receipt sent on your mail id!";
        }
        [HttpGet]
        [Route("dailyoffers/{id}")]
        public String DailyOffers(string id)
        {
            
            //Recurring Job - this job is executed many times on the specified cron schedule
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Sent similar product offer and suuggestions"), Cron.MinuteInterval(1));
            return "offer sent!";
        }
        [HttpPost]
        [Route("Welcome")]
        public IActionResult Welcome(string Username)
        {

            var jobid = BackgroundJob.Enqueue(() => _HangfireModel.WelcomeMail(Username));
            return Ok($"Jobid : {jobid} Completed. Welcome Mail Sent.");
        }

        [HttpPost]
        [Route("Scheduled")]
        public IActionResult Scheduled(string Username)
        {
            var jobid = BackgroundJob.Schedule(() => _HangfireModel.ScheduledWelcomeMail(Username), TimeSpan.FromMinutes(1));
            return Ok($"Jobid : {jobid} Scheduled (Mail will be send after 1 minute)");
        }

        [HttpPost]
        [Route("FT")]
        public IActionResult FT(string Username,string deleteUsername)
        {
            RecurringJob.RemoveIfExists(deleteUsername);
            RecurringJob.AddOrUpdate(Username,() => _HangfireModel.ScheduleFT(Username), Cron.MinuteInterval(1));
            return Ok($"Job Scheduled (Daily) for {Username}.");
        }
    }
}
