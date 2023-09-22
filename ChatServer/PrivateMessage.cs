using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class PrivateMessage
    {
        private List<String> messages;

        public PrivateMessage() 
        {
            messages = new List<String>();
        }

        public void addMessage(User recipient, String message)
        {
            messages.Add(message);
        }
    }
}
