using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChatServer
{
    [DataContract]
    public class DatabaseClass
    {
        [DataMember]
        public int userID = 1;
        [DataMember]
        public int serverID = 1000;
        [DataMember]
        private List<ChatRoom> allServers;
        [DataMember]
        private List<User> users;
        private List<string> allServerNames;

        public static DatabaseClass Instance { get; } = new DatabaseClass();
        static DatabaseClass() { }

        private DatabaseClass()
        {
            users = new List<User>();
            allServers = new List<ChatRoom>();
            allServerNames = new List<string>();
        }

        [DataMember]
        public List<string> AllServers
        {
            get { return allServerNames; }
            set { allServerNames = value; }
        }

        //Users
        public User addNewUser (string username)
        {
            User newUser = new User();
            newUser.UserID = userID;
            newUser.Username = username;
            users.Add(newUser);
            userID++;
            return newUser;
        }

        public int getUserID(string userName)
        {
            User temp = null;
            foreach (User user in users)
            {
                if (user.Username.Equals(userName))
                {
                    temp = user;
                }
            }
            return temp.UserID;
        }

        public User getUserAccountInfo(string userName)
        {
            User temp = null;
            foreach (User user in users)
            {
                if (user.Username.Equals(userName))
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
                if (temp[i].Username.Equals(currentUser.Username))
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
                if (user.Username.Equals(userName))
                {
                    isExisted = true;
                }
            }
            return isExisted;
        }

        public List<string> getChatRooms(string userName)
        {
            User theUser = null;
            foreach (User user in users)
            {
                if (user.Username.Equals(userName))
                {
                    theUser = user;
                }
            }
            return theUser.JoinedRooms;
        }

        public void addJoinedServer(string userName, string roomName)
        {
            User theUser = getUserAccountInfo(userName);
            ChatRoom theRoom = getRoomInfo(roomName);
            if (checkJoined(userName, theRoom))
            {
                MessageBox.Show("The user " + userName + "has already existed in this server.");
            }
            else
            {
                List<string> newList = theRoom.RoomUsers;
                newList.Add(userName);
                theRoom.RoomUsers = newList;
                addMessages("System: " + userName + " has joined the chat.", theRoom);
            }
            List<string> newroomList = theUser.JoinedRooms;
            newroomList.Add(roomName);
            theUser.JoinedRooms = newroomList;
            updateUserAccountInfo(theUser);
            updataRoomInfo(theRoom);
        }

        private bool checkJoined(string username, ChatRoom theRoom)
        {
            bool joined = false;
            foreach (string user in theRoom.RoomUsers)
            {
                if (user.Equals(username))
                {
                    joined = true;
                }
            }
            return joined;
        }

        private void addMessages(string message, ChatRoom theRoom)
        {
            List<string> newMsgList = theRoom.Messages;
            newMsgList.Add(message);
            theRoom.Messages = newMsgList;
            Console.WriteLine(message); //displaying message in the server, might change the way of display in the future
        }

        public void leaveRoom(string userName, string roomName)
        {
            User theUser = null;
            foreach (User user in users)
            {
                if (user.Username.Equals(userName))
                {
                    theUser = user;
                }
            }
            ChatRoom theRoom = null;
            foreach (ChatRoom room in allServers)
            {
                if (room.RoomName.Equals(roomName))
                {
                    theRoom = room;
                }
            }
            List<string> temp = theRoom.RoomUsers;
            for (int i = 0; i < theRoom.RoomUsers.Count; i++)
            {
                if (temp[i].Equals(userName))
                {
                    temp.RemoveAt(i);
                    addMessages("System: " + userName + " has left the chat.", theRoom);
                }
            }
            theRoom.RoomUsers = temp;

            List<string> roomList = theUser.JoinedRooms;
            for (int i = 0; i < theUser.JoinedRooms.Count; i++)
            {
                if (roomList[i].Equals(theRoom.RoomName))
                {
                    roomList.RemoveAt(i);
                }
            }
            theUser.JoinedRooms = roomList;

            updateUserAccountInfo(theUser);
            updataRoomInfo(theRoom);
        }

        //Chat Servers
        public void addNewChatServer (string username, string roomName)
        {
            //Create new room
            ChatRoom newRoom = new ChatRoom();
            newRoom.RoomName = roomName;
            newRoom.RoomID = serverID;

            User roomHost = getUserAccountInfo(username);
            List<string> newList = new List<string>();
            newList.Add(username);
            newRoom.RoomUsers = newList;
            addMessages("System: " + username + " has joined the chat.", newRoom);
            allServers.Add(newRoom);
            allServerNames.Add(roomName);

            List<string> newroomList = roomHost.JoinedRooms;
            newroomList.Add(roomName);
            roomHost.JoinedRooms = newroomList;
            updateUserAccountInfo(roomHost);
            serverID++;
        }

        public int getServerID (string roomName)
        {
            ChatRoom theRoom = null;
            foreach (ChatRoom temp in allServers)
            {
                if (temp.RoomName.Equals(roomName))
                {
                    theRoom = temp;
                }
            }
            return theRoom.RoomID;
        }

        public ChatRoom getRoomInfo(String roomName)
        {
            ChatRoom theRoom = null;
            foreach (ChatRoom temp in allServers)
            {
                if (temp.RoomName.Equals(roomName))
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
                if (temp[i].RoomName.Equals(currentRoom.RoomName))
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
                if (room.RoomName.Equals(roomName))
                {
                    isExisted = true;
                }
            }
            return isExisted;
        }

        public void addMessages(string message, string roomName)
        {
            ChatRoom theRoom = getRoomInfo(roomName);
            addMessages(message, theRoom);
            updataRoomInfo(theRoom);
        }

        public List<string> getMessages(string roomName)
        {
            ChatRoom theRoom = getRoomInfo(roomName);
            return theRoom.Messages;
        }

        public List<string> getParticipants(string roomName)
        {
            ChatRoom theRoom = getRoomInfo(roomName);
            return theRoom.RoomUsers;
        }

        public ChatRoom getChatRoom(String chatRoomName)
        {
            ChatRoom room = null;
            foreach(ChatRoom chatRoom in allServers)
            {
                if(chatRoom.RoomName.Equals(chatRoomName)) 
                {
                    room = chatRoom;
                }
            }
            return room;
        }

    }
}
