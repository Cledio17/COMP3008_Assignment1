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
        private string userName;
        private int ID;
        private List<string> chatRooms = new List<string> { };

        [DataMember]
        public string Username
        {
            get { return userName; }
            set { userName = value; }
        }

        [DataMember]
        public int UserID
        {
            get { return ID; }
            set { ID = value; }
        }

        [DataMember]
        public List<string> JoinedRooms
        { 
            get { return chatRooms; } 
            set { chatRooms = value; }
        }
    }
}
