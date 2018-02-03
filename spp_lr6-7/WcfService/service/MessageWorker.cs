using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService.jobs;
using WcfService.jobs.jobData;

namespace WcfService.service
{
    class MessageWorker
    {
        private int low;
        private int high;
        private int medium;
        private Bounce bounce;
        private Queue<string> work = new Queue<string>();
        private Dictionary<Type, Type> jobs = new Dictionary<Type, Type>();


        public MessageWorker(int low, int medium, int high)
        {
            jobs.Add(typeof(BaseJobData), typeof(BaseJob));
            jobs.Add(typeof(BaseJobCalculatorData), typeof(BaseJobCalculator));
            this.low = low;
            this.medium = medium;
            this.high = high;
        }

        public void DoJob(Queue<string> lowQ, Queue<string> mediumQ, Queue<string> highQ)
        {
            work.Clear();
            AddWork(highQ, high);
            AddWork(mediumQ, medium);
            AddWork(lowQ, low);
            DoWork();
        }

        private void DoWork()
        {
            foreach (var json in work)
            {
                dynamic obj = JsonConvert.DeserializeObject(json);
                Type dataType = Type.GetType(obj.Type.Value); //TODO: server logger error, if not field Type - not extends AbstractBaseJob
                Type jobType = jobs[dataType]; //TODO: server logger error, check if registered
                object data = obj as Type;
                object job = Activator.CreateInstance(jobType);
                object jobData = Activator.CreateInstance(dataType);
                foreach (var property in jobData.GetType().GetProperties())
                {
                    if (property.PropertyType.Equals(typeof(int)))
                    {
                        property.SetValue(jobData, int.Parse(obj[property.Name].ToString()));
                    }
                    else
                    {
                        property.SetValue(jobData, obj[property.Name].ToString());
                    }
                }
                var method = job.GetType().GetMethod("Perform");
                method.Invoke(job, new object[] { jobData });
            }
            work.Clear();
        }


        private void AddWork(Queue<string> queue, int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                if (queue.Count == 0)
                {
                    break;
                }
                else
                {
                    work.Enqueue(queue.Dequeue());
                }
            }
        }
    }
}
