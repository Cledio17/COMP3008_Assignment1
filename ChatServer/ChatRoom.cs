using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChatServer
{
    [DataContract]
    public class ChatRoom
    {
        [DataMember]
        string chatRoomName;
        [DataMember]
        int id;
        [DataMember]
        List<User> _users;
        [DataMember]
        List<string> messageby;
        [DataMember]
        List<string> messages;
        public ChatRoom(string chatRoomName, int id) 
        { 
            this.chatRoomName = chatRoomName; 
            this.id = id;
            _users = new List<User>(); 
            messageby = new List<string>(); 
            messages = new List<string>(); 
        }

        public string getChatRoomName() { return this.chatRoomName; }

        public int getId() { return this.id; }

        public List<User> getUser() { return this._users; }

        public List<string> getMessagesBy() { return this.messageby; }

        public List<string> getMessages() { return this.messages; }

        public void addUser(User user)
        {
            string username = user.getUserName();
            if(checkJoined(username))
            {
                MessageBox.Show("The user " + user.getUserName() + "has already existed in this server.");
            }
            else
            {
                _users.Add(user);
                addMessages("System", username + " has joined the chat.");
            }
        }

        private bool checkJoined(string username)
        {
            bool joined = false;
            foreach(User user in _users)
            {
                if (user.getUserName().Equals(username))
                {
                    joined = true;
                }
            }
            return joined;
        }

        public void removeUser(User user)
        {
            List<User> temp = _users;
            string username = user.getUserName();
            for (int i = 0; i < _users.Count; i++)
            {
                if (temp[i].getUserName().Equals(username))
                {
                    _users.RemoveAt(i);
                    addMessages("System", username + " has left the chat.");
                }
            }
        }

        public void addMessages(string messagebys, string message)
        {
            messageby.Add(messagebys);
            messages.Add(message);
            Console.WriteLine(messagebys + ": " + message); //displaying message in the server, might change the way of display in the future
        }

        /*public void sendPrivateMessage(User sender, User recipient, string message)
        {
            recipient.addPrivateMessage(sender, message);
        }

        public void sendFiles(User sender, string files)
        {
            throw new NotImplementedException();
        }*/
    }
}
