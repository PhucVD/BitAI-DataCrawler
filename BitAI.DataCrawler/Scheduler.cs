using BitAI.DataCrawler.ScheduledJobs;
using BitAI.DataService;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace BitAI.DataCrawler
{
    public class Scheduler
    {
        public static async Task RunScheduler()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                #region GetCurrencyJob
                // define the job and tie it to our job class
                //IJobDetail getCurrencyJob = JobBuilder.Create<GetCurrencyJob>()
                //    .WithIdentity("GetCurrencyJob", "group1")
                //    .Build();

                // Trigger the job to run now, and then every 40 seconds
                //ITrigger getCurrencyTrigger = TriggerBuilder.Create()
                //  .WithIdentity("GetCurrencyTrigger", "group1")
                //  .StartNow()
                //  .WithSimpleSchedule(x => x
                //      .WithIntervalInSeconds(30)
                //      .WithRepeatCount(2))
                //  .Build();

                //await scheduler.ScheduleJob(getCurrencyJob, getCurrencyTrigger);
                #endregion

                #region GetMarketJob
                //IJobDetail getMarketJob = JobBuilder.Create<GetMarketJob>()
                //    .WithIdentity("GetMarketJob", "group1")
                //    .Build();

              

                //ITrigger getMarketTrigger = TriggerBuilder.Create()
                //  .WithIdentity("GetMarketJob", "group1")
                //  .StartNow()
                //  .WithSimpleSchedule(x => x
                //      .WithIntervalInSeconds(30)
                //      .WithRepeatCount(4))
                //  .Build();

                //await scheduler.ScheduleJob(getMarketJob, getMarketTrigger);
                #endregion

                #region GetMarketHistoryJob
                IJobDetail getMarketHistoryJob = JobBuilder.Create<GetMarketHistoryJob>()
                    .WithIdentity("GetMarketHistory", "group1")
                    .Build();

                ITrigger getMarketHistoryTrigger = TriggerBuilder.Create()
                  .WithIdentity("GetMarketHistory", "group1")
                  .StartNow()
                  .WithSimpleSchedule(x => x
                      .WithIntervalInSeconds(30)
                      .WithRepeatCount(4))
                  .Build();

                await scheduler.ScheduleJob(getMarketHistoryJob, getMarketHistoryTrigger);
                #endregion

                // and start it off
                await scheduler.Start();

                // some sleep to show what's happening
                //await Task.Delay(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                //await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
    }    
}
