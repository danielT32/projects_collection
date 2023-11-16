using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Like : BaseEntity
    {
        private Post post;
        private User user;
        public Like() 
        { }
        public Like(Post post, User user)
        {
            this.post = post;
            this.user = user;
        }
        public Post Post { get => post; set => post = value; }
        public User User { get => user; set => user = value; }
    }
    public class LikeList : List<Like>
    {
        public LikeList() { }
        public LikeList(IEnumerable<Like> list) : base(list) { }
        public LikeList(IEnumerable<BaseEntity> list) : base(list.Cast<Like>().ToList()) { }
    }
}
