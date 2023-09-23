using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ChatServerManager
    {
        private HashSet<ChatRoom> availableServers = new HashSet<ChatRoom>();

        public void addServer(ChatRoom server)
        {
            availableServers.Add(server);
        }

        public void removeServer(ChatRoom server)
        {
            availableServers.Remove(server);
        }

        public HashSet<ChatRoom> getAllServer()
        {
            return availableServers;
        }
    }
}
