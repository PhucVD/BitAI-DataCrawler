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
                
                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<DataCrawlerJob>()
                    .WithIdentity("UpdateMarketHistory", "group1")
                    .Build();

                // Trigger the job to run now, and then every 40 seconds
                ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity("myTrigger", "group1")
                  .StartNow()
                  .WithSimpleSchedule(x => x
                      .WithIntervalInSeconds(15)
                      .WithRepeatCount(4))
                  .Build();

                await scheduler.ScheduleJob(job, trigger);

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

    public class DataCrawlerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var client = new BittrexClient("a1b69f60294f4bee9148f2575f292fbe", "d305491dd146407393ab4c4e9217495b");
            var service = new DataCrawlingService(client);

            Console.WriteLine($"Start job {context.JobDetail.Key} at {DateTime.Now.ToLongDateString()}...");

            await service.GetMarketHistory("BTC-ADA");
        }
    }
}
