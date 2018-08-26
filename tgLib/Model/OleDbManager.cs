using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGL.Model
{
    /// <summary>
    /// Manage read/write user's data in a database through OleDb connection.
    /// </summary>
    public class OleDbManager : DataManager
    {
        private OleDbConnection con;

        public OleDbManager(string conString)
        {
            try
            {
                con = new OleDbConnection(conString);
                con.Open();
            }catch(Exception e){
                throw new Exception("Error occurred when connecting to the database : " + e.Message);
            }
        }

        public override void close()
        {
            con.Close();
        }

        public override void create(CSUser user)
        {
            if (user == null) throw new Exception("CSUser cannot be null !");
            try
            {
                OleDbCommand cmdInsert = new OleDbCommand();
                cmdInsert.Connection = con;
                cmdInsert.CommandText = "INSERT INTO [user] (login, pass, points, nbparties) VALUES(@login, @pass, @points, @nbparties)";
                cmdInsert.Parameters.Add("@login", OleDbType.VarChar, 20).Value = user.Login;
                cmdInsert.Parameters.Add("@pass", OleDbType.VarChar, 20).Value = sha1Encrypt(user.Pass);
                cmdInsert.Parameters.Add("@points", OleDbType.Integer,  11).Value = user.Points;
                cmdInsert.Parameters.Add("@nbparties", OleDbType.Integer,  11).Value = user.NbParties;
                cmdInsert.Prepare();
                cmdInsert.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Error when inserting user '"+user.Login+"' !");
            }
        }

        public override void update(CSUser user)
        {
            if (user == null) throw new Exception("CSUser cannot be null !");
            try
            {
                OleDbCommand cmdUpdate = new OleDbCommand();
                cmdUpdate.Connection = con;
                cmdUpdate.CommandText = "UPDATE [user] SET points = @points, nbparties = @nbparties WHERE login = @login";
                cmdUpdate.Parameters.Add("@points", OleDbType.Integer, 11).Value = user.Points;
                cmdUpdate.Parameters.Add("@nbparties", OleDbType.Integer, 11).Value = user.NbParties;
                cmdUpdate.Parameters.Add("@login", OleDbType.VarChar, 20).Value = user.Login;
                cmdUpdate.Prepare();
                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Error when updating user '"+user.Login+"' !");
            }
        }

        public override CSUser find(CSUser user)
        {
            if (user == null) throw new Exception("CSUser cannot be null !");
            try
            {
                OleDbCommand cmdFind = new OleDbCommand();
                cmdFind.Connection = con;
                cmdFind.CommandText = "SELECT login, points, nbparties FROM [user] WHERE login = @login AND pass = @pass";
                cmdFind.Parameters.Add("@login", OleDbType.VarChar, 25).Value =  user.Login;
                cmdFind.Parameters.Add("@pass", OleDbType.VarChar, 25).Value = sha1Encrypt(user.Pass);
                cmdFind.Prepare();

                OleDbDataReader reader = cmdFind.ExecuteReader();
                CSUser logUser = null;

                if (reader.Read())
                {
                    logUser = new CSUser();
                    logUser.Login = reader.GetString(0);
                    logUser.Points = reader.GetInt32(1);
                    logUser.NbParties = reader.GetInt32(2);
                }
                reader.Close();
                return logUser;
            }
            catch (Exception e)
            {
                throw new Exception("Error when find user '" + user.Login + "' : " + e.Message);
            }
        }

        public override bool exist(string login)
        {
            if (String.IsNullOrWhiteSpace(login)) return false;
            try
            {
                OleDbCommand cmdExist = new OleDbCommand();
                cmdExist.Connection = con;
                cmdExist.CommandText = "SELECT login FROM [user] WHERE login = @login";
                cmdExist.Parameters.Add("@login", OleDbType.VarChar, 20).Value = login;
                cmdExist.Prepare();

                OleDbDataReader reader = cmdExist.ExecuteReader();
                bool exists = reader.Read();
                reader.Close();
                return exists;
            }
            catch (Exception)
            {
                throw new Exception("Error when check existing user !");
            }
        }

        public override string ToString()
        {
            return "OleDB";
        }
    }
}
