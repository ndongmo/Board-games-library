using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using TGL.Client.View;
using TGL.Client.Model;
using TGL.Model;

namespace GCM.view
{
    public partial class ClientForm : Form, IClientView, ILogger
    {
        private CSUser me;
        private List<CSUser> users;
        private EndParameters endParams;
        protected bool withEffect;
        protected int effectTime;
        private int currentEffectTime;

        private enum State { DECONNECTED, CONNECTED, LOGED, PLAYING, WAITING, PLAYING_COMPUTER };
        private State myState, lastState;
        private object objectLock = new Object();

        protected GroupBox groupBoxGame;

        public event newConnectionEvent connection;
        public event newDeconnectionEvent deconnection;
        public event newLoginEvent login;
        public event newAbortEvent abort;
        public event newRequestEvent request;
        public event newResponseEvent response;
        public event newQuitGameEvent quitGame;
        public event newEndGameEvent endGame;
        public event newSendMsgEvent sendMsg;
        public event newGameEvent inGame;

        public ClientForm()
        {
            InitializeComponent();

            groupBoxGame = new GroupBox();
            groupBoxGame.Location = new Point(7, 13);
            groupBoxGame.Size = new System.Drawing.Size(403, 331);
            groupBoxGame.FlatStyle = FlatStyle.Standard;
            groupBoxGameBoard.Controls.Add(groupBoxGame);

            deconnectionState();

            users = new List<CSUser>();
            withEffect = false;
            effectTime = 1000;
            currentEffectTime = 0;
        }

        public bool existRequest(string login)
        {
            if (labelSentTo.Text.Equals(login))
                return true;
            foreach (DataGridViewRow row in dataGridViewRequests.Rows)
                if (row.Cells[0].Value.Equals(login))
                    return true;

            return false;
        }

        public void deconnectionHandler()
        {
            this.Invoke((MethodInvoker)delegate
            {
                deconnectionState();
            });
        }

        private void deconnectionState()
        {
            timer1.Stop();
            currentEffectTime = 0;
            
            if (myState != State.PLAYING_COMPUTER)
            {
                reset();
                groupBoxGameBoard.Enabled = false;
                buttonQuitGame.Visible = false;
                buttonComputer.Enabled = true;
                dataGridViewGame.Rows.Clear();
            }
            groupBoxListofPlayers.Visible = true;
            groupBoxReceivedR.Visible = true;
            groupBoxTchat.Visible = false;
            buttonConnect.Enabled = true;
            buttonComputer.Enabled = true;
            buttonLogin.Enabled = false;
            buttonRegister.Enabled = false;
            buttonDisconnect.Enabled = false;
            buttonAbortSent.Enabled = false;

            dataGridViewList.Rows.Clear();
            dataGridViewRequests.Rows.Clear();
            labelSentTo.Text = "...";
            labelLog.Text = "";

            me = new CSUser(ClientThread.DEFAULTNAME);
            myState = State.DECONNECTED;
            toolStripStatusLabel1.Text = me + " : " + myState.ToString();
        }

        public void connectionHandler(CSUser user)
        {
            this.Invoke((MethodInvoker)delegate
            {
                me = user;
                connectionState();
            });
        }

        private void connectionState()
        {
            groupBoxListofPlayers.Enabled = true;
            groupBoxReceivedR.Enabled = true;
            buttonLogin.Enabled = true;
            buttonRegister.Enabled = true;
            buttonDisconnect.Enabled = true;
            buttonConnect.Enabled = false;
            labelLog.Text = "";
            myState = State.CONNECTED;
            toolStripStatusLabel1.Text = me + " : " + myState.ToString();
        }

        public void loginHandler(CSUser user)
        {
            this.Invoke((MethodInvoker)delegate
            {
                me = user;
                logedState();
            });
        }

        private void logedState()
        {
            if (dataGridViewGame.Rows.Count > 0)
                dataGridViewGame.Rows[0].Cells[0].Value = me.Login;

            groupBoxListofPlayers.Enabled = true;
            groupBoxReceivedR.Enabled = true;
            buttonLogin.Enabled = false;
            buttonRegister.Enabled = false;
            buttonDisconnect.Enabled = true;
            labelLog.Text = "";
            myState = State.LOGED;
            toolStripStatusLabel1.Text = me + " : " + myState.ToString();
        }

