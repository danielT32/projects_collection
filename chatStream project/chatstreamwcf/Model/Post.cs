using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Post : BaseEntity
    {
        private int iD;
        private User user;
        private string content;
        private bool isPrivate;

        public int ID { get => iD; set => iD = value; }
        public User User { get => user; set => user = value; }
        public string Content { get => content; set => content = value; }
        public bool IsPrivate { get => isPrivate; set => isPrivate = value; }
    }
    public class PostList : List<Post>
    {
        public PostList() { }
        public PostList(IEnumerable<Post> list) : base(list) { }
        public PostList(IEnumerable<BaseEntity> list) : base(list.Cast<Post>().ToList()) { }
    }
}
