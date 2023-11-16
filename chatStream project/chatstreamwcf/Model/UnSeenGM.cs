using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UnSeenGM : BaseEntity
    {
        private GMessage message; //group message
        private int user; // user ID to reduce memory usage
        private int group; // group ID to reduce memory usage
        //constructorss
        public UnSeenGM() { }
        public UnSeenGM(GMessage message, int user, int group)
        {
            this.message = message;
            this.user = user;
            this.group = group;
        }
        //getters and setters
        public GMessage Message { get => message; set => message = value; }
        public int User { get => user; set => user = value; }
        public int Group { get => group; set => group = value; }
    }
    public class UnSeenGMList : List<UnSeenGM>
    {
        public UnSeenGMList() { }
        public UnSeenGMList(IEnumerable<UnSeenGM> list) : base(list) { }
        public UnSeenGMList(IEnumerable<BaseEntity> list) : base(list.Cast<UnSeenGM>().ToList()) { }
    }
}
