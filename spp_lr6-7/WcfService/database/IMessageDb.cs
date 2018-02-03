using System.Collections.Generic;

namespace WcfService.database
{
    public interface IMessageDb
    {
        void Add(Message message);
        List<Message> GetAll();
        void AddRange(List<Message> messages);
    }
}
