using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public static class ChatServerManager
    {
        private static HashSet<ChatRoom> availableServers = new HashSet<ChatRoom>();

        private static int serverNo = 1000;

        public static ChatRoom addServer(string roomName)
        {
            ChatRoom newRoom = new ChatRoom(roomName, serverNo);
            availableServers.Add(newRoom);
            serverNo++;
            return newRoom;
        }

        public static void removeServer(ChatRoom server)
        {
            availableServers.Remove(server);
        }

        public static HashSet<ChatRoom> getAllServer()
        {
            return availableServers;
        }
    }
}
