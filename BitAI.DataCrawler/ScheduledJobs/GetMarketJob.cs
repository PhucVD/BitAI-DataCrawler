using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitAI.DataCrawler.ScheduledJobs
{
    public class GetMarketJob : BaseJob
    {
        public GetMarketJob() : base()
        {
            _jobName = "GetMarkets";
        }

        public override async Task<int> RunJob()
        {
            return await _service.GetMarkets();
        }
    }
}
