using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Friend : BaseEntity
    {
        //properties
        private User user1; //first user who initiated friend request
        private User user2; //second user
        private bool approved; //is the friendship is approved by the second user.
        //constructers
        public Friend(User user1, User user2, bool approved)
        {
            this.user1 = user1;
            this.user2 = user2;
            this.approved = approved;
        }
        public Friend() { this.approved = false; }
        public Friend(User user1, User user2)
        {
            this.user1 = user1;
            this.user2 = user2;
            this.approved = false;
        }
        //getters and setters
        public User User1 { get => user1; set => user1 = value; }
        public User User2 { get => user2; set => user2 = value; }
        public bool Approved { get => approved; set => approved = value; }
    }
    public class FriendList : List<Friend>
    {
        public FriendList() { }
        public FriendList(IEnumerable<Friend> list) : base(list) { }
        public FriendList(IEnumerable<BaseEntity> list) : base(list.Cast<Friend>().ToList()) { }
    }
}
