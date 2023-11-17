using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{
    public class GroupDB : BaseDB
    {
        public override void Delete(BaseEntity entity)
        {
            Group group = entity as Group;
            if (group != null)
            {
                deleted.Add(new ChangeEntity(CreateDeleteAllUnSeenSQL, entity));
                deleted.Add(new ChangeEntity(CreateDeleteAllMessagesSQL, entity));
                deleted.Add(new ChangeEntity(CreateDeleteMembersSQL, entity));
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        public string CreateDeleteAllUnSeenSQL(BaseEntity entity)
        {
            Group group = entity as Group;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM UnSeenGMTbl WHERE [Group]= {0}",  group.ID);
            return sql_builder.ToString();
        }
        public string CreateDeleteAllMessagesSQL(BaseEntity entity)
        {
            Group group = entity as Group;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM GMessageTbl WHERE [Group] = {0}", group.ID);
            return sql_builder.ToString();
        }
        public string CreateDeleteMembersSQL(BaseEntity entity)
        {
            Group group = entity as Group;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM GMemberTbl WHERE [Group]= {0}", group.ID);
            return sql_builder.ToString();
        }

        public override void Insert(BaseEntity entity)
        {
            Group group = entity as Group;
            if (group != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Group group = entity as Group;
            if (group != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            Group group = entity as Group;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM GroupTbl WHERE ID={0}", group.ID);
            return sql_builder.ToString();
        }

        protected override string CreateInsertSQL(BaseEntity entity)
        {
            Group group = entity as Group;
            return string.Format("INSERT INTO GroupTbl ([GName], [Description], [Publicity]) VALUES( '{0}', '{1}', {2} )",
                ToSqlStr(group.GNname), ToSqlStr(group.Description), group.Publicity.ToString());

        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Group group = entity as Group;
            group.ID = (int)reader["ID"];
            group.Description = (string)reader["Description"];
            group.GNname = (string)reader["GName"];
            try
            {
                group.Publicity = (bool)reader["Publicity"];
            }
            catch (Exception e)
            {
                group.Publicity = true;
                System.Diagnostics.Debug.WriteLine("exeption: " + e.Message);
                Delete(group);
            }

            return group;
        }
        public Group SelectGroup(int ID)
        {
            command.CommandText = $"SELECT * FROM GroupTbl WHERE GroupTbl.ID = {ID} ";
            GroupList groups = new GroupList(base.Select());
            SaveChanges();
            return groups.Count() > 0 ? groups[0] : null ;
        }

        public GroupList SelectGroupByGName(string gName)
        {
            command.CommandText = string.Format("SELECT * FROM GroupTbl WHERE [Publicity] = {0} AND [GName] LIKE '{1}%' ", true.ToString() , ToSqlStr(gName));
            GroupList groups = new GroupList(base.Select());
            SaveChanges();
            return groups;
        }
        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            Group group = entity as Group;
            return $"UPDATE GroupTbl SET [GName]='{group.GNname}', [Description]='{group.Description}', [Publicity]={group.Publicity} WHERE ID = {group.ID}";

        }

        protected override BaseEntity NewEntity()
        {
            return new Group() as BaseEntity;
        }
    }
}
