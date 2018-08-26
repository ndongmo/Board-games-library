using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using TGL.Model;
using TGL.Client.Model;

namespace TGL.Client.View
{
    public delegate void newConnectionEvent(TcpClient tcpclient);
    public delegate void newDeconnectionEvent();
    public delegate void newLoginEvent(CSUser user, bool enroll, bool save);
    public delegate void newAbortEvent(CSUser user, bool sent);
    public delegate void newRequestEvent(CSUser user);
    public delegate void newResponseEvent(CSUser user);
    public delegate void newQuitGameEvent();
    public delegate void newEndGameEvent(Object gameStuff, GameState state);
    public delegate void newSendMsgEvent(string msg);
    public delegate void newGameEvent(Object gameStuff);

    /// <summary>
    /// Client view interface.
    /// </summary>
    public interface IClientView
    {
        event newConnectionEvent connectionEvent;
        event newDeconnectionEvent deconnectionEvent;
        event newLoginEvent loginEvent;
        event newAbortEvent abortEvent;
        event newRequestEvent requestEvent;
        event newResponseEvent responseEvent;
        event newQuitGameEvent quitGameEvent;
        event newSendMsgEvent sendMsgEvent;
        event newEndGameEvent endGameEvent;
        event newGameEvent gameEvent;

        /// <summary>
        /// Update the client's view with the given user and show it.
        /// </summary>
        /// <param name="user"></param>
        void showView(CSUser user);
        /// <summary>
        /// Return true if the client's view is visible.
        /// </summary>
        /// <returns></returns>
        bool isOpened();
        /// <summary>
        /// Show register options in case the current user is not registered.
        /// </summary>
        void showRegisterOption();
        /// <summary>
        /// Handle a deconnection event by updating the client's view.
        /// </summary>
        void deconnectionHandler();
        /// <summary>
        /// Handle a connection event.
        /// </summary>
        /// <param name="user"></param>
        void connectionHandler(CSUser user);
        /// <summary>
        /// Handle a login event by updating the client's view 
        /// with the given user.
        /// </summary>
        /// <param name="user">Logged user</param>
        void loginHandler(CSUser user);
        /// <summary>
        /// Handle an abort event comming from the given user.
        /// </summary>
        /// <param name="user">User which has aborted the game</param>
        /// <param name="sent">True if the abort message has already been sent</param>
        void abortHandler(CSUser user, bool sent);
        /// <summary>
        /// Handle a received game request from the given user.
        /// </summary>
        /// <param name="user">User which is asking for a party</param>
        void requestHandler(CSUser user);
        /// <summary>
        /// Handle a start game event with the given challenger.
        /// </summary>
        /// <param name="user">challenger</param>
        /// <param name="play">True if the current user should play first</param>
        void startGameHandler(CSUser user, bool play);
        /// <summary>
        /// Handle a stop game event.
        /// </summary>
        /// <param name="user">challenger</param>
        void stopGameHandler(CSUser user);
        /// <summary>
        /// Handle a game event with the received data.
        /// </summary>
        /// <param name="gameStuff">Received game stuff data</param>
        void gameHandler(Object gameStuff);
        /// <summary>
        /// Handle an end game event.
        /// </summary>
        /// <param name="user">Current user</param>
        /// <param name="challenger"></param>
        /// <param name="state"></param>
        /// <param name="play"></param>
        /// <param name="gameStuff"></param>
        void endGameHandler(CSUser user, CSUser challenger, GameState state, bool play, Object gameStuff);
        /// <summary>
        /// Handle a received message from the given user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        void receiveMsgHandler(CSUser user, string msg);
        /// <summary>
        /// Handle a user list event.
        /// </summary>
        /// <param name="users">New list of connected users</param>
        void userListHandler(List<CSUser> users);
        /// <summary>
        /// Handle a clear event.
        /// </summary>
        void clearHandler();
        /// <summary>
        /// Print the given message on the board.
        /// </summary>
        /// <param name="msg"></param>
        void printCSMessage(KeyValuePair<Severiry, string> msg);
    }
}
