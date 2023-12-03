using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GMessage : BaseEntity
    {
        private int iD;
        private Group group;
        private User user;
        private string content;
        public GMessage() { }
        public GMessage(Group group, User user, string content)
        {
            this.group = group;
            this.user = user;
            this.content = content;
        }

        public int ID { get => iD; set => iD = value; }
        public Group Group { get => group; set => group = value; }
        public User User { get => user; set => user = value; }
        public string Content { get => content; set => content = value; }
    }
    public class GMessageList : List<GMessage>
    {
        public GMessageList() { }
        public GMessageList(IEnumerable<GMessage> list) : base(list) { }
        public GMessageList(IEnumerable<BaseEntity> list) : base(list.Cast<GMessage>().ToList()) { }
    }
}
