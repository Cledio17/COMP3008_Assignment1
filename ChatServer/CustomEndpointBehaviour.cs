using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ChatServer
{
    public class CustomClientEndpointBehavior : IEndpointBehavior
    {
        private int maxItemsInObjectGraph;

        public CustomClientEndpointBehavior(int maxItemsInObjectGraph)
        {
            this.maxItemsInObjectGraph = maxItemsInObjectGraph;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // You can access and modify the binding parameters here
            Binding binding = endpoint.Binding;

            // Check if the binding is a NetTcpBinding
            if (binding is NetTcpBinding tcp)
            {
                // Modify properties of the binding as needed
                tcp.Security.Mode = SecurityMode.None;
                tcp.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;
                tcp.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.None;
                tcp.Security.Message.ClientCredentialType = MessageCredentialType.None;

                tcp.MaxReceivedMessageSize = 2147483647;
                tcp.MaxBufferPoolSize = 2147483647;
                tcp.MaxBufferSize = 2147483647;
                tcp.OpenTimeout = TimeSpan.FromMinutes(15);
                tcp.SendTimeout = TimeSpan.FromMinutes(10);
                tcp.ReceiveTimeout = TimeSpan.FromMinutes(15);
            }
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            foreach (ClientOperation operation in clientRuntime.ClientOperations)
            {
                // No need to set MaxItemsInObjectGraph here
            }

            var dcsob = endpoint.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (dcsob != null)
            {
                dcsob.MaxItemsInObjectGraph = maxItemsInObjectGraph;
            }
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            foreach (var operation in endpointDispatcher.DispatchRuntime.Operations)
            {
                // No need to set MaxItemsInObjectGraph here
            }

            var dcsob = endpoint.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (dcsob != null)
            {
                dcsob.MaxItemsInObjectGraph = maxItemsInObjectGraph;
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            if (maxItemsInObjectGraph <= 0)
            {
                throw new InvalidOperationException("maxItemsInObjectGraph must be a positive integer.");
            }
        }

        // Other IEndpointBehavior methods (AddBindingParameters, ApplyDispatchBehavior, Validate)
    }
}

