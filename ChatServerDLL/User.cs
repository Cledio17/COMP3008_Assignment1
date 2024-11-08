﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChatServerDLL
{
    [DataContract]
    public class User
    {
        private string userName;
        private int ID;
        private bool loggedIn = false;
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
        public bool LoggedIn
        {
            get { return loggedIn; }
            set { loggedIn = value; }
        }

        [DataMember]
        public List<string> JoinedRooms
        { 
            get { return chatRooms; } 
            set { chatRooms = value; }
        }
    }
}
