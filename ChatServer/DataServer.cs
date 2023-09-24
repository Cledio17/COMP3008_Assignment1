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

        public bool isUserNameAvailable(string userName)
        {
            return usersDatabase.checkIsUserAvailable(userName);
        }

        public void updateUserAccountInfo(User user)
        {
            usersDatabase.updateUserAccountInfo(user);
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
        public ChatRoom addServer(User user, string roomName)
        {
            return usersDatabase.addNewChatServer(user, roomName);
        }

        public HashSet<ChatRoom> getAllServers ()
        {
            return usersDatabase.getAllServer();
        }
    }
}
