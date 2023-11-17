using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class BaseEntity 
    {
    }
    public class EntityList : BaseEntity
    {
        private List<BaseEntity> entities;
        public EntityList()
        {
            this.Entities = new List<BaseEntity>();
        }
        
        public EntityList(List<BaseEntity> entities)
        {
            this.entities = entities;
        }

        public List<BaseEntity> Entities { get => entities; set => entities = value; }
    }
}
