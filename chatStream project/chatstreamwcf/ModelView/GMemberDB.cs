using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{
    public class GMemberDB : BaseDB
    {
        public override void Delete(BaseEntity entity)
        {
            GMember gMember = entity as GMember;
            if (gMember != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeleteSQL, entity));
            }
        }

        public override void Insert(BaseEntity entity)
        {
            GMember gMember = entity as GMember;
            if (gMember != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            GMember gMember = entity as GMember;
            if (gMember != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        protected override string CreateDeleteSQL(BaseEntity entity)
        {
            GMember member = entity as GMember;
            StringBuilder sql_builder = new StringBuilder();
            sql_builder.AppendFormat("DELETE FROM GMemberTbl WHERE [User]= {0} AND [Group]= {1}", member.User.ID, member.Group.ID);
            return sql_builder.ToString();
        }
        

        protected override string CreateInsertSQL(BaseEntity entity)
        {
            GMember member = entity as GMember;
            return string.Format("INSERT INTO GMemberTbl ([User], [Group], [Admin]) VALUES( {0}, {1}, {2} )",
                member.User.ID, member.Group.ID, false.ToString());

        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UserDB userDB = new UserDB();
            GroupDB groupDB = new GroupDB();
            GMember gMember = entity as GMember;
            gMember.User = userDB.SelectByID((int)reader["User"]);
            gMember.Group = groupDB.SelectGroup((int)reader["Group"]);
            gMember.Admin = (bool)reader["Admin"];
            return gMember;
        }

        protected override string CreateUpdateSQL(BaseEntity entity)
        {
            GMember member = entity as GMember;
            return $"UPDATE GMemberTbl SET [Admin]={member.Admin} WHERE [User]={member.User.ID} AND [Group]={member.Group.ID} ";

        }

        protected override BaseEntity NewEntity()
        {
            return new GMember();
        }

        public GMemberList GetMembers(Group group)
        {
            command.CommandText = "SELECT * FROM GMemberTbl" +
                $" WHERE [Group] = {group.ID}";
            return new GMemberList(base.Select());
        }
        public List<User> GetUserMembers(Group group)
        {
            command.CommandText = "SELECT * FROM GMemberTbl" +
                $" WHERE [Group] = {group.ID}";
            return new GMemberList(base.Select()).Select(member => member.User).ToList(); ;
        }
        public List<User> SelectAdmins(Group group)
        {
            command.CommandText = "SELECT * FROM GMemberTbl" +
                $" WHERE [Group] = {group.ID} AND [Admin] = True";
            GMemberList adminMembers = new GMemberList(base.Select());
            return adminMembers.Select(member => member.User).ToList();
        }
        public List<User> SelectNonAdmins(Group group)
        {
            command.CommandText = "SELECT * FROM GMemberTbl" +
                $" WHERE [Group] = {group.ID} AND [Admin] = False";
            GMemberList adminMembers = new GMemberList(base.Select());
            return adminMembers.Select(member => member.User).ToList();
        }
        public bool IsAdmin(GMember member)
        {
            command.CommandText = "SELECT * FROM GMemberTbl" +
                $" WHERE [Group] = {member.Group.ID} AND [User] = {member.User.ID}";
            GMemberList members = new GMemberList(base.Select());
            return members.Count() > 0 ? members[0].Admin : false;
        }
        public bool IsMember(GMember member)
        {
            command.CommandText = "SELECT * FROM GMemberTbl" +
                $" WHERE [Group] = {member.Group.ID} AND [User] = {member.User.ID}";
            GMemberList members = new GMemberList(base.Select());
            return members.Count() > 0;
        }
        public List<Group> GetUsersGroup(User user)
        {
            command.CommandText = "SELECT * FROM GMemberTbl" +
                $" WHERE [User] = {user.ID}";
            GMemberList members = new GMemberList(base.Select());
            return members.Select(member => member.Group).ToList();
        }

    }
}
