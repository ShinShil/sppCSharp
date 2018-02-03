using System.Runtime.Serialization;

namespace WcfService.jobs.jobData
{
    [KnownType(typeof(BaseJobData))]
    public class BaseJobData: AbstractBaseJobData
    {
        [DataMember]
        public string PerformData { get; set; }
    }
}
