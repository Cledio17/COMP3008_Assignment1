using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    [Serializable]
    public class ChatRoom
    {
        private String chatRoomName;
        private int id;
        private List<User> _users;
        private List<String> messageby;
        private List<String> messages;
        public ChatRoom(string chatRoomName, int id) 
        { 
            this.chatRoomName = chatRoomName; 
            this.id = id; _users = new List<User>(); 
            messageby = new List<string>(); 
            messages = new List<String>(); 
        }

        public bool addUser(User user)
        {
            bool added = false;
            if(_users.Contains(user))
            {
                MessageBox.Show("The user " + user.getUserName() + "has already existed in this server.");
            }
            else
            {
                _users.Add(user);
                added = true;
            }
            return added;
        }

        public List<User> getUser()
        {
            return _users;

        }

        public String getChatRoomName()
        {
            return chatRoomName;

        }

        public int getId() { return id; }

        public List<String> getMessagesBy()
        {
            return messageby;
        }

        public List<String> getMessages()
        {
            return messages;
        }

        public bool removeUser(User user)
        {
            bool removed = false;
            foreach (User temp in _users)
            {
                if (temp.getUserName().Equals(user.getUserName()))
                {
                    _users.Remove(user);
                    removed = true;
                }
            }
            if(!removed)
            {
                MessageBox.Show("The user " + user.getUserName() + "does not exist in the server.");
            }
            return removed;
        }

        public void addMessages(String messagebys, String message)
        {
            messageby.Add(messagebys);
            messages.Add(message);
            Console.WriteLine(messagebys + ": "+ message); //displaying message in the server, might change the way of display in the future
        }

        public void hasJoinedChat(User user)
        {
            _users.Add(user);
            // addMessages(user.getUserName() + " has joined the chat."); 
        }

        public void hasLeftChat(User user)
        {
            _users.Remove(user);
            // addMessages(user.getUserName() + " has left the chat.");
        }

        public void sendMessage(User sender, string message)
        {
            // addMessages(sender.getUserName() + ": \n" +  message);
        }

        public void sendPrivateMessage(User sender, User recipient, string message)
        {
            recipient.addPrivateMessage(sender, message);
        }

        public void sendFiles(User sender, string files)
        {
            throw new NotImplementedException();
        }
    }
}
