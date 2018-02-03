using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace WcfService.database
{
    public class RedisMessageDb : IMessageDb
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        public void Add(Message message)
        {
            
            IDatabase db = redis.GetDatabase();
            String messages = db.StringGet("messages");
            if(messages == null)
            {
                messages = "";
            }
            messages += "\n" + message.JsonMessage;
            db.StringSet("messages", messages);
        }

        public void AddRange(List<Message> messages)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetAll()
        {
            IDatabase db = redis.GetDatabase();
            String[] messages = db.StringGet("messages").ToString().Split(new string[]{ "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<Message> resMessages = new List<Message>();
            foreach (var message in messages)
            {
                resMessages.Add(new Message()
                {
                    JsonMessage = message
                });
            }
            return resMessages;
        }
    }
}
