using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{

   
    public class DMessageDB : BaseDB
    {
        public override void Delete(BaseEntity entity)
        {
            DMessage message = entity as DMessage;
            if (message != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        public override void Insert(BaseEntity entity)
        {
            DMessage message = entity as DMessage;
            message.Seen = false;
            if (message != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }
        public override void Update(BaseEntity entity)
        {
            DMessage message = entity as DMessage;
            if (message != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public DMessage SelectMessage(int ID)
        {
            command.CommandText = "SELECT * FROM DMessageTbl" +
                $" WHERE [ID] = {ID}";
            DMessageList list = new DMessageList(base.Select()); //recive results
            return list.Count > 0 ? list[0] : null; // return chosen value by condition if true return list[0] else return null
        }

        private bool IsMessagePermition(User main, int ID)
        {
            command.CommandText = "SELECT * FROM DMessageTbl" +
                $" WHERE [ID] = {ID} AND ([DestUser] = {main.ID} OR [SourceUser] = {main.ID}) ";
            DMessageList list = new DMessageList(base.Select());
            return list.Count > 0;
        }

        
        private bool IsUserInUist(UserList list, User userToAdd)
        { //the method gets UserList and user wanted to add and returns true if the user is already in the list
            // else false
            foreach (User userDM in list)
            {
                if (userToAdd.ID == userDM.ID )
                    return true;
            }
            return false;
        }
        /// <summary>
        /// the method gets the current user and returns all the chats
        /// with that user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User's list</returns>
        public UserList GetAllDMChats(User user)
        {
            DMessageList sourseUsers = null;
            DMessageList destUsers = null;
            UserList users = new UserList();
            command.CommandText = "SELECT * FROM DMessageTbl" + 
                $" WHERE [DestUser] = {user.ID}"; //get all Messages that the main user recived sql
            sourseUsers = new DMessageList(base.Select());
            command.CommandText = "SELECT * FROM DMessageTbl" +
                $" WHERE [SourceUser] = {user.ID}"; //get all Messages that the main user sent sql
            destUsers = new DMessageList(base.Select());
            foreach (DMessage userS in sourseUsers)//for each user that the main user recived
                if (!(IsUserInUist(users, userS.SourceUser))) //if the second user is not in list
                    users.Add(userS.SourceUser); //add the user
            foreach (DMessage userD in destUsers) //for each user that the main user recived
                if (!(IsUserInUist(users, userD.DestUser)))//if the second user is not in list
                    users.Add(userD.DestUser);//add the user
            return users;
        }


        //returns a value that symbolizes the state of all chats
        //with that user the purpose is to check this update state value
        //to know if the the chats list should be refreshed
        public int GetChatsDMUpdateState(User user)
        { 
            UserList users = GetAllDMChats(user);
            int checksum = 0, count = 0;
            foreach (User userTemp in users)
            {
                checksum += (userTemp.ID * (int)userTemp.Uname[0]);
                count++;
            }
            checksum *= count;
            return checksum;
        }

        public DMessageList SelectAllDMsChat(User main, User chattedU)
        {
            UpdateSeen(main, chattedU);
            command.CommandText = "SELECT * FROM DMessageTbl" +
                $" WHERE ([DestUser] = {main.ID} AND [SourceUser] = {chattedU.ID}) " +
                $"OR ([DestUser] = {chattedU.ID} AND [SourceUser] = {main.ID})";
            return (new DMessageList(base.Select()));

        }

        public bool AreChatting(User main, User chattedU)
        {
            command.CommandText = "SELECT * FROM DMessageTbl" +
                $" WHERE ([DestUser] = {main.ID} AND [SourceUser] = {chattedU.ID}) " +
                $"OR ([DestUser] = {chattedU.ID} AND [SourceUser] = {main.ID}) LIMIT 1";
            return (new DMessageList(base.Select())).Count > 0;
        }
            
        public DMessageList SelectUnSeen(User main, User chattedU)
        {

            command.CommandText = "SELECT * FROM DMessageTbl" +
                $" WHERE ([DestUser] = {main.ID} AND [SourceUser] = {chattedU.ID} AND [Seen] = False) ";
            DMessageList dMessages = new DMessageList(base.Select());
            UpdateSeen(main, chattedU);
            foreach (DMessage message in dMessages)
            {
                message.Seen = true;
            }
            return dMessages;
        }

        private void UpdateSeen(User main, User chattedU)
        {
            EntityList pair = new EntityList();
            pair.Entities.Add(chattedU);
            pair.Entities.Add(main);
            updated.Add(new ChangeEntity(this.CreateUpdateSeenSQL, pair));
            this.SaveChanges();
        }
        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            DMessage message = entity as DMessage;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM DMessageTbl WHERE ID={0}", message.ID);
            return sql_builder.ToString();
        }

        protected override string CreateInsertSQL(BaseEntity entity)
        {
            DMessage message = entity as DMessage;
            return string.Format("INSERT INTO DMessageTbl ([SourceUser], [DestUser]," +
                " [Content], [Time], [Seen]) VALUES( {0}, {1}, '{2}', '{3}', {4})",
                message.SourceUser.ID, message.DestUser.ID, ToSqlStr(message.Content),
                DateTime.Now.ToString(), false.ToString());

        }

        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            DMessage message = entity as DMessage;
            return $"UPDATE DMessageTbl SET [Content]='{message.Content}', [Seen]='{message.Seen}'" +
                $" WHERE ID={message.ID}";

        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UserDB userDB = new UserDB();
            DMessage message = entity as DMessage;
            message.ID = (int)reader["ID"];
            message.DestUser = userDB.SelectByID((int)reader["DestUser"]);
            message.SourceUser= userDB.SelectByID((int)reader["SourceUser"]);
            message.Content = (string)reader["Content"];
            message.Seen = (bool)reader["Seen"];
            message.Time =(DateTime)reader["Time"];
            return message;
        }

        

        protected string CreateUpdateSeenSQL(BaseEntity entity)
        {
            EntityList entityList = entity as EntityList;
            User source = (User)entityList.Entities[0];
            User dest = (User)entityList.Entities[1];
            return $"UPDATE DMessageTbl SET [Seen]= True WHERE [DestUser]= {dest.ID} AND [SourceUser] = {source.ID}";

        }

        protected override BaseEntity NewEntity()
        {
            return new DMessage();
        }


    }
}
