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
        private string chatRoomName;
        private int id;
        private List<User> _users = new List<User> { };
        private List<string> messageby = new List<string> { };
        private List<string> messages = new List<string> { };

        [DataMember]
        public string RoomName
        {
            get { return chatRoomName; }
            set { chatRoomName = value; }
        }

        [DataMember]
        public int RoomID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public List<User> RoomUsers
        {
            get { return _users; }
            set { _users = value; }
        }

        [DataMember]
        public List<string> MessagesBy
        {
            get { return messageby; }
            set { messageby = value; }
        }

        [DataMember]
        public List<string> Messages
        {
            get { return messages; }
            set { messages = value; }
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
