using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    [ServiceContract]
    public interface DataServerInterface
    {
        [OperationContract]
        void addUserAccountInfo(string username);

        [OperationContract]
        User getUserAccountInfo(string userName);

        [OperationContract]
        void updateUserAccountInfo(User user);

        [OperationContract]
        bool isUserNameAvailable(string userName);
    }
}
