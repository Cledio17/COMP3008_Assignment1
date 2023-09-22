using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class ChatServer
    {
        private List<User> _users;
        public ChatServer() { _users = new List<User>(); }

        public void addUser(User user)
        {
            if(_users.Contains(user))
            {
                MessageBox.Show("The user " + user + "has already existed in this server.");
            }
            else
            {
                _users.Add(user);
            }
        }

        public void removeUser(User user)
        {
            _users.Remove(user);
        }
    }
}
