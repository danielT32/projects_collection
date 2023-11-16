using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{
    public class GMessageDB : BaseDB
    {
        /// <summary>
        /// deletes a group message
        /// </summary>
        /// <param name="entity">group message to delete</param>
        public override void Delete(BaseEntity entity)
        {
            GMessage message = entity as GMessage;
            if (message != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        /// <summary>
        /// adds a group message, also marks the message as unseen for each member except 
        /// the author
        /// </summary>
        /// <param name="entity">the GMessage to add to DataBase/param>
        public override void Insert(BaseEntity entity)
        {
            GMessage message = entity as GMessage;
            UnSeenGMDB unSeenGMDB = new UnSeenGMDB();
            if (message != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
                SaveChanges();
                unSeenGMDB.Insert(new UnSeenGM(message, 0, message.Group.ID));
                unSeenGMDB.SaveChanges();
            }
        }

        /// <summary>
        /// updates(changes) a group message
        /// </summary>
        /// <param name="entity">the updated group message</param>
        public override void Update(BaseEntity entity)
        {
            GMessage message = entity as GMessage;
            if (message != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }
        /// <summary>
        /// makes sql to delete a group message
        /// </summary>
        /// <param name="entity">the GMessage to delete</param>
        /// <returns>sql that deletes the message</returns>
        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            GMessage message = entity as GMessage;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM GMessageTbl WHERE ID= {0}", message.ID);
            return sql_builder.ToString();
        }
        /// <summary>
        /// makes sql to inserts a group message
        /// </summary>
        /// <param name="entity">the GMessage to insert</param>
        /// <returns>sql that inserts the message</returns>
        protected override string CreateInsertSQL(BaseEntity entity)
        {
            GMessage message = entity as GMessage;
            return string.Format("INSERT INTO GMessageTbl ([User], [Group], [Content]) VALUES( {0}, {1}, '{2}' )",
                message.User.ID, message.Group.ID, ToSqlStr(message.Content));

        }
        /// <summary>
        /// fills the feilds in the group message with the received information
        /// from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>filled entity</returns>
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UserDB userDB = new UserDB();
            GroupDB groupDB = new GroupDB();
            GMessage message = entity as GMessage;
            message.ID = (int)reader["ID"];
            message.User = userDB.SelectByID((int)reader["User"]);
            message.Group = groupDB.SelectGroup((int)reader["Group"]);
            message.Content = (string)reader["Content"];
            return message;
        }
        /// <summary>
        /// makes sql to updates a group message
        /// </summary>
        /// <param name="entity">the GMessage to update</param>
        /// <returns>sql that updates the message</returns>
        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            GMessage message = entity as GMessage;
            return $"UPDATE GMessageTbl SET [Content]='{ToSqlStr(message.Content)}' WHERE [ID]={message.ID}";
        }
        //creates an empty GMessage object
        protected override BaseEntity NewEntity()
        {
            return new GMessage() as BaseEntity;
        }
        /// <summary>
        /// returns the Gmessage with the ID given
        /// </summary>
        /// <param name="ID">id of wanted message</param>
        /// <returns>the group message</returns>
        public GMessage SelectGMessageByID(int ID)
        {
            command.CommandText = "SELECT * FROM GMessageTbl" +
                $" WHERE [ID] = {ID}";
            GMessageList list = new GMessageList(base.Select());
            return list.Count > 0 ? list[0] : null;
        }
        /// <summary>
        /// receives all the messages in a group. also marks the messages as seen
        /// </summary>
        /// <param name="user">the user in the group</param>
        /// <param name="group">the group</param>
        /// <returns>GMessageList of the messages</returns>
        public GMessageList GetMessages(User user, Group group)
        {
            UnSeenGMDB unSeenDB = new UnSeenGMDB();
            unSeenDB.Delete(new UnSeenGM(null, user.ID, group.ID));
            unSeenDB.SaveChanges();
            command.CommandText = "SELECT * FROM GMessageTbl" +
                $" WHERE [Group] = {group.ID}";
            return new GMessageList(base.Select());

        }
    }
}
