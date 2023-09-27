using ChatServer;
using ChatServerDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDataServer
{
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

        //Users
        public void setLoggedIn(string userName)
        {
            foob.setLoggedIn(userName);
        }

        public void setLoggedOut(string userName)
        {
            foob.setLoggedOut(userName);
        }

        public bool isLoggedIn(string userName)
        {
            return foob.isLoggedIn(userName);
        }

        public User addUserAccountInfo(string username)
        {
            return foob.addUserAccountInfo(username);
        }

        public int getUserID(string username)
        {
            return foob.getUserID(username);
        }


        public User getUserAccountInfo(string userName)
        {
            return foob.getUserAccountInfo(userName);
        }

        public bool checkIsUsernameExist(string userName)
        {
            return foob.checkIsUsernameExist(userName);
        }

        public bool addJoinedServer(string userName, string roomName)
        {
            return foob.addJoinedServer(userName, roomName);
        }

        public List<string> getJoinedServers(string userName)
        {
            return foob.getJoinedServers(userName);
        }

        //Chat Servers
        public void addServer(string username, string roomName)
        {
            foob.addServer(username, roomName);
        }

        public int getServerID(string roomName)
        {
            return foob.getServerID(roomName);
        }

        public ChatRoom getServerInfo(string roomName)
        {
            return foob.getServerInfo(roomName);
        }

        public bool checkIsRoomNameExist(string roomName)
        {
            return foob.checkIsRoomNameExist(roomName);
        }

        public void addMessages(string message, string roomName, bool isFile)
        {
            foob.addMessages(message, roomName, isFile);
        }

        public List<string> getMessages(string roomName)
        {
            return foob.getMessages(roomName);
        }

        public List<string> getParticipants(string roomName)
        {
            return foob.getParticipants(roomName);
        }

        public List<string> getAllServers()
        {
            return foob.getAllServers();
        }

        public List<int> getFileLoc(String roomName)
        {
            return foob.getFileLoc(roomName);
        }

        public void leaveRoom(string username, string roomName)
        {
            foob.leaveRoom(username, roomName);
        }

        public void addPrivateMessage(string username, string contactName, string message, bool isFile)
        {
            foob.addPrivateMessage(username, contactName, message, isFile);
        }

        public List<string> getPrivateMessages(string username, string contactName)
        {
            return foob.getPrivateMessages(username, contactName);
        }

        public List<int> getPMFileLoc(String username, string contactName)
        {
            return foob.getPMFileLoc(username, contactName);
        }
    }
}
