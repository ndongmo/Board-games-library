using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TGL.Model
{
    /// <summary>
    /// User manager class model.
    /// </summary>
    public abstract class DataManager
    {
        /// <summary>
        /// Create user method, it throws an exception when user isn't correct.
        /// </summary>
        /// <param name="user"></param>
        public abstract void create(CSUser user);
        /// <summary>
        /// Update user method.
        /// </summary>
        /// <param name="user"></param>
        public abstract void update(CSUser user);
        /// <summary>
        /// Find user method, it use login and password for finding.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public abstract CSUser find(CSUser user);
        /// <summary>
        /// Check if this login exists in the user table.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public abstract bool exist(string login);

        /// <summary>
        /// Close manager.
        /// </summary>
        public abstract void close();

        /// <summary>
        /// Encrypt password with SHA1 method.
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected string sha1Encrypt(string pass)
        {
            using (var sha1 = SHA1.Create())
            {
                try
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(pass);
                    byte[] hashBytes = sha1.ComputeHash(bytes);

                    return Encoding.UTF8.GetString(hashBytes);
                }
                catch (Exception)
                {
                    throw new Exception("Error when encryting password");
                }
            }
        }
    }
}
