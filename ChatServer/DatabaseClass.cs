using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChatServer
{
    public class DatabaseClass
    {
        //Users
        private List<User> users;
        private int userID = 1;
        private int serverID = 1000;

        //Chat Servers
        private List<ChatRoom> availableServers;

        public DatabaseClass()
        {
            users = new List<User>();
            availableServers = new List<ChatRoom>();
        }

        //Users
        public User addUserAccountInfo(string username)
        {
            User newUser = new User(username, userID);
            users.Add(newUser);
            userID++;
            return newUser;
        }

        public User getUserAccountInfo(String userName)
        {
            User temp = null;
            foreach (User user in users)
            {
                if (user.getUserName().Equals(userName))
                {
                    temp = user;
                }
            }
            return temp;
        }

        public void updateUserAccountInfo(User currentUser)
        {
            foreach (User user in users.ToList())
            {
                if (user.getUserName().Equals(currentUser.getUserName()))
                {
                    users.Remove(user);
                }
            }
            users.Add(currentUser);
        }

        public bool checkIsUserAvailable(string userName)
        {
            bool isExisted = false;

            foreach (User user in users)
            {
                if (user.getUserName().Equals(userName))
                {
                    isExisted = true;
                }
            }
            return isExisted;
        }

        public void addJoinedServer(string userName, string roomName)
        {
            User theUser = null;
            foreach (User user in users)
            {
                if (user.getUserName().Equals(userName))
                {
                    theUser = user;
                }
            }
            ChatRoom theRoom = null;
            foreach (ChatRoom room in availableServers)
            {
                if (room.getChatRoomName().Equals(roomName))
                {
                    theRoom = room;
                }
            }
            if (theRoom.addUser(theUser))
            {
                theUser.addChatRooms(theRoom);
            }
            updateUserAccountInfo(theUser);
        }

        public List<ChatRoom> getChatRooms(string userName)
        {
            User theUser = null;
            foreach (User user in users)
            {
                if (user.getUserName().Equals(userName))
                {
                    theUser = user;
                }
            }
            return theUser.getChatRooms();
        }

        public int getNumberOfUsers()
        {
            return users.Count;
        }

        //Chat Servers
        public ChatRoom addNewChatServer (User user, string roomName)
        {
            ChatRoom newRoom = new ChatRoom(roomName, serverID);
            User temp = getUserAccountInfo(user.getUserName());
            newRoom.addUser(user);
            availableServers.Add(newRoom);
            temp.addChatRooms(newRoom);
            updateUserAccountInfo(temp);
            serverID++;
            return newRoom;
        }

        public ChatRoom getRoomInfo(String roomName)
        {
            ChatRoom temp = null;
            foreach (ChatRoom room in availableServers)
            {
                if (room.getChatRoomName().Equals(roomName))
                {
                    temp = room;
                }
            }
            return temp;
        }

        public void updataRoomInfo(ChatRoom currentRoom)
        {
            List<ChatRoom> temp = availableServers;
            for (int i = 0; i < availableServers.Count; i++)
            {
                if (temp[i].getChatRoomName().Equals(currentRoom.getChatRoomName()))
                {
                    temp.RemoveAt(i);
                }
            }
            availableServers = null;
            availableServers = temp;
            availableServers.Add(currentRoom);
        }

        public void leaveChat(string username, string roomName)
        {
            User temp = getUserAccountInfo(username);
            MessageBox.Show("Username: " + temp.getUserName());
            ChatRoom tempRoom = getRoomInfo(roomName);
            MessageBox.Show("Room name: " + tempRoom.getChatRoomName());
            if (tempRoom.removeUser(temp))
            {
                temp.removeChatRooms(tempRoom);
            }
            updateUserAccountInfo(temp);
            updataRoomInfo(tempRoom);
        }

        public List<ChatRoom> getAllServer()
        {
            return availableServers;
        }
    }
}
