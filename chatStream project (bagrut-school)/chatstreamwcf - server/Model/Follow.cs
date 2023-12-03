using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Follow : BaseEntity
    {
        private User follower;
        private User following;

        public Follow(User follower, User following)
        {
            this.follower = follower;
            this.following = following;
        }
        public Follow() { }

        public User Follower { get => follower; set => follower = value; }
        public User Following { get => following; set => following = value; }
    }
    public class FollowList : List<Follow>
    {
        public FollowList() { }
        public FollowList(IEnumerable<Follow> list) : base(list) { }
        public FollowList(IEnumerable<BaseEntity> list) : base(list.Cast<Follow>().ToList()) { }
    }
}
