using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

using TGL.Client.Model;
using TGL.Client.View;
using TGL.Model;
using TGL.Controller;

namespace TGL.Client.Controller
{
    /// <summary>
    /// Controller class for client application.
    /// </summary>
    public class ClientController : DefaultController
    {
        private ClientThread process;
        private FileManager manager;
        private IClientView view;

        public ClientController(FileManager manager, IClientView view)
        {
            this.manager = manager;
            this.view = view;
            this.view.deconnectionEvent += new newDeconnectionEvent(deconnectionEvent);
            this.view.connectionEvent += new newConnectionEvent(connectionEvent);
            this.view.loginEvent += new newLoginEvent(loginEvent);
            this.view.abortEvent += new newAbortEvent(abortEvent);
            this.view.requestEvent += new newRequestEvent(requestEvent);
            this.view.responseEvent += new newResponseEvent(responseEvent);
            this.view.quitGameEvent += new newQuitGameEvent(quitGameEvent);
            this.view.sendMsgEvent += new newSendMsgEvent(sendMsgEvent);
            this.view.endGameEvent += new newEndGameEvent(endGameEvent);
            this.view.gameEvent += new newGameEvent(gameEvent);
            this.view.showView(manager.getUnique());
        }
        /// <summary>
        /// Send an end game message to the server.
        /// </summary>
        /// <param name="gameStuff"></param>
        /// <param name="state"></param>
        private void endGameEvent(Object gameStuff, GameState state)
        {
            CSMessage msg = new CSMessage(MessageType.END_GAME);
            msg.add(MessageType.GAME_STUFF, gameStuff);
            if (state == GameState.WON) msg.add(MessageType.WIN_GAME, true);
            else msg.add(MessageType.DRAW_GAME, true);
            process.send(msg);
        }
        /// <summary>
        /// Send a game message to the server.
        /// </summary>
        /// <param name="gameStuff"></param>
        private void gameEvent(Object gameStuff)
        {
            CSMessage tosend = new CSMessage(MessageType.IN_GAME).
                add(MessageType.GAME_STUFF, gameStuff);
            process.send(tosend);
        }
        /// <summary>
        /// Send a message to the server.
        /// </summary>
        /// <param name="msg"></param>
        private void sendMsgEvent(string msg)
        {
            CSMessage tosend = new CSMessage(MessageType.SEND_MSG).
                add(MessageType.USER, process.ClientUser).
                add(MessageType.MESSAGE, msg);
            process.send(tosend);
        }
        /// <summary>
        /// Send quit game message to the server.
        /// </summary>
        private void quitGameEvent()
        {
            CSMessage msg = new CSMessage(MessageType.STOP_GAME);
            process.send(msg);
        }
        /// <summary>
        /// Send an abort message to the server.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="sent"></param>
        private void abortEvent(CSUser user, bool sent)
        {
            if (sent)
                process.To = null;
            else
                process.Froms.Remove(user);
            CSMessage msg = new CSMessage(MessageType.CHALLENGING).
                add(MessageType.ABORT, true).
                add(MessageType.USER, user);
            process.send(msg);
        }
        /// <summary>
        /// Send a game request message to the server.
        /// </summary>
        /// <param name="user"></param>
        private void requestEvent(CSUser user)
        {
            process.To = user;
            CSMessage msg = new CSMessage(MessageType.CHALLENGING).
                add(MessageType.REQUEST, true).
                add(MessageType.USER, user);
            process.send(msg);
        }
        /// <summary>
        /// Send a game response message to the server.
        /// </summary>
        /// <param name="user"></param>
        private void responseEvent(CSUser user)
        {
            CSMessage msg = new CSMessage(MessageType.CHALLENGING).
                add(MessageType.RESPONSE, true).
                add(MessageType.USER, user);
            process.send(msg);
        }
        /// <summary>
        /// Start the client process and connect it with the server.
        /// </summary>
        /// <param name="tcpclient"></param>
        private void connectionEvent(TcpClient tcpclient)
        {
            process = new ClientThread(tcpclient);
            process.messageHandler = new UserThread.newMessageHandler(handleMessage);
            process.loginHandler = new UserThread.newLoginHandler(loginHandler);
            process.challengerHandler = new UserThread.newChallengerHandler(challengerHandler);
            process.connectionHandler = new ClientThread.newConnectionHandler(connectionHandler);
            process.deconnectionHandler = new UserThread.newDeconnectionHandler(deconnectionHandler);
            process.endGameHandler = new UserThread.newEndGameHandler(endGameHandler);
            process.stopGameHandler = new UserThread.newStopGameHandler(stopGameHandler);
            process.userListHandler = new ClientThread.newUserListHandler(userListHandler);
            process.clearHandler = new ClientThread.newClearHandler(clearHandler);
            process.startGameHandler = new ClientThread.newStartGameHandler(startGameHandler);
            process.receiveMsgHandler = new ClientThread.newReceiveHandler(receiveMsgHandler);
            process.gameHandler = new ClientThread.newGameHandler(gameHandler);
            process.start();
        }
        /// <summary>
        /// Send a login message to the server.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enrolled">True if the given user is registered</param>
        /// <param name="save">True if the server should save the given user</param>
        private void loginEvent(CSUser user, bool enrolled, bool save)
        {
            if (save) manager.createUnique(user);
            CSMessage msg = new CSMessage(MessageType.LOGIN);
            msg.add(MessageType.USER, user);
            if (enrolled) msg.add(MessageType.REGISTRATION, true);
            process.send(msg);
        }
        /// <summary>
        /// Send a deconnection message to the server.
        /// </summary>
        private void deconnectionEvent()
        {
            CSMessage msg = new CSMessage(MessageType.DECONNECTION);
            process.send(msg);
            process.stop();
        }

