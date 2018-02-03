using System;
using WcfService.jobs.jobData;

namespace WcfService
{
    public class BaseJob : IBaseJob<BaseJobData>
    {
        public void Perform(BaseJobData data)
        {
            Console.WriteLine(data.PerformData);
        }
    }
}
