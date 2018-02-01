using Quartz;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitAI.DataCrawler.ScheduledJobs
{
    public class GetCurrencyJob: BaseJob
    {
        public GetCurrencyJob() : base()
        {
            _jobName = "GetCurrencies";
        }

        public override async Task<int> RunJob()
        {
            return await _service.GetCurrencies();
        }
    }
}
