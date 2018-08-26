using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGL.Model
{
    /// <summary>
    /// Message type enumeration used during the party.
    /// </summary>
    public enum MessageType
    {
        ERROR,
        WARNING,
        INFO,
        CONNECTION,
        DECONNECTION,
        LOGIN,
        CHALLENGING,
        STATE,
        USER,
        REGISTRATION,
        REQUEST,
        RESPONSE,
        ABORT,
        START_GAME,
        END_GAME,
        STOP_GAME,
        PLAY_GAME,
        USER_LIST,
        WIN_GAME,
        DRAW_GAME,
        LOSE_GAME,
        CLEAR,
        SEND_MSG,
        MESSAGE,
        IN_GAME,
        GAME_STUFF
    };
}
