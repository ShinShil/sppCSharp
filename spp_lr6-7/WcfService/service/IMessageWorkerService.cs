using System.ServiceModel;

namespace WcfService.service
{
    [ServiceContract]
    interface IMessageWorkerService
    {
        [OperationContract]
        string GetTestMessage(string name);

        [OperationContract]
        string Push(string userName, string jsonString, string prior);

        [OperationContract]
        string Connect(string userName);

        [OperationContract]
        string Disconnect(string userName);

        [OperationContract]
        string Pop(string userName, string prior);

        [OperationContract]
        string Dump(string userName);

        [OperationContract]
        string Restore(string userName);
    }
}
