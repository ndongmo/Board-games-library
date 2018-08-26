using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TGL.Model
{
    public abstract class UserThread : ILogger
    {
        private CSUser user;
        private CSUser to;
        private List<CSUser> froms = new List<CSUser>();

        public const string DEFAULTNAME = "ANONYMOUS";
        private Thread thread;
        private TcpClient tcpclient;
        private IFormatter formatter;
        private CancellationTokenSource cts;
        private readonly object sync = new object();

        public delegate void newEndGameHandler(UserThread ut, CSMessage msg);
        public delegate void newStopGameHandler(UserThread ut, CSMessage msg);
        public delegate void newLoginHandler(UserThread ut, CSMessage msg);
        public delegate void newChallengerHandler(UserThread ut, CSMessage msg);
        public delegate void newDeconnectionHandler(UserThread ut);
        public delegate void newMessageHandler(KeyValuePair<Severiry, string> msg);

        public newEndGameHandler endGameHandler;
        public newStopGameHandler stopGameHandler;
        public newLoginHandler loginHandler;
        public newChallengerHandler challengerHandler;
        public newMessageHandler messageHandler;
        public newDeconnectionHandler deconnectionHandler;

        public UserThread(TcpClient tcpclient)
        {
            this.tcpclient = tcpclient;
            this.formatter = new BinaryFormatter();
            this.user = new CSUser(DEFAULTNAME);
        }

        public List<CSUser> Froms { get { return froms; } }
        public CSUser ClientUser { get { return user; } set { user = value; } }
        public CSUser To { get { return to; } set { to = value; } }
        /// <summary>
        /// Check if the current user is logged.
        /// </summary>
        /// <returns></returns>
        public bool isLogged()
        {
            return ! user.Login.StartsWith(DEFAULTNAME);
        }
        /// <summary>
        /// Check if the current process has started.
        /// </summary>
        /// <returns></returns>
        public bool isStarted()
        {
            return cts != null && !cts.IsCancellationRequested;
        }
        /// <summary>
        /// Start the current process.
        /// </summary>
        public void start()
        {
            try
            {
                lock (sync)
                {
                    if (tcpclient != null && (thread == null || !thread.IsAlive))
                    {
                        cts = new CancellationTokenSource();
                        thread = new Thread(() => run(cts.Token))
                        {
                            IsBackground = true
                        };
                        thread.Start();
                    }
                    else
                        log(Severiry.WARNING, "Attempt to restart when it is already started !");
                }
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Starting -> " + e.Message);
            }
        }
        /// <summary>
        /// Stop the current process.
        /// </summary>
        public void stop()
        {
            try
            {
                if (tcpclient.Connected && thread != null && thread.IsAlive)
                {
                    lock (sync)
                    {
                        cts.Cancel();
                        tcpclient.Close();
                        log(Severiry.INFO, "Stopped");
                    }
                }
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Stopping -> " + e.Message);
            }
        }

        public void log(Severiry severity, string msg)
        {
            messageHandler(new KeyValuePair<Severiry, string>(severity, "Client[" + user.Login + "] - " + msg));
        }
        /// <summary>
        /// Send a message by writing on the socket.
        /// </summary>
        /// <param name="msg"></param>
        public void send(CSMessage msg)
        {
            try
            {
                formatter.Serialize(tcpclient.GetStream(), msg);
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Sending -> " + e.Message);
            }
        }
        /// <summary>
        /// Process run method.
        /// </summary>
        /// <param name="token"></param>
        public void run(CancellationToken token)
        {
            while (tcpclient.Connected && !token.IsCancellationRequested)
            {
                try
                {
                    CSMessage msg = (CSMessage)formatter.Deserialize(tcpclient.GetStream());
                    switch (msg.Type)
                    {
                        case MessageType.DECONNECTION:
                            deconnetion();
                            break;
                        case MessageType.LOGIN:
                            loginHandler(this, msg);
                            break;
                        case MessageType.CHALLENGING:
                            challengerHandler(this, msg);
                            break;
                        case MessageType.STOP_GAME:
                            stopGameHandler(this, msg);
                            break;
                        case MessageType.END_GAME:
                            endGameHandler(this, msg);
                            break;
                        default:
                            handleMessage(msg);
                            break;
                    }

                }
                catch (Exception)
                {
                    if (!token.IsCancellationRequested) deconnetion();
                }
            }
        }
        /// <summary>
        /// Stop the current process.
        /// </summary>
        public void deconnetion()
        {
            stop();
            deconnectionHandler(this);
        }

        public abstract void handleMessage(CSMessage msg);
    }
}
