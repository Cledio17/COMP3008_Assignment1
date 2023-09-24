using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class DatabaseClass
    {
        private static List<User> users;
        public DatabaseClass()
        {
            users = new List<User>();
        }

        public void addUserAccountInfo(User currentUser)
        {
            users.Add(currentUser);
        }

        public User getUserAccountInfo(String userName)
        {
            User temp = null;
            foreach (User user in users)
            {
                if (user.getUserName().Equals(userName))
                {
                    temp = user;
                }
            }
            return temp;
        }

        public void updateUserAccountInfo(User currentUser)
        {
            foreach (User user in users)
            {
                if (user.getUserName().Equals(currentUser.getUserName()))
                {
                    users.Remove(user);
                }
            }
            users.Add(currentUser);
        }

        public bool checkIsUserAvailable(string userName)
        {
            bool isExisted = false;
            foreach (User user in users)
            {
                if (user.getUserName().Equals(userName))
                {
                    isExisted = true;
                }
            }
            return isExisted;
        }
    }
}
