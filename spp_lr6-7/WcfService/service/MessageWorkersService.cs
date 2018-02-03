using System.Collections.Generic;
using System.ServiceModel;
using WcfService.database;
using System.Configuration;

namespace WcfService.service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MessageWorkerService : IMessageWorkerService
    {
        static Dictionary<string, Queue<string>> lowClients = new Dictionary<string, Queue<string>>();
        static Dictionary<string, Queue<string>> highClients = new Dictionary<string, Queue<string>>();
        static Dictionary<string, Queue<string>> mediumClients = new Dictionary<string, Queue<string>>();
        static Bounce clearClientsQueue;
        static IMessageDb db = new RedisMessageDb();
        static object lockqueue = new object();
        static List<MessageWorker> workers = new List<MessageWorker>();
        public MessageWorkerService()
        {
            int workerAmount = int.Parse(ConfigurationManager.AppSettings["workerAmount"]);
            int highPriority = int.Parse(ConfigurationManager.AppSettings["highPriority"]);
            int mediumPriority = int.Parse(ConfigurationManager.AppSettings["mediumPriority"]);
            int lowPriority = int.Parse(ConfigurationManager.AppSettings["lowPriority"]);
            int dumpTimeout = int.Parse(ConfigurationManager.AppSettings["dumpTimeout"]);
            for (int i = 0; i < workerAmount; ++i)
            {
                workers.Add(new MessageWorker(lowPriority, mediumPriority, highPriority));
            }

            clearClientsQueue = new Bounce(() =>
            {
                foreach (var worker in workers)
                {
                    foreach (var userName in lowClients.Keys)
                        worker.DoJob(lowClients[userName], mediumClients[userName], highClients[userName]);
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
            if (!lowClients.ContainsKey(userName))
            {
                clearClientsQueue.CallBounce();
                lowClients.Add(userName, new Queue<string>());
                highClients.Add(userName, new Queue<string>());
                mediumClients.Add(userName, new Queue<string>());
                return MessageServiceConstants.Success;
            }
            else
            {
                return MessageServiceConstants.UserNameBusy;
            }
        }

        public string Disconnect(string userName)
        {
            if (lowClients.ContainsKey(userName))
            {
                lowClients.Remove(userName);
                highClients.Remove(userName);
                mediumClients.Remove(userName);
                return MessageServiceConstants.Success;
            }
            else
            {
                return MessageServiceConstants.UsreNotFound;
            }
        }

        public string Pop(string userName, string queueName)
        {
            switch (queueName)
            {
                case "low":
                    return lowClients.ContainsKey(userName)
                ? lowClients[userName].Dequeue()
                : MessageServiceConstants.UsreNotFound;
                    break;
                case "medium":
                    return mediumClients.ContainsKey(userName)
                ? mediumClients[userName].Dequeue()
                : MessageServiceConstants.UsreNotFound;
                    break;
                case "high":
                    return highClients.ContainsKey(userName)
                ? highClients[userName].Dequeue()
                : MessageServiceConstants.UsreNotFound;
                    break;
            }
            return MessageServiceConstants.UsreNotFound;

        }

        public string Push(string userName, string jsonString, string queue)
        {
            if (lowClients.ContainsKey(userName))
            {
                clearClientsQueue.CallBounce();
                switch(queue)
                {
                    case "low":
                        lowClients[userName].Enqueue(jsonString);
                        break;
                    case "medium":
                        mediumClients[userName].Enqueue(jsonString);
                        break;
                    case "high":
                        highClients[userName].Enqueue(jsonString);
                        break;
                }
                return MessageServiceConstants.Success;
            }
            else
            {
                return MessageServiceConstants.UsreNotFound;
            }
        }

        public string Dump(string userName)
        {
            foreach (var jsonMessage in lowClients[userName])
            {
                db.Add(new Message()
                {
                    JsonMessage = jsonMessage
                });
            }
            foreach (var jsonMessage in mediumClients[userName])
            {
                db.Add(new Message()
                {
                    JsonMessage = jsonMessage
                });
            }
            foreach (var jsonMessage in highClients[userName])
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
                Push(userName, msg.JsonMessage, "high");
            }
            return MessageServiceConstants.Success;
        }
    }
}
