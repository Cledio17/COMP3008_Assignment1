using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class User
    {
        private String userName;
        private String ID;
        private PrivateMessage privateMessages;
        private List<ChatRoom> chatRooms;

        public User()
        {
            
        }

        public User(string userName, string ID)
        {
            this.userName = userName;
            this.ID = ID;
            privateMessages = new PrivateMessage();
            chatRooms = new List<ChatRoom>();
        }

        public void setUserName(String userName)
        {
            this.userName = userName;
        }
        
        public void setID(String ID) { this.ID = ID;}

        public String getUserName() { return this.userName;}

        public String getID() { return this.ID;}

        public void addPrivateMessage(User sender, String message) 
        {
            privateMessages.addMessage(sender, message);
        }

        public List<ChatRoom> getChatRooms() {  return chatRooms; }

        public void addChatRooms(ChatRoom chatRoom)
        {
            chatRooms.Add(chatRoom);
        }
    }
}
