using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class ChatServerManager
    {
        private HashSet<ChatServer> availableServers = new HashSet<ChatServer>();

        public void addServer(ChatServer server)
        {
            availableServers.Add(server);
        }

        public void removeServer(ChatServer server)
        {
            availableServers.Remove(server);
        }
    }
}
