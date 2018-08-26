using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TGL.Model;

namespace TGL.Server.Model
{
    public class ServerThread : UserThread
    {
        private static int counter = 0;
        private ServerThread currentChallenger;

        public ServerThread(TcpClient tcpclient) : base(tcpclient) 
        {
            ClientUser.Login = DEFAULTNAME + "_" + counter++;
            ClientUser.Index = counter;
        }

        public ServerThread CurrentChallenger { get { return currentChallenger; } set { currentChallenger = value; } }
        public int Key { get { return ClientUser.Index; } }

        public override void handleMessage(CSMessage msg)
        {
            if (playing())
                currentChallenger.send(msg);
        }

        public bool playing()
        {
            return currentChallenger != null;
        }

        public static void initCounter()
        {
            counter = 0;
        }
    }
}