using System;
using WcfService.jobs.jobData;

namespace WcfService.jobs
{
    public class BaseJobCalculator : IBaseJob<BaseJobCalculatorData>
    {
        public void Perform(BaseJobCalculatorData data)
        {
            Console.WriteLine(data.X + data.Y);
        }
    }
}
