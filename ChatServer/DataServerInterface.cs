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
        int getUserID(string username);

        [OperationContract]
        User getUserAccountInfo(string userName);

        [OperationContract]
        bool checkIsUsernameExist(string userName);

        [OperationContract]
        void addJoinedServer(string userName, string roomName);

        [OperationContract]
        List<string> getJoinedServers(string userName);

        //Chat Servers
        [OperationContract]
        void addServer(string username, string roomName);

        [OperationContract]
        int getServerID (string roomName);

        [OperationContract]
        ChatRoom getServerInfo(string roomName);

        [OperationContract]
        bool checkIsRoomNameExist(string roomName);

        [OperationContract]
        void addMessages(string messagebys, string message, string roomName);

        [OperationContract]
        List<string> getAllServers();

        [OperationContract]
        void leaveRoom(string username, string roomName);
    }
}
