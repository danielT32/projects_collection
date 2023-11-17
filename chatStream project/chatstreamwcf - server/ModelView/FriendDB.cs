using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ModelView
{
    public class FriendDB : BaseDB
    {
        public override void Delete(BaseEntity entity)
        {
            Friend friend = entity as Friend;
            if (friend != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        public override void Insert(BaseEntity entity)
        {
            Friend friend = entity as Friend;
            if (friend != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }
        public FriendList SelectRecivedReq (User user)
        {
            command.CommandText = string.Format("SELECT * FROM FriendTbl WHERE [User2] = {0} AND [Approved] = false", user.ID);
            return new FriendList(base.Select());
        }

        public FriendList SelectUserFriends(User user)
        {
            command.CommandText = string.Format("SELECT * FROM FriendTbl WHERE ([User2] = {0} OR [User1] = {0}) AND [Approved] = True", user.ID);
            return new FriendList(base.Select());
        }
        public bool AreFriends(int user1, int user2)
        {
            command.CommandText = string.Format("SELECT * FROM FriendTbl WHERE (([User1] = {0} AND [User2] = {1}) OR ([User1] = {1} AND [User2] = {0})) AND [Approved] = true", user1, user2);
            return (new FriendList(base.Select())).Count > 0;
        }
        public Friend SelectFriend(User user1, User user2)
        {
            command.CommandText = string.Format("SELECT * FROM FriendTbl WHERE ([User1] = {0} AND [User2] = {1}) OR ([User1] = {1} AND [User2] = {0})", user1.ID, user2.ID);
            FriendList results = new FriendList(base.Select());
            if (results.Count() > 0)
            {
                return results[0];
            }
            else
                return null;
        }
        public bool AreFriends(Friend friend)
        {
            return AreFriends(friend.User1.ID, friend.User2.ID);
        }
        public override void Update(BaseEntity entity)
        {
            Friend friend = entity as Friend;
            if (friend != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            Friend friend = entity as Friend;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM FriendTbl WHERE ([User1]={0} AND [User2]={1}) OR ([User1]={1} AND [User2]={0})", friend.User1.ID, friend.User2.ID);
            return sql_builder.ToString();
        }

        protected override string CreateInsertSQL(BaseEntity entity)
        {
            Friend friend = entity as Friend;
            return string.Format("INSERT INTO FriendTbl ([User1], [User2], [Approved]) VALUES({0}, {1}, false)",
                friend.User1.ID, friend.User2.ID);
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UserDB userDB = new UserDB();
            Friend friend = entity as Friend;
            friend.User1 = userDB.SelectByID((int)reader["User1"]);
            friend.User2 = userDB.SelectByID((int)reader["User2"]);
            friend.Approved = (bool)reader["Approved"];
            return friend;
        }

        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            Friend friend = entity as Friend;
            return $"UPDATE FriendTbl SET [Approved]={friend.Approved} WHERE ([User1]={friend.User1.ID} AND [User2]={friend.User2.ID}) OR ([User1]={friend.User2.ID} AND [User2]={friend.User1.ID})";
        }

        protected override BaseEntity NewEntity()
        {
            return new Friend() as BaseEntity;
        }
    }
}
