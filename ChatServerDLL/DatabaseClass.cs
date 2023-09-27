using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace ChatServerDLL
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
        [DataMember]
        private List<PrivateMessage> pmList;
        private List<string> allServerNames;

        public static DatabaseClass Instance { get; } = new DatabaseClass();
        static DatabaseClass() { }

        private DatabaseClass()
        {
            users = new List<User>();
            allServers = new List<ChatRoom>();
            allServerNames = new List<string>();
            pmList = new List<PrivateMessage>();
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

        public bool addJoinedServer(string userName, string roomName)
        {
            bool joined = false;
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
                joined = true;
            }
            List<string> newroomList = theUser.JoinedRooms;
            newroomList.Add(roomName);
            theUser.JoinedRooms = newroomList;
            updateUserAccountInfo(theUser);
            updataRoomInfo(theRoom);
            return joined;
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

        public void addMessages(string message, string roomName, bool isFile)
        {
            ChatRoom theRoom = getRoomInfo(roomName);
            addMessages(message, theRoom);
            if (isFile)
            {
                int locNo = theRoom.Messages.Count - 1;
                List<int> temp = theRoom.FileLoc;
                temp.Add(locNo);
            }    
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

        public List<int> getFileLoc(String roomName)
        {
            ChatRoom theRoom = getRoomInfo(roomName);
            return theRoom.FileLoc;
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

        private void updatePMList (PrivateMessage updated)
        {
            List<PrivateMessage> newList = pmList;
            for (int i = 0; i < pmList.Count; i++)
            {
                if (pmList[i].Sender.Equals(updated.Sender))
                {
                    if (pmList[i].Recipient.Equals(updated.Recipient))
                    {
                        newList.RemoveAt(i);
                    }
                }
                else if (pmList[i].Sender.Equals(updated.Recipient))
                {
                    if (pmList[i].Recipient.Equals(updated.Sender))
                    {
                        newList.RemoveAt(i);
                    }
                }
                else if (pmList[i].Recipient.Equals(updated.Sender))
                {
                    if (pmList[i].Sender.Equals(updated.Recipient))
                    {
                        newList.RemoveAt(i);
                    }
                }
                else if (pmList[i].Recipient.Equals(updated.Recipient))
                {
                    if (pmList[i].Sender.Equals(updated.Sender))
                    {
                        newList.RemoveAt(i);
                    }
                }
            }
            pmList = newList;
            pmList.Add(updated);
        }

        public void addPrivateMessage (string username, string contactName, string message, bool isFile)
        {
            PrivateMessage conversation = null; //the conversation
            List<string> messageList;
            foreach(PrivateMessage pm in pmList)
            {
                if (pm.Sender.Equals(username))
                {
                    if (pm.Recipient.Equals(contactName))
                    {
                        conversation = pm;
                    }
                }
                else if (pm.Sender.Equals(contactName))
                {
                    if (pm.Recipient.Equals(username))
                    {
                        conversation = pm;
                    }
                }
                else if (pm.Recipient.Equals(contactName))
                {
                    if (pm.Sender.Equals(username))
                    {
                        conversation = pm;
                    }
                }
                else if (pm.Recipient.Equals(username))
                {
                    if (pm.Sender.Equals(contactName))
                    {
                        conversation = pm;
                    }
                }
            }
            if (conversation == null) //the conversation havent exist before
            {
                conversation = new PrivateMessage();
                conversation.Sender = username;
                conversation.Recipient = contactName;
                messageList = new List<string>();
                messageList.Add(message);
                pmList.Add(conversation);
            }
            else //conversation between two parties already exist
            {
                messageList = conversation.Messages;
                messageList.Add(message);
                updatePMList(conversation);
            }
            if (isFile)
            {
                int locNo = conversation.Messages.Count - 1;
                List<int> temp = conversation.FileLoc;
                temp.Add(locNo);
                conversation.FileLoc = temp;
            }
            conversation.Messages = messageList;
        }

        public List<string> getPrivateMessages (string username, string contactName)
        {
            List<string> messageList;
            PrivateMessage conversation = null;
            foreach (PrivateMessage pm in pmList)
            {
                if (pm.Sender.Equals(username))
                {
                    if (pm.Recipient.Equals(contactName))
                    {
                        conversation = pm;
                    }
                }
                else if (pm.Sender.Equals(contactName))
                {
                    if (pm.Recipient.Equals(username))
                    {
                        conversation = pm;
                    }
                }
                else if (pm.Recipient.Equals(contactName))
                {
                    if (pm.Sender.Equals(username))
                    {
                        conversation = pm;
                    }
                }
                else if (pm.Recipient.Equals(username))
                {
                    if (pm.Sender.Equals(contactName))
                    {
                        conversation = pm;
                    }
                }
            }
            if (conversation != null)
            {
                messageList = conversation.Messages;
            }
            else
            {
                messageList = new List<string>();
            }
            return messageList;
        }

        public List<int> getPMFileLoc(String username, string contactName)
        {
            List<int> fileLoc;
            PrivateMessage conversation = null;
            foreach (PrivateMessage pm in pmList)
            {
                if (pm.Sender.Equals(username))
                {
                    if (pm.Recipient.Equals(contactName))
                    {
                        conversation = pm;
                    }
                }
                else if (pm.Sender.Equals(contactName))
                {
                    if (pm.Recipient.Equals(username))
                    {
                        conversation = pm;
                    }
                }
                else if (pm.Recipient.Equals(contactName))
                {
                    if (pm.Sender.Equals(username))
                    {
                        conversation = pm;
                    }
                }
                else if (pm.Recipient.Equals(username))
                {
                    if (pm.Sender.Equals(contactName))
                    {
                        conversation = pm;
                    }
                }
            }
            if (conversation == null)
            {
                fileLoc = new List<int>();
            }
            else
            {
                fileLoc = conversation.FileLoc;
            }
            return fileLoc;
        }
    }
}
