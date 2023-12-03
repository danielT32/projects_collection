using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{
    public class CommentDB : BaseDB
    {
        public override void Delete(BaseEntity entity)
        {
            Comment comment = entity as Comment;
            if (comment != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        public CommentList SelectByPost(Post post)
        {
            command.CommandText = string.Format("SELECT * FROM CommentTbl WHERE [Post] = {0} ", post.ID);
            return new CommentList(base.Select());
        }

        public override void Insert(BaseEntity entity)
        {
            Comment comment = entity as Comment;
            if (comment != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Comment comment = entity as Comment;
            if (comment != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            Comment comment = entity as Comment;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM CommentTbl WHERE ID={0}", comment.ID);
            return sql_builder.ToString();
        }

        protected override string CreateInsertSQL(BaseEntity entity)
        {
            Comment comment = entity as Comment;
            return $"INSERT INTO CommentTbl ([User], [Post], [Content]) VALUES({comment.User.ID}, {comment.Post.ID}, '{ToSqlStr(comment.Content)}')";

        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UserDB userDB = new UserDB();
            PostDB postDB = new PostDB();
            Comment comment = entity as Comment;
            comment.ID = (int)reader["ID"];
            comment.User = userDB.SelectByID((int)reader["User"]);
            comment.Post = postDB.SelectByID((int)reader["Post"]);
            comment.Content = (string)reader["Content"];
            return comment;
        }

        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            Comment comment = entity as Comment;
            return $"UPDATE CommentTbl SET [Content]='{ToSqlStr(comment.Content)}' WHERE ID={comment.ID}";
        }

        protected override BaseEntity NewEntity()
        {
            return new Comment() as BaseEntity;
        }
    }
}
