using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{
    public class UserDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new User() as BaseEntity;
        }
      
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User user = entity as User;
            user.ID = (int)reader["ID"];
            user.Fname = (string)reader["Fname"];
            user.Lname = (string)reader["Lname"];
            user.Uname = (string)reader["Uname"];
            user.Description = (string)reader["Description"];
            if (user.Description == null)//if user doesn't have description
                user.Description = "";
            user.EntrDate = (DateTime)reader["EntrDate"];
            user.Password = (string)reader["Password"];
            user.Email = (string)reader["Email"];
            return user;
        }

        
        public override void Insert(BaseEntity entity)
        {
            User people = entity as User;
            if (people != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }
        public override void Update(BaseEntity entity)
        {
            User people = entity as User;
            if (people != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }
        public override void Delete(BaseEntity entity)
        {
            User people = entity as User;
            if (people != null)
            {
                deleted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        protected override string CreateInsertSQL(BaseEntity entity)
        {
            User user = entity as User;
            return string.Format("INSERT INTO UserTbl (Uname, FName, LName, EntrDate, [Password], Description, Email)" +
                " VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                RemoveNonAlphanumeric(user.Uname), RemoveNonAlphanumeric(user.Fname), RemoveNonAlphanumeric(user.Lname),
                user.EntrDate.ToString(), ToSqlStr(user.Password), ToSqlStr(user.Description), ToSqlStr(user.Email));
        }
        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            User user = entity as User;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM UserTbl WHERE [Uname]={0} AND [Password] = {1}", ToSqlStr(user.Uname), ToSqlStr(user.Password));
            return sql_builder.ToString();
        }
        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            User user = entity as User;
            return $"UPDATE UserTbl SET Uname='{RemoveNonAlphanumeric(user.Uname)}', FName='{RemoveNonAlphanumeric(user.Fname)}'," +
                $" LName='{RemoveNonAlphanumeric(user.Lname)}'," +
                $" Description='{ToSqlStr(user.Description)}', [Password]='{ToSqlStr(user.Password)}' WHERE ID={user.ID}";
        }

        public UserList SelectAll()
        {
            command.CommandText = "SELECT * FROM UserTbl";
            return new UserList(base.Select());
        }
        public UserList SelectUserData(string Uname, string Password)
        {
            command.CommandText = string.Format("SELECT * FROM UserTbl WHERE Uname = '{0}' AND [Password] = '{1}' ", ToSqlStr(Uname), ToSqlStr(Password));
            return new UserList(base.Select());
        }
        public UserList SelectByUName(string Uname) //select all usernames starts with the Uname
        {
            command.CommandText = string.Format("SELECT * FROM UserTbl WHERE [Uname] LIKE '{0}%' ", ToSqlStr(Uname));
            UserList userL = new UserList(base.Select());
            foreach (User user in userL)//for each user remove sensitive information.
            {
                user.Password = "";
                user.Email = "";
            }
            return userL;
        }
        public UserList SelectUNameExist(string Uname) //select the User with the exact UserName
        {
            command.CommandText = string.Format("SELECT * FROM UserTbl WHERE [Uname] = '{0}' ", ToSqlStr(Uname));
            UserList userL = new UserList(base.Select());
            foreach (User user in userL)//for each user remove sensitive information.
            {
                user.Password = "";
                user.Email = "";
            }
            return userL;
        }
        public User SelectByID(int ID) //select user by id
        {
            command.CommandText = string.Format("SELECT * FROM UserTbl WHERE ID = {0} ", ID);
            UserList userL = new UserList(base.Select());
            foreach (User user in userL)//for each user remove sensitive information.
            {
                user.Password = "";
                user.Email = "";
            }
            return userL.Count > 0 ? userL[0] : null;
        }
    }
}