        private void waitingState()
        {
            groupBoxListofPlayers.Enabled = false;
            groupBoxReceivedR.Enabled = false;
            buttonLogin.Enabled = false;
            buttonRegister.Enabled = false;
            buttonDisconnect.Enabled = false;
            buttonAbortSent.Enabled = false;
            myState = State.WAITING;
            toolStripStatusLabel1.Text = me + " : " + myState.ToString();
        }

        public void userListHandler(List<CSUser> users)
        {
            this.Invoke((MethodInvoker)delegate
            {
                dataGridViewList.Rows.Clear();

                foreach (CSUser user in users)
                    dataGridViewList.Rows.Add(new String[] 
                    { user.Login, user.Points.ToString(), user.NbParties.ToString() });
            });
        }

        public void clearHandler()
        {
            this.Invoke((MethodInvoker)delegate
            {
                labelSentTo.Text = "...";
                dataGridViewRequests.Rows.Clear();
            });
        }

        public void requestHandler(CSUser user)
        {
            this.Invoke((MethodInvoker)delegate
            {
                dataGridViewRequests.Rows.Add(new string[] { user.Login, "Accept", "Abort" });
            });
        }

        public void abortHandler(CSUser user, bool sent)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (sent)
                {
                    labelSentTo.Text = "...";
                    buttonAbortSent.Enabled = false;
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridViewRequests.Rows)
                    {
                        if (row.Cells[0].Value.Equals(user.Login))
                        {
                            dataGridViewRequests.Rows.Remove(row);
                            break;
                        }
                    }
                }
            });
        }

        public void startGameHandler(CSUser user, bool beginer)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lastState = myState;
                myState = State.PLAYING;

                dataGridViewRequests.Rows.Clear();
                dataGridViewList.Rows.Clear();
                groupBoxGameBoard.Enabled = true;
                groupBoxListofPlayers.Visible = false;
                groupBoxReceivedR.Visible = false;
                groupBoxTchat.Visible = true;
                labelSentTo.Text = "...";
                labelLog.Text = "";
                buttonAbortSent.Enabled = false;
                buttonComputer.Enabled = false;
                buttonLogin.Enabled = false;
                buttonRegister.Enabled = false;
                buttonQuitGame.Visible = true;
                toolStripStatusLabel1.Text = me + " : " + myState.ToString() + " (vs " + user.Login + ")";

                dataGridViewGame.Rows.Add(new string[] { me.Login, me.Points.ToString(), me.NbParties.ToString() });
                dataGridViewGame.Rows.Add(new string[] { user.Login, user.Points.ToString(), user.NbParties.ToString() });

                playState(beginer);
            });
        }

        public void gameHandler(Object gameStuff)
        {
            this.Invoke((MethodInvoker)delegate
            {
                setLastMove(gameStuff, 1);
                drawGame(1);
            });
        }

        private void updateGame()
        {
            if (endParams != null)
            {
                endGameState(endParams.current, endParams.challenger, endParams.state, endParams.play);
                endParams = null;
            }
        }

        private void drawGame(int player)
        {
            playState(player == 1);

            if (withEffect)
            {
                computeEffectTime();
                buttonQuitGame.Enabled = false;
                timer1.Start();
            }
            else
            {
                updateGame();
                draw();
            }
        }

        protected void moveGame(Object gameMove, int player)
        {
            setLastMove(gameMove, player);
            GameState output = updateMove();
            if (output != GameState.CONTINUE)
                endGame(gameMove, output);
            else
            {
                inGame(gameMove);
                drawGame(0);
            }
        }

        public void stopGameHandler(CSUser user)
        {
            this.Invoke((MethodInvoker)delegate
            {
                stopGameState();
                log(Severiry.INFO, "Player '" + user.Login + "' stopped the game !");
            });
        }

        protected virtual void computeEffectTime() { }

        private void stopGameState()
        {
            groupBoxGameBoard.Enabled = false;
            groupBoxListofPlayers.Visible = true;
            groupBoxReceivedR.Visible = true;
            groupBoxTchat.Visible = false;
            buttonQuitGame.Visible = false;
            dataGridViewGame.Rows.Clear();
            buttonComputer.Enabled = true;
            if (lastState != State.LOGED)
            {
                buttonLogin.Enabled = true;
                buttonRegister.Enabled = true;
            }
            reset();
            myState = lastState;
            toolStripStatusLabel1.Text = me + " : " + myState.ToString();
        }

        public void endGameHandler(CSUser user, CSUser challenger, GameState state, bool play, Object gameStuff)
        {
            this.Invoke((MethodInvoker)delegate
            {
               endParams = new EndParameters(user, challenger, state, play);
               setLastMove(gameStuff, 1);
               drawGame(1);
            });
        }

        public void endGameState(CSUser user, CSUser challenger, GameState state, bool play)
        {
            me = user;
            if (dataGridViewGame.Rows.Count > 1)
            {
                dataGridViewGame.Rows[0].Cells[1].Value = me.Points.ToString();
                dataGridViewGame.Rows[0].Cells[2].Value = me.NbParties.ToString();
                dataGridViewGame.Rows[1].Cells[1].Value = challenger.Points.ToString();
                dataGridViewGame.Rows[1].Cells[2].Value = challenger.NbParties.ToString();
                toolStripStatusLabel1.Text = me + " : " + myState.ToString() + " (vs " + challenger.Login + ")";
                reset();
                playState(play);
            }
            if (state == GameState.DRAW)
            {
                log(Severiry.INFO, "Draw game !");
                receiveMsgState(new CSUser("Game master"), "Draw game, well played both of you !");
            }
            else if (state == GameState.WON)
            {
                log(Severiry.INFO, "Congratulation, You won !");
                receiveMsgState(new CSUser("Game master"), "Congratulation '" + me.Login + "', you are the winner !");
            }
            else
            {
                log(Severiry.INFO, "Bad played, You lost !");
                receiveMsgState(new CSUser("Game master"), "Congratulation '" + challenger.Login + "', you are the winner !");
            }
        }

        private void playState(bool play)
        {
            if (play)
            {
                dataGridViewGame.Rows[0].Selected = true;
                groupBoxGame.Enabled = true;
            }
            else
            {
                dataGridViewGame.Rows[1].Selected = true;
                groupBoxGame.Enabled = false;
            }
        }

        public void receiveMsgHandler(CSUser user, string msg)
        {
            this.Invoke((MethodInvoker)delegate
            {
                receiveMsgState(user, msg);
            });
        }

        private void receiveMsgState(CSUser user, string msg)
        {
            Color color = user.Equals(me) ? Color.Blue : user.Equals(new CSUser("Game master")) ? Color.DarkRed : Color.Green;

            richTextBoxMsg.SelectionColor = color;
            richTextBoxMsg.AppendText(user.Login + " [" + DateTime.Now.ToString("HH:mm") + "] - " + msg + "\n");
            richTextBoxMsg.SelectionColor = richTextBoxMsg.ForeColor;
        }

        public void showView(CSUser user)
        {
            if (user != null)
            {
                textBoxLogin.Text = user.Login;
                textBoxPassword.Text = user.Pass;
                buttonRegister.Visible = false;
            }
            this.ShowDialog();
        }

        public void showRegisterOption()
        {
            this.Invoke((MethodInvoker)delegate
            {
                buttonRegister.Visible = true;
            });
        }

        public bool isOpened()
        {
            return this.Visible;
        }

        public void printCSMessage(KeyValuePair<Severiry, string> msg)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Color color;
                if (Severiry.ERROR == msg.Key)
                    color = Color.Red;
                else if (Severiry.WARNING == msg.Key)
                    color = Color.Blue;
                else
                    color = Color.Green;

                labelLog.ForeColor = color;
                labelLog.Text = DateTime.Now.ToString("HH:mm") + " - " + msg.Value;
            });
        }

        public void log(Severiry severity, string msg)
        {
            printCSMessage(new KeyValuePair<Severiry, string>(severity, msg));
        }

        public event newConnectionEvent connectionEvent
        {
            add { lock (objectLock) { connection += value; } }
            remove { lock (objectLock) { connection -= value; } }
        }

        public event newDeconnectionEvent deconnectionEvent
        {
            add { lock (objectLock) { deconnection += value; } }
            remove { lock (objectLock) { deconnection -= value; } }
        }

        public event newLoginEvent loginEvent
        {
            add { lock (objectLock) { login += value; } }
            remove { lock (objectLock) { login -= value; } }
        }

        public event newAbortEvent abortEvent
        {
            add { lock (objectLock) { abort += value; } }
            remove { lock (objectLock) { abort -= value; } }
        }

        public event newRequestEvent requestEvent
        {
            add { lock (objectLock) { request += value; } }
            remove { lock (objectLock) { request -= value; } }
        }

        public event newResponseEvent responseEvent
        {
            add { lock (objectLock) { response += value; } }
            remove { lock (objectLock) { response -= value; } }
        }

        public event newQuitGameEvent quitGameEvent
        {
            add { lock (objectLock) { quitGame += value; } }
            remove { lock (objectLock) { quitGame -= value; } }
        }

        public event newEndGameEvent endGameEvent
        {
            add { lock (objectLock) { endGame += value; } }
            remove { lock (objectLock) { endGame -= value; } }
        }

        public event newSendMsgEvent sendMsgEvent
        {
            add { lock (objectLock) { sendMsg += value; } }
            remove { lock (objectLock) { sendMsg -= value; } }
        }

        public event newGameEvent gameEvent
        {
            add { lock (objectLock) { inGame += value; } }
            remove { lock (objectLock) { inGame -= value; } }
        }

        public void register(bool enroll)
        {
            if (textBoxLogin.Text.Length > 2 && !textBoxLogin.Text.Contains(' ') && 
                !textBoxLogin.Text.StartsWith(ClientThread.DEFAULTNAME)
                && textBoxLogin.Text.Length < 20)
            {
                if (textBoxPassword.Text.Length > 2 && textBoxPassword.Text.Length < 20)
                {
                    CSUser user = new CSUser(textBoxLogin.Text, textBoxPassword.Text);
                    login(user, enroll, checkBox1.Checked && buttonRegister.Visible);
                    waitingState();
                }
                else
                    log(Severiry.ERROR, "Password format : 20 > length > 2");
            }
            else
                log(Severiry.ERROR, "Login format : 20 > length > 2; not contain space or 'ANONYMOUS'");
        }

        protected virtual void draw(){}
        protected virtual void reset() { }
        protected virtual void setLastMove(Object move, int player) { }
        protected virtual GameState updateMove() { return GameState.CONTINUE; }

        protected void setTimerInterval(int interval)
        {
            timer1.Interval = interval;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            string pattern = @"^((([1-2]\d{2})|([1-9][0-9])|([0-9]))\.){3}(([1-2]\d{2})|([1-9][0-9])|([0-9]))$";
            Match addressMatch = Regex.Match(textBoxAddress.Text, pattern);

            if (addressMatch.Success)
            {
                try
                {
                    TcpClient client = new TcpClient(textBoxAddress.Text, (int)numericUpDown1.Value);
                    connection(client);
                    waitingState();
                }
                catch (Exception ex)
                {
                    log(Severiry.ERROR, "[Connection] - " + ex.Message);
                }
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            deconnection();
            deconnectionState();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            register(false);
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            register(true);
        }

        private void buttonQuitGame_Click(object sender, EventArgs e)
        {
            stopGameState();
            quitGame();
        }

        private void buttonComputer_Click(object sender, EventArgs e)
        {
            printCSMessage(new KeyValuePair<Severiry, string>(Severiry.INFO, "Not implemented yet!"));
        }

        private void buttonAbortSent_Click(object sender, EventArgs e)
        {
            CSUser user = new CSUser(labelSentTo.Text);
            labelSentTo.Text = "...";
            buttonAbortSent.Enabled = false;
            abort(user, true);
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myState != State.DECONNECTED)
                deconnection();
        }

        private void dataGridViewRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string login = dataGridViewRequests.Rows[e.RowIndex].Cells[0].Value.ToString();
            CSUser user = new CSUser(login);
            if (e.ColumnIndex == 1)
                response(user);
            else if (e.ColumnIndex == 2)
                abort(user, false);
            dataGridViewRequests.Rows.RemoveAt(e.RowIndex);
        }

        private void dataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string login = dataGridViewList.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (!existRequest(login))
            {
                CSUser user = new CSUser(login);
                labelSentTo.Text = login;
                buttonAbortSent.Enabled = true;
                request(user);
            }
        }

        private void buttonMsgSend_Click(object sender, EventArgs e)
        {
            receiveMsgState(me, textBoxMsg.Text);
            sendMsg(textBoxMsg.Text);
            textBoxMsg.Text = "";
        }

        private void buttonMsgReset_Click(object sender, EventArgs e)
        {
            richTextBoxMsg.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            draw();
            currentEffectTime += timer1.Interval;
            if (currentEffectTime >= effectTime)
            {
                currentEffectTime = 0;
                buttonQuitGame.Enabled = true;
                timer1.Stop();
                updateGame();
            }
        }

        private class EndParameters
        {
            public CSUser current, challenger;
            public bool play;
            public GameState state;

            public EndParameters(CSUser current, CSUser challenger, GameState state, bool play)
            {
                this.challenger = challenger;
                this.current = current;
                this.state = state;
                this.play = play;
            }
        }
    }
}
