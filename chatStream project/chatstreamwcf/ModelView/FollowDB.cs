using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{
    public class FollowDB : BaseDB
    {
        public override void Delete(BaseEntity entity)
        {
            Follow follow = entity as Follow;
            if (follow != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        public override void Insert(BaseEntity entity)
        {
            Follow follow = entity as Follow;
            if (follow != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }
        public FollowList SelectFollowed(User follower)
        {
            command.CommandText = string.Format("SELECT * FROM FollowTbl WHERE User = {0}", follower.ID);
            return new FollowList(base.Select());
        }
        public bool IsFollowing (int follower, int followed)
        {
            command.CommandText = string.Format("SELECT * FROM FollowTbl WHERE User = {0} AND Followed = {1} ", follower, followed);
            return (new FollowList(base.Select())).Count > 0;
        }
        public bool IsFollowing(Follow follow)
        {
            return IsFollowing(follow.Follower.ID, follow.Following.ID);
        }
        public override void Update(BaseEntity entity)
        {
            Follow follow = entity as Follow;
            if (follow != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            Follow follow = entity as Follow;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM FollowTbl WHERE [User]={0} AND [Followed]={1}", follow.Follower.ID, follow.Following.ID);
            return sql_builder.ToString();
        }

        protected override string CreateInsertSQL(BaseEntity entity)
        {
            Follow follow = entity as Follow;
            return string.Format("INSERT INTO FollowTbl ([User], [Followed]) VALUES({0}, {1})",
                follow.Follower.ID, follow.Following.ID);
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UserDB userDB = new UserDB();
            Follow follow = entity as Follow;
            follow.Follower = userDB.SelectByID((int)reader["User"]);
            follow.Following = userDB.SelectByID((int)reader["Followed"]); ;
            return follow;
        }

        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            return "";
        }

        protected override BaseEntity NewEntity()
        {
            return new Follow() as BaseEntity;
        }
    }
}
