using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitAI.DataCrawler.ScheduledJobs
{
    public class GetMarketHistoryJob : BaseJob
    {
        public GetMarketHistoryJob() : base()
        {
            _jobName = "GetMarketHistory";
        }

        public override async Task<int> RunJob()
        {
            Console.WriteLine($"Market: BTC-ADA");
            return await _service.GetMarketHistory("BTC-ADA");
        }
    }
}
