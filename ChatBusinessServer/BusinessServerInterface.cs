using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatBusinessServer
{
    [ServiceContract]
    public interface BusinessServerInterface
    {
        //Users
        [OperationContract]
        User addUserAccountInfo(string username);

        [OperationContract]
        User getUserAccountInfo(string userName);

        [OperationContract]
        bool checkIsUsernameExist(string userName);

        [OperationContract]
        void addJoinedServer(string userName, string roomName);

        [OperationContract]
        List<ChatRoom> getJoinedServers(string userName);

        //Chat Servers
        [OperationContract]
        void addServer(User user, string roomName);

        [OperationContract]
        ChatRoom getServerInfo(string roomName);

        [OperationContract]
        bool checkIsRoomNameExist(string roomName);

        [OperationContract]
        void addMessages(string messagebys, string message, string roomName);

        [OperationContract]
        List<ChatRoom> getAllServers();

        [OperationContract]
        void leaveRoom(string username, string roomName);
    }
}
