﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.WcfService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfService.IMessageService")]
    public interface IMessageService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/GetTestMessage", ReplyAction="http://tempuri.org/IMessageService/GetTestMessageResponse")]
        string GetTestMessage(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/GetTestMessage", ReplyAction="http://tempuri.org/IMessageService/GetTestMessageResponse")]
        System.Threading.Tasks.Task<string> GetTestMessageAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Push", ReplyAction="http://tempuri.org/IMessageService/PushResponse")]
        string Push(string userName, string jsonString);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Push", ReplyAction="http://tempuri.org/IMessageService/PushResponse")]
        System.Threading.Tasks.Task<string> PushAsync(string userName, string jsonString);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Connect", ReplyAction="http://tempuri.org/IMessageService/ConnectResponse")]
        string Connect(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Connect", ReplyAction="http://tempuri.org/IMessageService/ConnectResponse")]
        System.Threading.Tasks.Task<string> ConnectAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Disconnect", ReplyAction="http://tempuri.org/IMessageService/DisconnectResponse")]
        string Disconnect(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Disconnect", ReplyAction="http://tempuri.org/IMessageService/DisconnectResponse")]
        System.Threading.Tasks.Task<string> DisconnectAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Pop", ReplyAction="http://tempuri.org/IMessageService/PopResponse")]
        string Pop(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Pop", ReplyAction="http://tempuri.org/IMessageService/PopResponse")]
        System.Threading.Tasks.Task<string> PopAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Dump", ReplyAction="http://tempuri.org/IMessageService/DumpResponse")]
        string Dump(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Dump", ReplyAction="http://tempuri.org/IMessageService/DumpResponse")]
        System.Threading.Tasks.Task<string> DumpAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Restore", ReplyAction="http://tempuri.org/IMessageService/RestoreResponse")]
        string Restore(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageService/Restore", ReplyAction="http://tempuri.org/IMessageService/RestoreResponse")]
        System.Threading.Tasks.Task<string> RestoreAsync(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessageServiceChannel : Client.WcfService.IMessageService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessageServiceClient : System.ServiceModel.ClientBase<Client.WcfService.IMessageService>, Client.WcfService.IMessageService {
        
        public MessageServiceClient() {
        }
        
        public MessageServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MessageServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessageServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessageServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetTestMessage(string name) {
            return base.Channel.GetTestMessage(name);
        }
        
        public System.Threading.Tasks.Task<string> GetTestMessageAsync(string name) {
            return base.Channel.GetTestMessageAsync(name);
        }
        
        public string Push(string userName, string jsonString) {
            return base.Channel.Push(userName, jsonString);
        }
        
        public System.Threading.Tasks.Task<string> PushAsync(string userName, string jsonString) {
            return base.Channel.PushAsync(userName, jsonString);
        }
        
        public string Connect(string userName) {
            return base.Channel.Connect(userName);
        }
        
        public System.Threading.Tasks.Task<string> ConnectAsync(string userName) {
            return base.Channel.ConnectAsync(userName);
        }
        
        public string Disconnect(string userName) {
            return base.Channel.Disconnect(userName);
        }
        
        public System.Threading.Tasks.Task<string> DisconnectAsync(string userName) {
            return base.Channel.DisconnectAsync(userName);
        }
        
        public string Pop(string userName) {
            return base.Channel.Pop(userName);
        }
        
        public System.Threading.Tasks.Task<string> PopAsync(string userName) {
            return base.Channel.PopAsync(userName);
        }
        
        public string Dump(string userName) {
            return base.Channel.Dump(userName);
        }
        
        public System.Threading.Tasks.Task<string> DumpAsync(string userName) {
            return base.Channel.DumpAsync(userName);
        }
        
        public string Restore(string userName) {
            return base.Channel.Restore(userName);
        }
        
        public System.Threading.Tasks.Task<string> RestoreAsync(string userName) {
            return base.Channel.RestoreAsync(userName);
        }
    }
}