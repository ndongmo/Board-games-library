using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TGL.Server.View;
using TGL.Model;

namespace GSM.view
{
    public partial class ServerForm : Form, IServerView
    {
        public event newStartServerHandler startServer;
        public event newStopServerHandler stopServer;
        private object objectLock = new Object();

        public ServerForm()
        {
            InitializeComponent();

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridViewUsers.ColumnCount = 3;
            dataGridViewUsers.ColumnHeadersVisible = true;
            dataGridViewUsers.RowHeadersVisible = false;
            dataGridViewUsers.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridViewUsers.Columns[0].Name = "Login";
            dataGridViewUsers.Columns[1].Name = "Points";
            dataGridViewUsers.Columns[2].Name = "Number of matches";

            dataGridViewParties.ColumnCount = 3;
            dataGridViewParties.ColumnHeadersVisible = true;
            dataGridViewParties.RowHeadersVisible = false;
            dataGridViewParties.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridViewParties.Columns[0].Name = "Player 1";
            dataGridViewParties.Columns[1].Name = "VS";
            dataGridViewParties.Columns[2].Name = "Player 2";
        }

        public void showView()
        {
            this.ShowDialog();
        }

        public void updateState(bool state)
        {
            this.Invoke((MethodInvoker) delegate
            {
                buttonStart.Enabled = !state;
                buttonStop.Enabled = state;
                toolStripStatusLabel1.Text = (state ? "Server started" : "Server stopped");
            });
        }

        public void printMessage(KeyValuePair<Severiry, string> msg)
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

                richTextBox1.SelectionColor = color;
                richTextBox1.AppendText(DateTime.Now.ToString()+ " - " + msg.Value + "\n");
                richTextBox1.SelectionColor = richTextBox1.ForeColor;
            });
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int port = (int) numericUpDown1.Value;
            if (startServer != null) startServer(port);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (stopServer != null) stopServer();
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(stopServer != null) stopServer();
        }

        public bool isOpened()
        {
            return this.Visible;
        }

        public void clearAll()
        {
            this.Invoke((MethodInvoker)delegate
            {
                dataGridViewUsers.Rows.Clear();
                dataGridViewParties.Rows.Clear();
            });
        }

        public void addUser(CSUser user)
        {
            this.Invoke((MethodInvoker)delegate
            {
                dataGridViewUsers.Rows.Add(new string[] { 
                    user.Login, user.Points.ToString(), user.NbParties.ToString()
                });
            });
        }

        public void update(string login, CSUser user)
        {
            this.Invoke((MethodInvoker)delegate
            {
                foreach (DataGridViewRow row in  dataGridViewUsers.Rows)
                {
                    if (row.Cells[0].Value.Equals(login))
                    {
                        row.SetValues(new string[] { 
                            user.Login, user.Points.ToString(), user.NbParties.ToString()
                        });
                    }
                }
            });
        }

        public void delete(CSUser user)
        {
            this.Invoke((MethodInvoker)delegate
            {
                foreach (DataGridViewRow row in dataGridViewUsers.Rows)
                {
                    if (row.Cells[0].Value.Equals(user.Login))
                    {
                        dataGridViewUsers.Rows.Remove(row);
                        break;
                    }
                }
            });
        }

        public void newParty(CSUser user1, CSUser user2)
        {
            this.Invoke((MethodInvoker)delegate
            {
                dataGridViewParties.Rows.Add(new string[] { 
                    user1.Login, " ", user2.Login
                });
            });
        }

        public void removeParty(CSUser user1, CSUser user2)
        {
            this.Invoke((MethodInvoker)delegate
            {
                foreach (DataGridViewRow row in dataGridViewParties.Rows)
                {
                    if (row.Cells[0].Value.Equals(user1.Login) || row.Cells[0].Value.Equals(user2.Login))
                    {
                        dataGridViewParties.Rows.Remove(row);
                        break;
                    }
                }
            });
        }

        public event newStartServerHandler startServerHandler
        {
            add
            {
                lock (objectLock)
                {
                    startServer += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    startServer -= value;
                }
            }
        }

        public event newStopServerHandler stopServerHandler
        {
            add
            {
                lock (objectLock)
                {
                    stopServer += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    stopServer -= value;
                }
            }
        }
    }
}
