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
    internal class DataServer : DataServerInterface
    {
        private DatabaseClass usersDatabase;
        public DataServer() 
        {
            usersDatabase = new DatabaseClass();
        }

        public void addUserAccountInfo(User user)
        {
            usersDatabase.addUserAccountInfo(user);
        }

        public User getUserAccountInfo(string userName)
        {
            return usersDatabase.getUserAccountInfo(userName);
        }

        public bool isUserNameAvailable(string userName)
        {
            return usersDatabase.checkIsUserAvailable(userName);
        }

        public void updateUserAccountInfo(User user)
        {
            usersDatabase.updateUserAccountInfo(user);
        }
    }
}