        protected void receiveMsgHandler(UserThread client, CSMessage msg)
        {
            try
            {
                CSUser user = (CSUser) msg.get(MessageType.USER);
                string message = (string)msg.get(MessageType.MESSAGE);
                if (view.isOpened()) view.receiveMsgHandler(user, message);
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller [ReceiveMsg] - " + e.Message);
            }
        }

        protected void userListHandler(UserThread client, CSMessage msg)
        {
            try
            {
                List<CSUser> users = (List<CSUser>)msg.get(MessageType.USER_LIST);
                users.Remove(client.ClientUser);
                if (view.isOpened()) view.userListHandler(users);
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller [UserList] - " + e.Message);
            }
        }

        protected void clearHandler(UserThread client)
        {
            if (view.isOpened()) view.clearHandler();
        }

        protected void startGameHandler(UserThread client, CSMessage msg)
        {
            try
            {
                CSUser user = (CSUser)msg.get(MessageType.USER);
                bool play = (bool)msg.get(MessageType.PLAY_GAME);
                client.To = null;
                client.Froms.Clear();
                if (view.isOpened()) view.startGameHandler(user, play);
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller[StartGame] - " + e.Message);
            }
        }

        protected override void endGameHandler(UserThread client, CSMessage msg)
        {
            try
            {
                CSUser challenger = (CSUser) msg.get(MessageType.USER);
                bool play = (bool) msg.get(MessageType.PLAY_GAME);
                Object gameStuff = (Object)msg.get(MessageType.GAME_STUFF);
                GameState state = GameState.LOST;

                if (msg.get(MessageType.WIN_GAME) != null)
                {
                    client.ClientUser.win();
                    state = GameState.WON;
                }
                else if (msg.get(MessageType.DRAW_GAME) != null)
                {
                    client.ClientUser.draw();
                    state = GameState.DRAW;
                }

                client.ClientUser.incrementParties();
                if (view.isOpened()) view.endGameHandler(client.ClientUser, challenger, state, play, gameStuff);
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller[EndGame] - " + e.Message);
            }
        }

        protected void gameHandler(UserThread client, CSMessage msg)
        {
            try
            {
                Object gameStuff = msg.get(MessageType.GAME_STUFF);
                if (view.isOpened()) view.gameHandler(gameStuff);
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller[InGame] - " + e.Message);
            }
        }

        protected override void stopGameHandler(UserThread client, CSMessage msg)
        {
            try
            {
                CSUser user = (CSUser)msg.get(MessageType.USER);
                if (view.isOpened()) view.stopGameHandler(user);
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller[StopGame] - " + e.Message);
            }
        }

        protected override void loginHandler(UserThread client, CSMessage msg)
        {
            try
            {
                if ((bool)msg.get(MessageType.STATE))
                {
                    client.ClientUser = (CSUser)msg.get(MessageType.USER);
                    if (view.isOpened()) view.loginHandler(client.ClientUser);
                }
                else
                {
                    if (view.isOpened())
                    {
                        view.showRegisterOption();
                        view.connectionHandler(client.ClientUser);
                    }
                    log(Severiry.ERROR, (string)msg.get(MessageType.ERROR));
                }
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller - " + e.Message);
            }
        }

        protected override void challengerHandler(UserThread client, CSMessage msg)
        {
            try
            {
                CSUser user = (CSUser)msg.get(MessageType.USER);

                if (msg.get(MessageType.REQUEST) != null)
                {
                    client.Froms.Add(user);
                    if (view.isOpened()) view.requestHandler(user);
                }
                else if ((msg.get(MessageType.ABORT) != null))
                {
                    if (user.Equals(client.To))
                    {
                        client.To = null;
                        if (view.isOpened()) view.abortHandler(user, true);
                    }
                    else
                    {
                        client.Froms.Remove(user);
                        if (view.isOpened()) view.abortHandler(user, false);
                    }
                }
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller [Challenging] - " + e.Message);
            }
        }

        protected void connectionHandler(UserThread client, CSMessage msg)
        {
            try
            {
                CSUser user = (CSUser)msg.get(MessageType.USER);
                client.ClientUser = user;
                if (view.isOpened()) view.connectionHandler(user);
            }
            catch (Exception e)
            {
                log(Severiry.ERROR, "Controller[Connection] - " + e.Message);
            }
        }

        protected override void deconnectionHandler(UserThread client)
        {
            if (view.isOpened())
            {
                view.deconnectionHandler();
                log(Severiry.WARNING, "Server stopped");
            }
        }

        protected override void handleMessage(KeyValuePair<Severiry, string> msg)
        {
            if (view.isOpened()) view.printCSMessage(msg);
        }
    }
}
