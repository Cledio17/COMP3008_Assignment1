using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChatServerDLL
{
    [DataContract]
    public class ChatRoom
    {
        private string chatRoomName;
        private int id;
        private List<string> _users = new List<string> { };
        private List<string> messages = new List<string> { };
        private List<int> fileLoc = new List<int>();

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
        public List<string> RoomUsers
        {
            get { return _users; }
            set { _users = value; }
        }

        [DataMember]
        public List<string> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        [DataMember]
        public List<int> FileLoc
        {
            get { return fileLoc; }
            set { fileLoc = value; }
        }
    }
}
