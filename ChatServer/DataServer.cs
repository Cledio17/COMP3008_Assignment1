using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class DataServer : DataServerInterface
    {
        private DatabaseClass usersDatabase = DatabaseClass.Instance;

        //Users
        public User addUserAccountInfo(string username)
        {
            return usersDatabase.addNewUser(username);
        }

        public int getUserID(string username)
        {
            return usersDatabase.getUserID(username);
        }


        public User getUserAccountInfo(string userName)
        {
            return usersDatabase.getUserAccountInfo(userName);
        }

        public bool checkIsUsernameExist(string userName)
        {
            return usersDatabase.checkIsUsernameExist(userName);
        }

        public bool addJoinedServer (string userName, string roomName)
        {
            return usersDatabase.addJoinedServer(userName, roomName);
        }

        public List<string> getJoinedServers(string userName)
        {
            return usersDatabase.getChatRooms(userName);
        }

        //Chat Servers
        public void addServer(string username, string roomName)
        {
            usersDatabase.addNewChatServer(username, roomName);
        }

        public int getServerID(string roomName)
        {
            return usersDatabase.getServerID(roomName);
        }

        public ChatRoom getServerInfo(string roomName)
        {
            return usersDatabase.getRoomInfo(roomName);
        }

        public bool checkIsRoomNameExist(string roomName)
        {
            return usersDatabase.checkRoomNameExist(roomName);
        }

        public void addMessages(string message, string roomName, bool isFile)
        {
            usersDatabase.addMessages(message, roomName, isFile);
        }

        public List<string> getMessages(string roomName)
        {
            return usersDatabase.getMessages(roomName);
        }

        public List<string> getParticipants(string roomName)
        {
            return usersDatabase.getParticipants(roomName);
        }

        public List<string> getAllServers ()
        {
            return usersDatabase.AllServers;
        }

        public List<int> getFileLoc(String roomName)
        {
            return usersDatabase.getFileLoc(roomName);
        }

        public void leaveRoom(string username, string roomName)
        {
            usersDatabase.leaveRoom(username, roomName);
        }

        public void addPrivateMessage(string username, string contactName, string message, bool isFile)
        {
            usersDatabase.addPrivateMessage(username, contactName, message, isFile);
        }

        public List<string> getPrivateMessages(string username, string contactName)
        {
            return usersDatabase.getPrivateMessages(username, contactName);
        }

        public List<int> getPMFileLoc(String username, string contactName)
        {
            return usersDatabase.getPMFileLoc(username, contactName);
        }
    }
}
