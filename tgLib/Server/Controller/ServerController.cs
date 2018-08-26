using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TGL.Model;
using TGL.Controller;
using TGL.Server.View;
using TGL.Server.Model;

namespace TGL.Server.Controller
{
    /// <summary>
    /// Controller class for sever application.
    /// </summary>
    public class ServerController : DefaultController
    {
        private ServerProcess ps;
        private DataManager manager;
        private IServerView view;

        private List<ServerThread> clients = new List<ServerThread>();
        /// <summary>
        /// Initialize variables. If a manager isn't given, it uses the FileManager as
        /// DataManager. Also, if a view isn't provided, it starts the process in background.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="view"></param>
        /// <param name="port"></param>
        public ServerController(DataManager manager, IServerView view, int port = 4445)
        {
            this.ps = new ServerProcess();
            this.ps.clientHandler = new ServerProcess.newClientHandler(incomingClient);
            this.ps.messageHandler = new ServerProcess.newMessageHandler(handleMessage);

            this.manager = (manager != null) ? manager : new FileManager(true);

            if (view != null)
            {
                this.view = view;
                this.view.startServerHandler += new newStartServerHandler(startServer);
                this.view.stopServerHandler += new newStopServerHandler(stopServer);
                this.view.showView();
            }
            else
                ps.start(port, false);
        }
        /// <summary>
        /// Initialize variables. If a manager isn't given, it uses the FileManager as
        /// DataManager.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="port"></param>
        public ServerController(DataManager manager, int port) : this(manager, null, port) { }
        /// <summary>
        /// Initialize variables. If a view isn't provided, it starts the process in background.
        /// </summary>
        /// <param name="view"></param>
        public ServerController(IServerView view) : this(null, view) { }
        /// <summary>
        /// Manage incoming message from the given client's socket.
        /// </summary>
        /// <param name="tcpclient"></param>
        private void incomingClient(TcpClient tcpclient)
        {
            ServerThread client = new ServerThread(tcpclient);

            client.messageHandler = new UserThread.newMessageHandler(handleMessage);
            client.loginHandler = new UserThread.newLoginHandler(loginHandler);
            client.challengerHandler = new UserThread.newChallengerHandler(challengerHandler);
            client.deconnectionHandler = new UserThread.newDeconnectionHandler(deconnectionHandler);
            client.endGameHandler = new UserThread.newEndGameHandler(endGameHandler);
            client.stopGameHandler = new UserThread.newStopGameHandler(stopGameHandler);

            CSMessage tosend = new CSMessage(MessageType.CONNECTION);
            tosend.add(MessageType.USER, client.ClientUser);
            client.send(tosend);
            client.start();
            clients.Add(client);
            broadcast();

            if (view != null && view.isOpened()) view.addUser(client.ClientUser);
        }

        protected override void endGameHandler(UserThread client, CSMessage msg)
        {
            try
            {
                ServerThread cl1 = (ServerThread)client;
                ServerThread cl2 = cl1.CurrentChallenger;
                CSMessage msg2 = new CSMessage(MessageType.END_GAME).
                    add(MessageType.GAME_STUFF, msg.get(MessageType.GAME_STUFF));

                bool play = new Random().Next(2) != 0;

                if (msg.get(MessageType.WIN_GAME) != null)
                {
                    cl1.ClientUser.win();
                    msg2.add(MessageType.LOSE_GAME, true);
                }
                if (msg.get(MessageType.DRAW_GAME) != null)
                {
                    cl1.ClientUser.draw();
                    cl2.ClientUser.draw();
                    msg2.add(MessageType.DRAW_GAME, true);
                }

                cl1.ClientUser.incrementParties();
                cl2.ClientUser.incrementParties();
                if (cl1.isLogged()) manager.update(cl1.ClientUser);
                if (cl2.isLogged()) manager.update(cl2.ClientUser);
                if (view != null && view.isOpened()) view.update(cl1.ClientUser.Login, cl1.ClientUser);
                if (view != null && view.isOpened()) view.update(cl2.ClientUser.Login, cl2.ClientUser);

                msg.add(MessageType.USER, cl2.ClientUser);
                msg.add(MessageType.PLAY_GAME, play);
                msg2.add(MessageType.USER, cl1.ClientUser);
                msg2.add(MessageType.PLAY_GAME, !play);
                cl1.send(msg);
                cl2.send(msg2);
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller[ENDGAME] - " + e.Message);
            }
        }

