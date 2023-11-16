using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Comment : BaseEntity
    {
        
        public User User { get => user; set => user = value; }
        public Post Post { get => post; set => post = value; }
        public string Content { get => content; set => content = value; }
        public int ID { get => iD; set => iD = value; }

        private int iD;
        private User user;
        private Post post;
        private string content;

        public Comment(User user, Post post, string content)
        {
            this.user = user;
            this.post = post;
            this.content = content;
        }

        public Comment() { }

        public Comment(int id, User user, Post post, string content)
        {
            this.iD = id;
            this.user = user;
            this.post = post;
            this.content = content;
        }
    }

    public class CommentList : List<Comment>
    {
        public CommentList() { }
        public CommentList(IEnumerable<Comment> list) : base(list) { }
        public CommentList(IEnumerable<BaseEntity> list) : base(list.Cast<Comment>().ToList()) { }
    }
}
