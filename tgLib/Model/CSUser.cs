using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGL.Model
{
    /// <summary>
    /// User model implementation.
    /// </summary>
    [Serializable]
    public class CSUser
    {
        private string login;
        private string pass;
        private int points;
        private int nbParties;
        [NonSerialized]
        private int index;

        public CSUser(string login)
        {
            this.Login = login;
            this.Points = 0;
            this.NbParties = 0;
        }

        public CSUser(string login, string pass)
        {
            this.Login = login;
            this.pass = pass;
            this.Points = 0;
            this.NbParties = 0;
        }

        public CSUser() { }

        public void win()
        {
            points += 3;
        }

        public void draw()
        {
            points += 1;
        }

        public void incrementParties()
        {
            nbParties++;
        }

        public string Login { get { return login; } set { login = value; } }
        public string Pass { get { return pass; } set { pass = value; } }
        public int Points { get { return points; } set { points = value; } }
        public int NbParties { get { return nbParties; } set { nbParties = value; } }
        public int Index { get { return index; } set { index = value; } }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return login.Equals(((CSUser)obj).Login);
        }

        public override int GetHashCode()
        {
            return login.GetHashCode();
        }

        public override string ToString()
        {
            return login + " [ Points = " + points + ", Nb matches = " + nbParties + " ]";
        }
    }
}
