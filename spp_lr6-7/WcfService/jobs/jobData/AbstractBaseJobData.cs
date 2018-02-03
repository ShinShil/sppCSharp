using System.Runtime.Serialization;

namespace WcfService.jobs.jobData
{
    [DataContract]
    public abstract class AbstractBaseJobData
    {
        [DataMember]
        public string Type { get; protected set; }

        public AbstractBaseJobData()
        {
            Type = GetType().AssemblyQualifiedName;
        }
    }
}
