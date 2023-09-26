using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class DataServer : DataServerInterface
    {
        //Users
        private DatabaseClass usersDatabase;

        //Chat Servers
        public DataServer()
        {
            usersDatabase = new DatabaseClass();
        }

        //Users
        public User addUserAccountInfo(string username)
        {
            return usersDatabase.addUserAccountInfo(username);
        }

        public User getUserAccountInfo(string userName)
        {
            return usersDatabase.getUserAccountInfo(userName);
        }

        public bool checkIsUsernameExist(string userName)
        {
            return usersDatabase.checkIsUsernameExist(userName);
        }

        public void addJoinedServer (string userName, string roomName)
        {
            usersDatabase.addJoinedServer(userName, roomName);
        }

        public List<ChatRoom> getJoinedServers(string userName)
        {
            return usersDatabase.getChatRooms(userName);
        }

        //Chat Servers
        public void addServer(User user, string roomName)
        {
            usersDatabase.addNewChatServer(user, roomName);
        }

        public ChatRoom getServerInfo(string roomName)
        {
            return usersDatabase.getRoomInfo(roomName);
        }

        public bool checkIsRoomNameExist(string roomName)
        {
            return usersDatabase.checkRoomNameExist(roomName);
        }

        public void addMessages(string messagebys, string message, string roomName)
        {
            usersDatabase.addMessages(messagebys, message, roomName);
        }

        public List<ChatRoom> getAllServers ()
        {
            return usersDatabase.getAllServer();
        }

        public void leaveRoom(string username, string roomName)
        {
            usersDatabase.leaveRoom(username, roomName);
        }
    }
}
