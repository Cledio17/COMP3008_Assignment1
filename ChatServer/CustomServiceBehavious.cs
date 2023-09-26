using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ChatServer
{
    public class CustomServiceBehavior : IServiceBehavior
    {
        private int maxItemsInObjectGraph;

        public CustomServiceBehavior(int maxItemsInObjectGraph)
        {
            this.maxItemsInObjectGraph = maxItemsInObjectGraph;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            // You can access and modify the binding parameters here
            foreach (var endpoint in endpoints)
            {
                // Access the binding for the endpoint
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
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpointDispatcher in dispatcher.Endpoints)
                {
                    foreach (DispatchOperation operation in endpointDispatcher.DispatchRuntime.Operations)
                    {
                        // No need to set MaxItemsInObjectGraph here
                    }

                    var dcsob = endpointDispatcher.DispatchRuntime.OperationSelector as DataContractSerializerOperationBehavior;
                    if (dcsob != null)
                    {
                        dcsob.MaxItemsInObjectGraph = maxItemsInObjectGraph;
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (maxItemsInObjectGraph <= 0)
            {
                throw new InvalidOperationException("maxItemsInObjectGraph must be a positive integer.");
            }
        }

        // Other IServiceBehavior methods (AddBindingParameters, Validate)
    }
}