        protected override void stopGameHandler(UserThread client, CSMessage msg)
        {
            try
            {
                ServerThread cl1 = (ServerThread)client;
                if (cl1.playing())
                {
                    ServerThread cl2 = cl1.CurrentChallenger;
                    if (cl2.playing())
                    {
                        msg.add(MessageType.USER, cl1.ClientUser);
                        cl2.send(msg);
                    }
                    cl2.CurrentChallenger = null;
                    cl1.CurrentChallenger = null;
                    if (view != null && view.isOpened()) view.removeParty(cl1.ClientUser, cl2.ClientUser);

                    broadcast();
                }
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller[STOPGAME] - " + e.Message);
            }
        }

        protected override void loginHandler(UserThread client, CSMessage msg)
        {
            try
            {
                CSUser user = (CSUser)msg.get(MessageType.USER);
                CSUser tempUser = new CSUser(client.ClientUser.Login);
                bool error = true;

                if (msg.get(MessageType.REGISTRATION) != null)
                {
                    msg.clear();
                    if (manager.exist(user.Login))
                    {
                        msg.add(MessageType.STATE, false).add(MessageType.ERROR, "Login '" + user.Login + "' already exists !");
                    }
                    else
                    {
                        manager.create(user);
                        if (view != null && view.isOpened()) view.update(client.ClientUser.Login, user);
                        client.ClientUser.Login = user.Login;
                        msg.add(MessageType.STATE, true).add(MessageType.USER, client.ClientUser);
                        error = false;
                    }
                }
                else if (findClient(user) == null)
                {
                    msg.clear();
                    if ((user = manager.find(user)) != null)
                    {
                        client.ClientUser.Points = user.Points;
                        client.ClientUser.NbParties = user.NbParties;
                        if (view != null && view.isOpened()) view.update(client.ClientUser.Login, user);
                        client.ClientUser.Login = user.Login;
                        msg.add(MessageType.STATE, true).add(MessageType.USER, client.ClientUser);
                        error = false;
                    }
                    else
                    {
                        msg.add(MessageType.STATE, false).add(MessageType.ERROR, "Incorrect login or password !");
                    }
                }
                else
                    msg.add(MessageType.STATE, false).add(MessageType.ERROR, "Login already connected !");

                if (!error)
                {
                    abortRequest(client, tempUser);
                    foreach (CSUser u in client.Froms)
                        abortRequest(u, tempUser);
                    client.To = null;
                    client.Froms.Clear();
                    CSMessage clearmsg = new CSMessage(MessageType.CLEAR);
                    client.send(clearmsg);
                }

                client.send(msg);
                if (!error) broadcast();
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Manager[" + manager + "] - " + e.Message);
            }
        }
        /// <summary>
        /// Abort the party between the given users.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="user"></param>
        private void abortRequest(UserThread client, CSUser user)
        {
            if (client.To != null)
            {
                ServerThread old;
                if ((old = findClient(client.To)) != null)
                {
                    CSMessage msg = new CSMessage(MessageType.CHALLENGING).
                        add(MessageType.ABORT, true).
                        add(MessageType.USER, user);
                    old.send(msg);
                    old.Froms.Remove(client.ClientUser);
                }
            }
        }
        /// <summary>
        /// Send the abort message form the given user 'from' to the given user 'to'.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        private void abortRequest(CSUser to, CSUser from)
        {
            ServerThread cl = findClient(to);
            if (cl != null)
            {
                CSMessage msg = new CSMessage(MessageType.CHALLENGING)
                    .add(MessageType.ABORT, true)
                    .add(MessageType.USER, from);
                cl.send(msg);
                cl.To = null;
            }
        }

        private void broadcast(CSMessage msg)
        {
            foreach (ServerThread cl in clients)
                cl.send(msg);
        }
        /// <summary>
        /// Send a broadcast message.
        /// </summary>
        private void broadcast()
        {
            CSMessage msg = new CSMessage(MessageType.USER_LIST);
            List<CSUser> users = new List<CSUser>();

            foreach (ServerThread cl in clients)
            {
                if (!cl.playing())
                    users.Add(cl.ClientUser);
            }

            msg.add(MessageType.USER_LIST, users);

            foreach (ServerThread cl in clients)
            {
                if (!cl.playing())
                    cl.send(msg);
            }
        }
        /// <summary>
        /// Start a party between the users behind the given threads.
        /// </summary>
        /// <param name="c1">First user thread</param>
        /// <param name="c2">Second user thread</param>
        private void beginGame(ServerThread c1, ServerThread c2)
        {
            c1.CurrentChallenger = c2;
            c2.CurrentChallenger = c1;

            bool play = new Random().Next(2) != 0;

            CSMessage startMsg = new CSMessage(MessageType.START_GAME);
            startMsg.add(MessageType.PLAY_GAME, play);
            startMsg.add(MessageType.USER, c2.ClientUser);
            c1.send(startMsg);

            startMsg.add(MessageType.PLAY_GAME, !play);
            startMsg.add(MessageType.USER, c1.ClientUser);
            c2.send(startMsg);

            broadcast();

            if (view != null && view.isOpened()) view.newParty(c1.ClientUser, c2.ClientUser);
        }

