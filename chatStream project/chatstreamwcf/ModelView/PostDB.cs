using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ModelView
{
    public class PostDB: BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Post() as BaseEntity;
        }

        public PostList SelectAll()
        {
            command.CommandText = "SELECT * FROM PostTbl";
            return new PostList(base.Select());
        }

        public PostList SelectMyFeed(User user)
        {
            FollowDB followDB = new FollowDB();
            PostList posts = new PostList();
            foreach (Follow follow in followDB.SelectFollowed(user))
            {
                foreach(Post post in this.SelectUserFeed(user, follow.Following))
                    posts.Add(post);
            }
            posts.Sort(delegate (Post post1, Post post2)
            {
                return -post1.ID.CompareTo(post2.ID);
            });
            return posts;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UserDB userDB = new UserDB();
            Post post = entity as Post;
            post.ID = (int)reader["ID"];
            post.User = userDB.SelectByID((int)reader["User"]);
            post.Content = (string)reader["Content"];
            post.IsPrivate = (bool)reader["Private"];
            return post;
        }

        public PostList SelectUserFeed(User mainUser, User req)
        {
            FriendDB friendDB = new FriendDB();
            if (mainUser.ID == req.ID || friendDB.AreFriends(mainUser.ID, req.ID))
                command.CommandText = string.Format("SELECT * FROM PostTbl WHERE [User] = {0}", req.ID);
            else
                command.CommandText = string.Format("SELECT * FROM PostTbl WHERE [User] = {0} AND [Private] = false ", req.ID);
            PostList posts = new PostList(base.Select());
            posts.Sort(delegate (Post post1, Post post2)
            {
                return -post1.ID.CompareTo(post2.ID);
            });
            return posts;
        }
        public PostList SelectByUID(int ID)
        {
            command.CommandText = string.Format("SELECT * FROM PostTbl WHERE User = {0} AND [Private] = false ", ID);
            return new PostList(base.Select());
        }
        public PostList SelectByUser(User user)
        {
            return SelectByUID(user.ID);
        }
        public Post SelectByID(int id)
        {
            command.CommandText = "SELECT * FROM PostTbl" +
                $" WHERE PostTbl.ID = {id}";
            PostList postList = new PostList(base.Select());
            if (postList.Count == 1)
            {
                return postList[0];
            }
            else
                return null;
        }
        public override void Insert(BaseEntity entity)
        {
            Post post = entity as Post;
            if (post != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }
        public override void Update(BaseEntity entity)
        {
            Post post = entity as Post;
            if (post != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }
        public override void Delete(BaseEntity entity)
        {
            User people = entity as User;
            if (people != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        protected override string CreateInsertSQL(BaseEntity entity)
        {
            Post post = entity as Post;
            return $"INSERT INTO PostTbl ([User], [Content], [Private]) VALUES({post.User.ID}, '{ToSqlStr(post.Content)}', {post.IsPrivate})";
        }

        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            Post post = entity as Post;
            return $"UPDATE PostTbl SET [Private]={post.IsPrivate}, [Content]='{ToSqlStr(post.Content)}' WHERE ID={post.ID}";
        }

        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            Post post = entity as Post;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM PostTbl WHERE ID={0}", post.ID);
            return sql_builder.ToString();
        }
    }
}
