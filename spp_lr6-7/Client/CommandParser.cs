using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WcfService.jobs.jobData;

namespace Client
{
    public class CommandParser
    {
        private WcfService.MessageServiceClient client;
        private Dictionary<string, Action> commands = new Dictionary<string, Action>();
        private string userName = "Lotegr";

        public CommandParser(WcfService.MessageServiceClient client, string userName)
        {
            this.client = client;
            this.userName = userName;
            this.client.Connect(userName);
            commands[Commands.Push] = Push;
            commands[Commands.Dump] = Dump;
            commands[Commands.Restore] = Restore;
            commands[Commands.Exit] = () => { };
        }

        ~CommandParser()
        {
            client.Disconnect(userName);
        }

        public void Parse(string command)
        {
            if (!commands.ContainsKey(command))
            {
                Console.WriteLine("No such command: " + command);
            } else
            {
                commands[command]();
            }
        }

        private void Push()
        {
            Console.WriteLine("Available operations");
            Console.WriteLine("\t1. Show message");
            Console.WriteLine("\t2. Accumulate");
            Console.Write("Select the operations(enter the number): ");
            int choice = int.Parse(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    Console.Write("Enter the message: ");
                    BaseJobData job = new BaseJobData()
                    {
                        PerformData = Console.ReadLine()
                    };
                    client.Push(userName, JsonConvert.SerializeObject(job));
                    break;
                case 2:
                    BaseJobCalculatorData jobCalc = new BaseJobCalculatorData();
                    Console.Write("Enter value X: ");
                    jobCalc.X = int.Parse(Console.ReadLine());
                    Console.Write("Enter value Y: ");
                    jobCalc.Y = int.Parse(Console.ReadLine());
                    client.Push(userName, JsonConvert.SerializeObject(jobCalc));
                    break;
                default: Console.WriteLine("Bad option");
                    break;
            }            
        }

        private void Dump()
        {
            client.Dump(userName);   
        }
        
        private void Restore()
        {
            client.Restore(userName);
        }

        private void Pop()
        {
            client.Pop(userName);
        }
    }
}
