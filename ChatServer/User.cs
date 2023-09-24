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
        private int ID;
        private PrivateMessage privateMessages;

        public User(string userName, int ID)
        {
            this.userName = userName;
            this.ID = ID;
            privateMessages = new PrivateMessage();
        }

        public void setUserName(String userName)
        {
            this.userName = userName;
        }
        
        public void setID(int ID) { this.ID = ID;}

        public String getUserName() { return this.userName;}

        public int getID() { return this.ID;}

        public void addPrivateMessage(User sender, String message) 
        {
            privateMessages.addMessage(sender, message);
        }
    }
}
