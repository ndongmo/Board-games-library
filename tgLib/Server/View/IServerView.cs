using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGL.Model;

namespace TGL.Server.View
{
    public delegate void newStartServerHandler(int port);
    public delegate void newStopServerHandler();

    public interface IServerView
    {
        event newStartServerHandler startServerHandler;
        event newStopServerHandler stopServerHandler;

        /// <summary>
        /// Show the server's view.
        /// </summary>
        void showView();
        /// <summary>
        ///  Return true if the server's view is visible.
        /// </summary>
        /// <returns></returns>
        bool isOpened();
        /// <summary>
        /// Clear the sever message interface.
        /// </summary>
        void clearAll();
        /// <summary>
        /// Add the given user.
        /// </summary>
        /// <param name="user"></param>
        void addUser(CSUser user);
        /// <summary>
        /// Connect and update the given user.
        /// </summary>
        /// <param name="login">Login connection</param>
        /// <param name="user"></param>
        void update(string login, CSUser user);
        /// <summary>
        /// Delete the given user.
        /// </summary>
        /// <param name="user"></param>
        void delete(CSUser user);
        /// <summary>
        /// Start a new party between the given users.
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        void newParty(CSUser user1, CSUser user2);
        /// <summary>
        /// Remove from server the party involving the given users.
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        void removeParty(CSUser user1, CSUser user2);
        /// <summary>
        /// Update the server's state.
        /// </summary>
        /// <param name="state"></param>
        void updateState(bool state);
        /// <summary>
        /// Print the given message on the server board.
        /// </summary>
        /// <param name="msg"></param>
        void printMessage(KeyValuePair<Severiry, string> msg);
    }
}
