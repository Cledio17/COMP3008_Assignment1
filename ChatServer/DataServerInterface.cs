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
        //Users
        [OperationContract]
        User addUserAccountInfo(string username);

        [OperationContract]
        User getUserAccountInfo(string userName);

        [OperationContract]
        void updateUserAccountInfo(User user);

        [OperationContract]
        bool isUserNameAvailable(string userName);

        [OperationContract]
        void addJoinedServer(string userName, string roomName);

        [OperationContract]
        List<ChatRoom> getJoinedServers(string userName);

        //Chat Servers
        [OperationContract]
        ChatRoom addServer(string roomName);

        [OperationContract]
        HashSet<ChatRoom> getAllServers();
    }
}
