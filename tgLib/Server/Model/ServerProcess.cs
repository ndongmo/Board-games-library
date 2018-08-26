using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using TGL.Model;

namespace TGL.Server.Model
{
    /// <summary>
    /// Server process class manager.
    /// </summary>
    public class ServerProcess : ILogger
    {
        private int port;
        private Thread process;
        private TcpListener listener;
        private CancellationTokenSource cts;
        private readonly object sync = new object();

        public delegate void newMessageHandler(KeyValuePair<Severiry, string> msg);
        public delegate void newClientHandler(TcpClient client);

        public newMessageHandler messageHandler;
        public newClientHandler clientHandler;
        /// <summary>
        /// Start the server process on the given port number.
        /// </summary>
        /// <param name="port"></param>
        /// <param name="background"></param>
        public void start(int port, bool background = true)
        {
            this.port = port;
            try
            {
                lock (sync)
                {
                    if (process == null || !process.IsAlive)
                    {
                        listener = new TcpListener(IPAddress.Any, port);
                        cts = new CancellationTokenSource();
                        process = new Thread(() => run(cts.Token))
                        {
                            IsBackground = background
                        };
                        listener.Start();
                        process.Start();
                    }
                    else
                        log(Severiry.WARNING, "Attempt to restart when it is already started !");
                }
            }
            catch (Exception)
            {
                log(Severiry.ERROR, "Starting, may be this port is busy !");
            }
        }
        /// <summary>
        /// Stop the server process.
        /// </summary>
        public void stop()
        {
            try
            {
                if (listener != null && process != null && process.IsAlive)
                {
                    lock (sync)
                    {
                        cts.Cancel();
                        listener.Stop();
                        log(Severiry.INFO, "Stopped");
                    }
                }
            }
            catch (Exception)
            {
                log(Severiry.ERROR, "Stopping");
            }
        }
        /// <summary>
        /// Server process run method.
        /// </summary>
        /// <param name="token"></param>
        public void run(CancellationToken token)
        {
            log(Severiry.INFO, "Started");
            while (!token.IsCancellationRequested)
            {
                try
                {
                    clientHandler(listener.AcceptTcpClient());
                }
                catch (Exception)
                {
                    if(!token.IsCancellationRequested) stop();
                }
            }
        }
        /// <summary>
        /// Return true if the sever process is started.
        /// </summary>
        /// <returns></returns>
        public bool isStarted()
        {
            return cts != null && !cts.IsCancellationRequested;
        }

        public void log(Severiry severity, string msg)
        {
            messageHandler(new KeyValuePair<Severiry, string>(severity, "Server[" + port + "] - " + msg));
        }
    }
}
