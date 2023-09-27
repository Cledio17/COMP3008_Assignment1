using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChatServerDLL
{
    [DataContract]
    public class PrivateMessage
    {
        private string sender;
        private string recipient;
        private List<string> messages = new List<string> ();
        private List<int> fileLoc = new List<int>();

        [DataMember]
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        [DataMember]
        public string Recipient
        {
            get { return recipient; }
            set { recipient = value; }
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
