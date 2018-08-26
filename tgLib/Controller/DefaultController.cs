using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGL.Model;

namespace TGL.Controller
{
    /// <summary>
    /// Default controller which defines methods 
    /// for managing messages between clients and server.
    /// </summary>
    public abstract class DefaultController : ILogger
    {
        protected abstract void endGameHandler(UserThread client, CSMessage msg);
        protected abstract void stopGameHandler(UserThread client, CSMessage msg);
        protected abstract void loginHandler(UserThread client, CSMessage msg);
        protected abstract void challengerHandler(UserThread client, CSMessage msg);
        protected abstract void deconnectionHandler(UserThread client);
        protected abstract void handleMessage(KeyValuePair<Severiry, string> msg);

        public void log(Severiry severity, string msg)
        {
            handleMessage(new KeyValuePair<Severiry, string>(severity, msg));
        }
    }
}
