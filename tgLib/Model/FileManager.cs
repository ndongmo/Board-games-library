using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGL.Model
{
    /// <summary>
    /// Manage read/write user's data in a file.
    /// </summary>
    public class FileManager : DataManager
    {
        private string PATH = System.Environment.CurrentDirectory + "\\config.txt";
        private const char SEPARATOR = ' ';
        private bool encryptPassword;

        public FileManager(bool encryptPassword)
        {
            this.encryptPassword = encryptPassword;
        }
        /// <summary>
        /// Get the unique user stored in the config file 
        /// knowing that each file stores a single user.
        /// </summary>
        /// <returns></returns>
        public CSUser getUnique()
        {
            if (!File.Exists(PATH)) return null;

            using (StreamReader sr = File.OpenText(PATH))
            {
                string s = "";
                if ((s = sr.ReadLine()) != null)
                {
                    string[] tab = s.Split(new char[] { SEPARATOR });
                    {
                        int temp;
                        CSUser u = new CSUser(tab[0]);
                        u.Pass = tab[1];
                        u.Points = (Int32.TryParse(tab[2], out temp)) ? temp : 0;
                        u.NbParties = (Int32.TryParse(tab[3], out temp)) ? temp : 0;
                        return u;
                    }
                }
                return null;
            }
        }

        public void createUnique(CSUser user)
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(PATH))
            {
                sw.WriteLine(user.Login + SEPARATOR + user.Pass + SEPARATOR + user.Points + SEPARATOR + user.NbParties);
            }
        }

        public override void create(CSUser user)
        {
            string pass = user.Pass;

            if (encryptPassword)
            {
                pass = sha1Encrypt(pass);
                pass = pass.Replace("\r", @"\r").Replace("\n", @"\n");
            }

            if (!File.Exists(PATH))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(PATH))
                {
                    sw.WriteLine(user.Login + SEPARATOR + pass + SEPARATOR + user.Points + SEPARATOR + user.NbParties);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(PATH))
                {
                    sw.WriteLine(user.Login + SEPARATOR + pass + SEPARATOR + user.Points + SEPARATOR + user.NbParties);
                }
            }
        }

        public override void update(CSUser user)
        {
            if (File.Exists(PATH))
            {
                string[] lines = File.ReadAllLines(PATH);
                using (StreamWriter sw = File.AppendText(PATH))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] tab = lines[i].Split(new char[] { SEPARATOR });
                        if (tab[0].Equals(user.Login))
                            lines[i] = tab[0] + SEPARATOR + tab[1] + SEPARATOR + user.Points + SEPARATOR + user.NbParties;
                        sw.WriteLine(lines[i]);
                    }
                }
            }
        }

        public override CSUser find(CSUser user)
        {
            if (!File.Exists(PATH)) return null;

            string pass = user.Pass;

            if (encryptPassword)
            {
                pass = sha1Encrypt(pass);
                pass = pass.Replace("\r", @"\r").Replace("\n", @"\n");
            }

            using (StreamReader sr = File.OpenText(PATH))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] tab = s.Split(new char[]{SEPARATOR});
                    
                    if (tab[0].Equals(user.Login) && s.Contains(pass))
                    {
                        int temp = 0;
                        CSUser u = new CSUser(user.Login);
                        u.Points = (Int32.TryParse(tab[2], out temp)) ? temp:0;
                        u.NbParties = (Int32.TryParse(tab[3], out temp)) ? temp : 0;
                        return u;
                    }
                }
                return null;
            }
        }

        public override bool exist(string login)
        {
            if (!File.Exists(PATH)) return false;

            using (StreamReader sr = File.OpenText(PATH))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] tab = s.Split(new char[] { SEPARATOR });
                    if (tab[0].Equals(login)) return true;
                }
                return false;
            }
        }

        public override void close()
        {
            
        }
    }
}
