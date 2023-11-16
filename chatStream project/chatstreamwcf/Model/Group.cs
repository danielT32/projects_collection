using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Group : BaseEntity
    {
        //properties
        private int iD; //מסםפר מזהה של הקבוצה
        private string gNname; //שם הקבוצה
        private string description; //תיאור הקבוצה
        private bool publicity; // האם הקבוצה ציבורית או פרטית
        //getters and setters
        public int ID { get => iD; set => iD = value; }
        public string GNname { get => gNname; set => gNname = value; }
        public string Description { get => description; set => description = value; }
        public bool Publicity { get => publicity; set => publicity = value; }
    }
    public class GroupList : List<Group>
    {
        public GroupList() { }
        public GroupList(IEnumerable<Group> list) : base(list) { }
        public GroupList(IEnumerable<BaseEntity> list) : base(list.Cast<Group>().ToList()) { }
    }
}
