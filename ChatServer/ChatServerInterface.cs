using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    [ServiceContract]
    public interface ChatServerInterface
    {
        [OperationContract]
        void hasJoinedChat(User user);

        [OperationContract]
        void hasLeftChat(User user);

        [OperationContract]
        void sendMessage(User sender, string message);

        [OperationContract]
        void sendPrivateMessage(User sender, User recipient, string message);

        [OperationContract]
        void sendFiles(User sender, string files);
    }
}
