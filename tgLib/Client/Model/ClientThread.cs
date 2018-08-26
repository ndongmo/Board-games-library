using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TGL.Model;

namespace TGL.Client.Model
{
    /// <summary>
    /// Client process class. This class is inherited by remote client application.
    /// It defines special few delegates for client application.
    /// </summary>
    public class ClientThread : UserThread
    {
        public delegate void newConnectionHandler(UserThread ut, CSMessage msg);
        public delegate void newUserListHandler(UserThread ut, CSMessage msg);
        public delegate void newClearHandler(UserThread ut);
        public delegate void newStartGameHandler(UserThread ut, CSMessage msg);
        public delegate void newReceiveHandler(UserThread ut, CSMessage msg);
        public delegate void newGameHandler(UserThread ut, CSMessage msg);

        public newConnectionHandler connectionHandler;
        public newUserListHandler userListHandler;
        public newClearHandler clearHandler;
        public newStartGameHandler startGameHandler;
        public newReceiveHandler receiveMsgHandler;
        public newGameHandler gameHandler;

        public ClientThread(TcpClient tcpclient) : base(tcpclient)
        {
            
        }

        public override void handleMessage(CSMessage msg)
        {
            switch (msg.Type)
            {
                case MessageType.IN_GAME:
                    gameHandler(this, msg);
                    break;
                case MessageType.USER_LIST:
                    userListHandler(this, msg);
                    break;
                case MessageType.CLEAR:
                    clearHandler(this);
                    break;
                case MessageType.CONNECTION:
                    connectionHandler(this, msg);
                    break;
                case MessageType.START_GAME:
                    startGameHandler(this, msg);
                    break;
                case MessageType.SEND_MSG:
                    receiveMsgHandler(this, msg);
                    break;
                default:
                    break;
            }
        }
    }
}
