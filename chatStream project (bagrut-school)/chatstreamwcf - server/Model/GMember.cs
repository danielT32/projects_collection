using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GMember : BaseEntity
    {
        private Group group;
        private User user;
        private bool admin;
        
        public Group Group { get => group; set => group = value; }
        public User User { get => user; set => user = value; }
        public bool Admin { get => admin; set => admin = value; }

        public GMember() { }
        public GMember(Group group, User user, bool admin)
        {
            this.group = group;
            this.user = user;
            this.admin = admin;
        }
    }
    public class GMemberList : List<GMember>
    {
        public GMemberList() { }
        public GMemberList(IEnumerable<GMember> list) : base(list) { }
        public GMemberList(IEnumerable<BaseEntity> list) : base(list.Cast<GMember>().ToList()) { }
    }
}
