using ChatServerDLL;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace BusinessDataServer
{
    [ServiceContract]
    public interface BusinessServerInterface
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
        bool addJoinedServer(string userName, string roomName);

        [OperationContract]
        List<string> getJoinedServers(string userName);

        //Chat Servers
        [OperationContract]
        void addServer(string username, string roomName);

        [OperationContract]
        int getServerID(string roomName);

        [OperationContract]
        ChatRoom getServerInfo(string roomName);

        [OperationContract]
        bool checkIsRoomNameExist(string roomName);

        [OperationContract]
        void addMessages(string message, string roomName, bool isFile);

        [OperationContract]
        List<string> getMessages(string roomName);

        [OperationContract]
        List<string> getParticipants(string roomName);

        [OperationContract]
        List<string> getAllServers();

        [OperationContract]
        List<int> getFileLoc(String roomName);

        [OperationContract]
        void leaveRoom(string username, string roomName);

        [OperationContract]
        void addPrivateMessage(string username, string contactName, string message, bool isFile);

        [OperationContract]
        List<string> getPrivateMessages(string username, string contactName);

        [OperationContract]
        List<int> getPMFileLoc(String username, string contactName);
    }
}
