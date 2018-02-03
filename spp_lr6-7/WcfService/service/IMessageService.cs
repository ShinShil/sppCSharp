using System.ServiceModel;

namespace WcfService
{
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        string GetTestMessage(string name);

        [OperationContract]
        string Push(string userName, string jsonString);

        [OperationContract]
        string Connect(string userName);

        [OperationContract]
        string Disconnect(string userName);

        [OperationContract]
        string Pop(string userName);

        [OperationContract]
        string Dump(string userName);

        [OperationContract]
        string Restore(string userName);
    }
}
