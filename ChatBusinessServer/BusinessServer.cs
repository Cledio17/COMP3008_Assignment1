using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatBusinessServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class BusinessServer : BusinessServerInterface
    {
        private DataServerInterface foob;

        public BusinessServer()
        {
            ChannelFactory<DataServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        public void addJoinedServer(string userName, string roomName)
        {
            foob.addJoinedServer(userName, roomName);
        }

        public void addMessages(string messagebys, string message, string roomName)
        {
            foob.addMessages(messagebys, message, roomName);
        }

        public void addServer(User user, string roomName)
        {
            foob.addServer(user, roomName);
        }

        public User addUserAccountInfo(string username)
        {
            return foob.addUserAccountInfo(username);
        }

        public bool checkIsRoomNameExist(string roomName)
        {
            return foob.checkIsRoomNameExist(roomName);
        }

        public bool checkIsUsernameExist(string userName)
        {
            return foob.checkIsUsernameExist(userName);
        }

        public List<ChatRoom> getAllServers()
        {
            return foob.getAllServers();
        }

        public List<ChatRoom> getJoinedServers(string userName)
        {
            return foob.getJoinedServers(userName);
        }

        public ChatRoom getServerInfo(string roomName)
        {
            return foob.getServerInfo(roomName);
        }

        public User getUserAccountInfo(string userName)
        {
            return foob.getUserAccountInfo(userName);
        }

        public void leaveRoom(string username, string roomName)
        {
            foob.leaveRoom(username, roomName);
        }
    }
}
