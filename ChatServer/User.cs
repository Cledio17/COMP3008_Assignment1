using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string userName;
        [DataMember]
        public int ID;
        //private PrivateMessage privateMessages;
        [DataMember]
        public List<ChatRoom> chatRooms;

        public User(string userName, int ID)
        {
            this.userName = userName;
            this.ID = ID;
            //privateMessages = new PrivateMessage();
            chatRooms = new List<ChatRoom>();
        }

        public String getUserName() { return this.userName; }

        public int getID() { return this.ID; }

        public List<ChatRoom> getChatRooms() { return this.chatRooms; }

        /*public void addPrivateMessage(User sender, String message) 
        {
            privateMessages.addMessage(sender, message);
        }*/

        public void addChatRooms(ChatRoom chatRoom)
        {
            chatRooms.Add(chatRoom);
        }

        public void removeChatRooms(ChatRoom chatRoom)
        {
            List<ChatRoom> roomList = this.chatRooms;
            for (int i = 0; i < chatRooms.Count; i++)
            {
                if (roomList[i].getChatRoomName().Equals(chatRoom.getChatRoomName()))
                {
                    chatRooms.RemoveAt(i);
                }
            }
        }
    }
}
