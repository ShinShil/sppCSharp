using System;

namespace Client
{
    class Program
    {
        static WcfService.MessageServiceClient client;
        static CommandParser commandsParser;
        static void Main(string[] args)
        {
            Auth();
            ReadCommands();
        }

        static void Auth()
        {
            client = new WcfService.MessageServiceClient();
            Console.Write("Enter user name: ");
            string userName = Console.ReadLine();
            commandsParser = new CommandParser(client, userName);
        }

        static void ReadCommands()
        {
            string command = "";
            while (command != Commands.Exit)
            {
                Console.Write("Enter the command: ");
                commandsParser.Parse((command = Console.ReadLine()));
            }
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
