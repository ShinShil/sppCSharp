using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WcfService.database;
using WcfService.jobs;
using WcfService.jobs.jobData;
using WcfService.service;
using System.Configuration;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MessageService : IMessageService
    {
        static Dictionary<string, Queue<string>> clients = new Dictionary<string, Queue<string>>();
        static Dictionary<Type, Type> jobs = new Dictionary<Type, Type>();
        static Bounce clearClientsQueue;
        static IMessageDb db = new SQLiteMessageDb();
        static object lockqueue = new object();
        static List<MessageWorker> workers = new List<MessageWorker>();
        public MessageService()
        {
            int dumpTimeout = int.Parse(ConfigurationManager.AppSettings["dumpTimeout"]);
            jobs.Add(typeof(BaseJobData), typeof(BaseJob));
            jobs.Add(typeof(BaseJobCalculatorData), typeof(BaseJobCalculator));
            clearClientsQueue = new Bounce(() =>
            {
                foreach (var queue in clients.Values)
                {
                    foreach (var json in queue)
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
                    queue.Clear();
                }
            }, dumpTimeout);
            clearClientsQueue.CallBounce();
        }

        public string GetTestMessage(string name)
        {
            return "Hello, " + name;
        }

        public string Connect(string userName)
        {
            if (!clients.ContainsKey(userName))
            {
                clearClientsQueue.CallBounce();
                clients.Add(userName, new Queue<string>());
                return MessageServiceConstants.Success;
            }
            else
            {
                return MessageServiceConstants.UserNameBusy;
            }
        }

        public string Disconnect(string userName)
        {
            if (clients.ContainsKey(userName))
            {
                clients.Remove(userName);
                return MessageServiceConstants.Success;
            }
            else
            {
                return MessageServiceConstants.UsreNotFound;
            }
        }

        public string Pop(string userName)
        {
            return clients.ContainsKey(userName)
                ? clients[userName].Dequeue()
                : MessageServiceConstants.UsreNotFound;
        }

        public string Push(string userName, string jsonString)
        {
            if (clients.ContainsKey(userName))
            {
                clearClientsQueue.CallBounce();
                clients[userName].Enqueue(jsonString);
                return MessageServiceConstants.Success;
            }
            else
            {
                return MessageServiceConstants.UsreNotFound;
            }
        }

        public string Dump(string userName)
        {
            foreach (var jsonMessage in clients[userName])
            {
                db.Add(new Message()
                {
                    JsonMessage = jsonMessage
                });
            }
            return MessageServiceConstants.Success;
        }

        public string Restore(string userName)
        {
            List<Message> messages = db.GetAll();
            foreach (var msg in messages)
            {
                Push(userName, msg.JsonMessage);
            }
            return MessageServiceConstants.Success;
        }
    }
}
