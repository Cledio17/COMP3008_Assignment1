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

        public User(string userName, string ID)
        {
            this.userName = userName;
            this.ID = ID;
        }

        public void setUserName(String userName)
        {
            this.userName = userName;
        }
        
        public void setID(String ID) { this.ID = ID;}

        public String getUserName() { return this.userName;}

        public String getID() { return this.ID;}
    }
}
