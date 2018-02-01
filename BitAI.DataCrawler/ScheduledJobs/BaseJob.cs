using Quartz;
using System;
using System.Threading.Tasks;
using BitAI.DataService;

namespace BitAI.DataCrawler.ScheduledJobs
{
    public abstract class BaseJob : IJob
    {
        protected BittrexClient _client;
        protected DataCrawlingService _service;
        protected string _jobName;

        public BaseJob()
        {
            _client = new BittrexClient("a1b69f60294f4bee9148f2575f292fbe", "d305491dd146407393ab4c4e9217495b");
            _service = new DataCrawlingService(_client);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("----------");
            Console.WriteLine($"Start job {_jobName} at {String.Format("{0:s}", DateTime.Now)}...");
            int n = await RunJob();
            Console.WriteLine($"Number of records updated: {n}");
            Console.WriteLine($"End job {_jobName} at {String.Format("{0:G}", DateTime.Now)}...");
        }

        public virtual async Task<int> RunJob()
        {
            throw new NotImplementedException();
        }
    }
}
