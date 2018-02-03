using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace WcfService.database
{
    public class SQLiteMessageDb : IMessageDb
    {
        
        public void Add(Message message)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=D:\\messages.sqlite; FailIfMissing=true"))
            {
                conn.Open();
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = "insert into messages (jsonMessage) values ('" + message.JsonMessage + "');";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void AddRange(List<Message> messages)
        {
            foreach(var msg in messages)
            {
                Add(msg);
            }
        }

        public List<Message> GetAll()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=D:\\messages.sqlite; FailIfMissing=true"))
            {
                conn.Open();
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = "select * from messages;";
                try
                {
                    var reader = command.ExecuteReader();
                    List<Message> result = new List<Message>();
                    while (reader.Read())
                    {
                        result.Add(new Message()
                        {
                            JsonMessage = reader["jsonMessage"].ToString()
                        });
                    }
                    return result;
                }
                catch
                {

                }
                return null;
            }
        }
    }
}
