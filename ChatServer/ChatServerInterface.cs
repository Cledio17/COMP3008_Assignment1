using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    [ServiceContract]
    public class ChatServerInterface
    {
        [OperationContract]
        Boolean hasJoinedChat(UserManager user);

        [OperationContract]
        Boolean hasLeftChat(UserManager user);

        [OperationContract]
        void sendMessage(UserManager sender, UserManager recipient, string message);

        [OperationContract]
        void sendPrivateMessage
    }
}