        protected override void challengerHandler(UserThread client, CSMessage msg)
        {
            try
            {
                ServerThread to;
                CSUser user = (CSUser)msg.get(MessageType.USER);

                if ((to = findClient(user)) != null && !to.playing())
                {
                    if (msg.get(MessageType.REQUEST) != null)
                    {
                        if (!user.Equals(client.To) && !client.Froms.Contains(user))
                        {
                            abortRequest(client, client.ClientUser);
                            client.To = to.ClientUser;
                            to.Froms.Add(client.ClientUser);
                            msg.add(MessageType.USER, client.ClientUser);
                            to.send(msg);
                        }
                    }
                    else if (msg.get(MessageType.RESPONSE) != null)
                    {
                        if (client.Froms.Contains(user) && to.To.Equals(client.ClientUser))
                        {
                            abortRequest(client, client.ClientUser);
                            client.To = null;
                            client.Froms.Remove(to.ClientUser);
                            to.To = null;
                            foreach (CSUser u in client.Froms)
                                abortRequest(u, client.ClientUser);
                            foreach (CSUser u in to.Froms)
                                abortRequest(u, to.ClientUser);
                            client.Froms.Clear();
                            to.Froms.Clear();

                            beginGame((ServerThread)client, to);
                        }
                    }
                    else if (msg.get(MessageType.ABORT) != null)
                    {
                        if (user.Equals(client.To))
                        {
                            client.To = null;
                            to.Froms.Remove(client.ClientUser);
                        }
                        else
                        {
                            to.To = null;
                            client.Froms.Remove(user);
                        }
                        msg.add(MessageType.USER, client.ClientUser);
                        to.send(msg);
                    }
                }
                else
                {
                    msg.clear();
                    msg.Type = MessageType.CHALLENGING;
                    msg.add(MessageType.ABORT, true).add(MessageType.USER, user);
                    client.To = null;
                    client.Froms.Remove(user);
                    client.send(msg);
                }
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller[PARTY] - " + e.Message);
            }
        }

        protected override void deconnectionHandler(UserThread client)
        {
            if (!ps.isStarted()) return;
            try
            {
                ServerThread cl1 = (ServerThread)client;
                if (cl1.playing())
                {
                    ServerThread cl2 = cl1.CurrentChallenger;
                    CSMessage msg = new CSMessage(MessageType.STOP_GAME).add(MessageType.USER, cl1.ClientUser);
                    cl2.send(msg);
                    cl2.CurrentChallenger = null;
                    cl1.CurrentChallenger = null;
                    if (view != null && view.isOpened()) view.removeParty(cl1.ClientUser, cl2.ClientUser);
                }
                else
                {
                    abortRequest(client, client.ClientUser);
                    foreach (CSUser u in client.Froms)
                        abortRequest(u, client.ClientUser);
                }

                if (view != null && view.isOpened()) view.delete(cl1.ClientUser);
                cl1.stop();
                clients.Remove(cl1);

                broadcast();
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller[DECONNECTION] - " + e.Message);
            }

        }
        /// <summary>
        /// Start the server process on the given port.
        /// </summary>
        /// <param name="port"></param>
        private void startServer(int port)
        {
            if (!ps.isStarted()) ps.start(port);
            if (view != null && view.isOpened()) view.updateState(ps.isStarted());
        }
        /// <summary>
        /// Handle the stop server event.
        /// </summary>
        private void stopServer()
        {
            CSMessage msg = new CSMessage(MessageType.DECONNECTION);
            broadcast(msg);

            foreach (UserThread cl in clients)
                cl.stop();

            clients.Clear();

            if (ps.isStarted()) ps.stop();
            if (view != null && view.isOpened())
            {
                view.clearAll();
                view.updateState(ps.isStarted());
                ServerThread.initCounter();
            }
            else
                manager.close();
        }

        protected override void handleMessage(KeyValuePair<Severiry, string> msg)
        {
            if (view != null && view.isOpened()) view.printMessage(msg);
        }
        /// <summary>
        /// Find and return the thread of the given user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Return null if the given user doesn't exist</returns>
        private ServerThread findClient(CSUser user)
        {
            foreach (ServerThread client in clients)
                if (client.ClientUser.Equals(user))
                    return client;
            return null;
        }

    }
}
