using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{
    public class UnSeenGMDB : BaseDB
    {
        /// <summary>
        /// the method inserts the unseen entity into the database
        /// </summary>
        /// <param name="entity"></param>
        public override void Insert(BaseEntity entity)
        {
            //DataBases needed for the insert
            GMemberDB gMemberDB = new GMemberDB();
            UnSeenGM unSeen = entity as UnSeenGM;
            GMessageDB gMessageDB = new GMessageDB();
            if (unSeen != null)
            {
                //get the group in order to make sure it excists and correct.
                Group group = gMessageDB.SelectGMessageByID(unSeen.Message.ID).Group;
                unSeen.Group = group.ID;//add the group id to the unSeen object
                List<User> users = gMemberDB.GetUserMembers(group);//get all the members if tge group
                foreach (User user in users)
                {
                    //add an UnSeen for each user in order to make sure what messages the user 
                    //hasn't received yet.
                    UnSeenGM temp = new UnSeenGM(unSeen.Message, user.ID, group.ID);
                    inserted.Add(new ChangeEntity(this.CreateInsertSQL, temp));
                    
                }

            }
        }

        /// <summary>
        /// the method deletes an UnSeeb entity from the UnSeenGMTbl Database
        /// in order to mark that the user saw the message
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(BaseEntity entity)
        {
            UnSeenGM unSeen = entity as UnSeenGM;
            if (unSeen != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        

        public override void Update(BaseEntity entity)
        {
            UnSeenGM unSeen = entity as UnSeenGM;
            if (unSeen != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }
        /// <summary>
        /// the method gets all the messages that the user in the group hasn't seen yet
        /// </summary>
        /// <param name="user"> the user that want to receive unseen messages</param>
        /// <param name="group">the group that the user in</param>
        /// <returns>list of GMessages that the user hasn't received yet</returns>
        public List<GMessage> GetChatsUnSeenMessages(User user, Group group)
        {

            command.CommandText = "SELECT * FROM UnSeenGMTbl" +
                $" WHERE [Group] = {group.ID} AND [USER] = {user.ID}" ;
            List<GMessage> messages =(new UnSeenGMList(base.Select())).Select(unseen => unseen.Message).ToList();
            Delete(new UnSeenGM(null, user.ID, group.ID)); //delete all messages that the user receives from unseen database
            SaveChanges();//save the changes in the group unseen messages database
            return messages;
            

        }
        /// <summary>
        /// makes sql that deletes every message that certain user seen from the list of didn't unseen messages 
        /// </summary>
        /// <param name="entity">an unseen message with user and group fluids are used</param>
        /// <returns>sql string that deletes all seen messages</returns>
        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            UnSeenGM unSeen = entity as UnSeenGM;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM UnSeenGMTbl WHERE [User]= {0} AND [Group]= {1}", unSeen.User, unSeen.Group);
            return sql_builder.ToString();
        }
        /// <summary>
        /// makes sql string that inserts a message to an unseen group messages database
        /// in order to mark a message as unseen by user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override string CreateInsertSQL(BaseEntity entity)
        {
            UnSeenGM unSeen = entity as UnSeenGM;
            return string.Format("INSERT INTO UnSeenGMTbl ([User], [Message], [Group]) VALUES( {0}, {1}, {2} )",
                unSeen.User, unSeen.Message.ID, unSeen.Group);
        }
        /// <summary>
        /// receives data from database about unseen message by user
        /// </summary>
        /// <param name="entity">An empty UnSeenGM object pointer that the method fills </param>
        /// <returns>filled recieved entity entity</returns>
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UnSeenGM unSeen = entity as UnSeenGM;
            GMessageDB gMessageDB = new GMessageDB();
            unSeen.Message = gMessageDB.SelectGMessageByID((int)reader["Message"]);
            unSeen.User = (int)reader["User"];
            unSeen.Group = (int)reader["Group"];
            return unSeen;
        }
        
        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        /// method returns an empty entity of UnSeenGM
        protected override BaseEntity NewEntity()
        {
            return new UnSeenGM();
        }
    }
}
