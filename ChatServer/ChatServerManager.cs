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
        private HashSet<String> availableServers = new HashSet<String>();

        public void addServer(string serverName)
        {
            availableServers.Add(serverName);
        }

        public void removeUser(string serverName)
        {
            availableServers.Remove(serverName);
        }
    }
}
