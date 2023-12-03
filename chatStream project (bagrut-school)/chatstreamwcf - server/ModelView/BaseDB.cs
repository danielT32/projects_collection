using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Model;
using System.Text.RegularExpressions;
namespace ModelView
{
    public delegate string CreateSql(BaseEntity entity);
    public class ChangeEntity
    {
        private BaseEntity entity;
        private CreateSql createSql;

        public ChangeEntity(CreateSql createSql, BaseEntity entity)
        {
            this.entity = entity;
            this.createSql = createSql;
        }

        public BaseEntity Entity { get => entity; set => entity = value; }
        public CreateSql CreateSql { get => createSql; set => createSql = value; }
    }
    public abstract class BaseDB
    {
        //Properties of BaseDB; 
        protected string connectionString; 
        protected OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;
        protected List<ChangeEntity> inserted = new List<ChangeEntity>();
        protected List<ChangeEntity> deleted = new List<ChangeEntity>();
        protected List<ChangeEntity> updated = new List<ChangeEntity>();
        
        //BaseDB Class abstract Methods:
        protected abstract BaseEntity NewEntity();
        protected abstract BaseEntity CreateModel(BaseEntity entity);
        public abstract void Insert(BaseEntity entity);
        public abstract void Update(BaseEntity entity);
        public abstract void Delete(BaseEntity entity);
        
        protected abstract string CreateInsertSQL(BaseEntity entity);
        protected abstract string CreateUpdateSQL(BaseEntity entity);
        protected abstract string CreateDeleteSQL(BaseEntity entity);

        public BaseDB()
        {
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\magshimim\Desktop\ChatStreamProject\chatstreamwcf\ModelView\SocialNet.accdb;Persist Security Info=True";
            connection = new OleDbConnection(connectionString);
            command = new OleDbCommand();
        }
        /// <summary>
        /// Removes all non-alphanumeric characters from the input string.
        /// </summary>
        /// <param name="input">The string to remove non-alphanumeric characters from.</param>
        /// <returns>A new string that contains only alphanumeric characters.</returns>
        public static string RemoveNonAlphanumeric(string input)
        {
            // Define a regular expression that matches all non-alphanumeric characters
            Regex regex = new Regex("[^a-zA-Z0-9]");

            // Use the regular expression to remove all non-alphanumeric characters from the input string
            string result = regex.Replace(input, "");

            return result;
        }
        /// <summary>
        /// Sanitizes the input string for safe use in database queries to prevent SQL injection attacks.
        /// </summary>
        /// <param name="original">The string to sanitize.</param>
        /// <returns>A sanitized string that can be safely used in database queries.</returns>

        protected string ToSqlStr(string original)
        {

            StringBuilder builder = new StringBuilder(original);
            builder.Replace("'", "''");
            builder.Replace("\\", "\\\\");

            return builder.ToString(); 
        }
        protected List<BaseEntity> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>(); // create list to store the results of the query
            try
            {
                command.Connection = connection; //adds the database connection to the command
                //command text
                connection.Open(); //opens the connection
                reader = command.ExecuteReader(); //executes the query
                while (reader.Read()) //for each result:
                {
                    BaseEntity entity = NewEntity(); //create the entity to store the information.
                    list.Add(CreateModel(entity)); // store the result in the entity created.
                }
            }
            catch (Exception e)
            {
                //show the error in case of error
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL: " + command.CommandText);
            }
            finally
            {
                //close the reader and the connection in the end.
                if (reader != null)
                    reader.Close();
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return list;
        }
        public int SaveChanges()
        {
            int records = 0;
            try
            {
                command.Connection = this.connection; //add conssnection reference to query
                connection.Open(); //open connection
                foreach (var item in updated)
                {
                    command.CommandText = item.CreateSql(item.Entity); //receive string of the query
                    records = command.ExecuteNonQuery(); // execute update query
                }
                foreach (var item in inserted)
                {
                    command.CommandText = item.CreateSql(item.Entity);//receive string of the query
                    records = command.ExecuteNonQuery(); // execute update query
                    if (item.Entity is User)
                    {
                        User user = (User)item.Entity;
                        command.CommandText = "Select @@Identity"; //if the entity is user, get it's identity property
                        user.ID = (int)command.ExecuteScalar(); // receive and add property
                    }
                    if (item.Entity is GMessage)
                    {
                        GMessage message = (GMessage)item.Entity;
                        command.CommandText = "Select @@Identity"; //if the entity is user, get it's identity property
                        message.ID = (int)command.ExecuteScalar(); // receive and add property
                    }
                    if(item.Entity is Model.Group)
                    {
                        Model.Group group = (Model.Group)item.Entity;
                        command.CommandText = "Select @@Identity"; //if the entity is user, get it's identity property
                        group.ID = (int)command.ExecuteScalar(); // receive and add property
                    }
                }
                foreach (var item in deleted)
                {
                    command.CommandText = item.CreateSql(item.Entity); //receive string of the query
                    records = command.ExecuteNonQuery(); // execute update query
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText); //show error if occurred
            }
            finally
            {
                //clear queries lists:
                inserted.Clear();
                updated.Clear();
                deleted.Clear();
                //close connection:
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return records;

        }
    }
}
