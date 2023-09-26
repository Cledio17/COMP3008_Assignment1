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
        private List<ChatRoom> allServers;

        public DatabaseClass()
        {
            users = new List<User>();
            allServers = new List<ChatRoom>();
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
            List<User> temp = users;
            for (int i = 0; i < users.Count; i++)
            {
                if (temp[i].getUserName().Equals(currentUser.getUserName()))
                {
                    users.RemoveAt(i);
                }
            }
            users.Add(currentUser);
        }

        public bool checkIsUsernameExist(string userName)
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

        public void addJoinedServer(string userName, string roomName)
        {
            User theUser = getUserAccountInfo(userName);
            ChatRoom theRoom = getRoomInfo(roomName);
            theRoom.addUser(theUser);
            theUser.addChatRooms(theRoom);
            updateUserAccountInfo(theUser);
            updataRoomInfo(theRoom);
        }

        public void leaveRoom(string username, string roomName)
        {
            User theUser = null;
            foreach (User user in users)
            {
                if (user.getUserName().Equals(username))
                {
                    theUser = user;
                }
            }
            ChatRoom theRoom = null;
            foreach (ChatRoom room in allServers)
            {
                if (room.getChatRoomName().Equals(roomName))
                {
                    theRoom = room;
                }
            }
            theRoom.removeUser(theUser);
            theUser.removeChatRooms(theRoom);
            updateUserAccountInfo(theUser);
            updataRoomInfo(theRoom);
        }

        //Chat Servers
        public void addNewChatServer (User roomHost, string roomName)
        {
            ChatRoom newRoom = new ChatRoom(roomName, serverID);
            User user = getUserAccountInfo(roomHost.getUserName());
            newRoom.addUser(roomHost);
            allServers.Add(newRoom);
            user.addChatRooms(newRoom);
            updateUserAccountInfo(user);
            serverID++;
        }

        public ChatRoom getRoomInfo(String roomName)
        {
            ChatRoom theRoom = null;
            foreach (ChatRoom temp in allServers)
            {
                if (temp.getChatRoomName().Equals(roomName))
                {
                    theRoom = temp;
                }
            }
            return theRoom;
        }

        public void updataRoomInfo(ChatRoom currentRoom)
        {
            List<ChatRoom> temp = allServers;
            for (int i = 0; i < allServers.Count; i++)
            {
                if (temp[i].getChatRoomName().Equals(currentRoom.getChatRoomName()))
                {
                    allServers.RemoveAt(i);
                }
            }
            allServers.Add(currentRoom);
        }

        public bool checkRoomNameExist(string roomName)
        {
            bool isExisted = false;

            foreach (ChatRoom room in allServers)
            {
                if (room.getChatRoomName().Equals(roomName))
                {
                    isExisted = true;
                }
            }
            return isExisted;
        }

        public void addMessages(string messagebys, string message, string roomName)
        {
            ChatRoom theRoom = getRoomInfo(roomName);
            theRoom.addMessages(messagebys, message);
            updataRoomInfo(theRoom);
        }
        public List<ChatRoom> getAllServer()
        {
            return allServers;
        }

        public ChatRoom getChatRoom(String chatRoomName)
        {
            ChatRoom room = null;
            foreach(ChatRoom chatRoom in allServers)
            {
                if(chatRoom.getChatRoomName().Equals(chatRoomName)) 
                {
                    room = chatRoom;
                }
            }
            return room;
        }
    }
}
