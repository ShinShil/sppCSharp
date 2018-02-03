using System.Runtime.Serialization;

namespace WcfService.jobs.jobData
{
    [KnownType(typeof(BaseJobCalculatorData))]
    public class BaseJobCalculatorData: AbstractBaseJobData
    {
        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }
    }
}
