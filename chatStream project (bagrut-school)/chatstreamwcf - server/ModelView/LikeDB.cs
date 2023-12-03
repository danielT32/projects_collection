using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ModelView
{
    public class LikeDB : BaseDB
    {
        public override void Delete(BaseEntity entity)
        {
            Like like = entity as Like;
            if (like != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        public override void Insert(BaseEntity entity)
        {
            Like like = entity as Like;
            if (like != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Like like = entity as Like;
            if (like != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            
            Like like = entity as Like;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM LikeTbl WHERE [User] = {0} AND [Post] = {1}", like.User.ID, like.Post.ID);
            return sql_builder.ToString();
        }

        protected override string CreateInsertSQL(BaseEntity entity)
        {
            Like like = entity as Like;
            return string.Format("INSERT INTO LikeTbl ([User], [Post]) VALUES({0}, {1})",
                like.User.ID, like.Post.ID);
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UserDB userDB = new UserDB();
            PostDB postDB = new PostDB();
            Like like = entity as Like;
            like.User = userDB.SelectByID((int)reader["User"]);
            like.Post = postDB.SelectByID((int)reader["Post"]);
            return like;
        }

        public LikeList SelectByPost(Post post)
        {
            command.CommandText = "SELECT * FROM LikeTbl" +
                $" WHERE [Post] = {post.ID}";
            LikeList likeList = new LikeList(base.Select());
            return likeList;
        }
        public int CountLikes(Post post)
        {
            return SelectByPost(post).Count;
        }
        public bool IsLiked(Like like)
        {
            command.CommandText = "SELECT * FROM LikeTbl" +
                $" WHERE [Post]={like.Post.ID} AND [User]={like.User.ID}";
            LikeList likeList = new LikeList(base.Select());
            return likeList.Count > 0;
        }

        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            return "";

        }

        protected override BaseEntity NewEntity()
        {
            return new Like() as BaseEntity;
        }
    }
}
