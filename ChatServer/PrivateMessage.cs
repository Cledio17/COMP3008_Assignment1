using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class PrivateMessage
    {
        private List<User> userList;
        private List<String> messages;

        public PrivateMessage() 
        {
            userList = new List<User>();
            messages = new List<String>();
        }

        public void addMessage(User sender, String message)
        {
            userList.Add(sender);
            messages.Add(message);
        }
    }
}
