using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DMessage : BaseEntity
    {
        //properties
        private int iD; //מספר מזהה של ההודעה
        private User destUser; // משתמש יעד של ההודעה
        private User sourceUser; //משתמש שכתב את ההודעה
        private string content; //תוכן ההודעה
        private bool seen; //האם ההודעה נשלחה ונראתה על ידי משתמש היעד
        private DateTime time; //זמן שליחת ההודעה
        //constructors
        public DMessage()
        { }
        public DMessage(User destUser, User sourceUser, string content)
        {
            this.DestUser = destUser;
            this.SourceUser = sourceUser;
            this.content = content;
            this.seen = false;
            this.iD = 0;
        }
        //getters and setters
        public int ID { get => iD; set => iD = value; }
        public string Content { get => content; set => content = value; }
        public bool Seen { get => seen; set => seen = value; }
        public DateTime Time { get => time; set => time = value; }
        public User DestUser { get => destUser; set => destUser = value; }
        public User SourceUser { get => sourceUser; set => sourceUser = value; }
    }
    public class DMessageList : List<DMessage>
    {
        public DMessageList() { }
        public DMessageList(IEnumerable<DMessage> list) : base(list) { }
        public DMessageList(IEnumerable<BaseEntity> list) : base(list.Cast<DMessage>().ToList()) { }
    }
}
