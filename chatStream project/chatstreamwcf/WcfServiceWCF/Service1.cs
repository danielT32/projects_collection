using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;
using ModelView;

namespace WcfServiceWCF
{
    public class Service1 : IService1
    {
        //databases used:
        private CommentDB commentDB = new CommentDB();
        private UserDB userDB = new UserDB(); 
        private FollowDB followDB = new FollowDB(); 
        private FriendDB friendDB = new FriendDB(); 
        private LikeDB likeDB = new LikeDB(); 
        private PostDB postDB = new PostDB(); 
        private DMessageDB dMessageDB = new DMessageDB();
        private GroupDB groupDB = new GroupDB();
        private GMemberDB gMemberDB = new GMemberDB();
        private GMessageDB gMessageDB = new GMessageDB();
        private UnSeenGMDB unSeenGMDB = new UnSeenGMDB();
        //operations implementation
        public void DeleteComment(Comment entity)
        {
            commentDB.Delete(entity);
            commentDB.SaveChanges();
        }
        public void InsertComment(Comment entity)
        {
            commentDB.Insert(entity);
            commentDB.SaveChanges();
        }
        public void UpdateComment(Comment entity)
        {
            commentDB.Update(entity);
            commentDB.SaveChanges();
        }
        public List<Comment> SelectCommentByPost(Post post)
        {
            return commentDB.SelectByPost(post);
        }
        public User InsertUser(User entity)
        {
            UserList users = userDB.SelectUNameExist(entity.Uname);
            if (users.Count > 0)
            {
                entity.ID = 0;
                return entity;
            }
            userDB.Insert(entity);
            userDB.SaveChanges();
            return entity;
        }

        public bool IsFollowing(Follow follow)
        {
            return followDB.IsFollowing(follow);
        }
        public User Login(string userName, string password)
        {
            UserList user = null;
            if (userName != "" && password != "")
            {
                user = userDB.SelectUserData(userName, password);
            }
            if(user != null && user.Count > 0)
                return user[0];
            return null;
            
        }
        public User SelectByUserID(int ID)
        {
            return userDB.SelectByID(ID);
        }
        public List<User> SelectUserByUName(string Uname)
        {
            return userDB.SelectByUName(Uname);
        }
        public void UpdateUser(User user)
        {
            userDB.Update(user);
            userDB.SaveChanges();
        }
        public void DeleteUser(string userName, string password)
        {
            User user = new User();
            user.Uname = userName;
            user.Password = password;
            userDB.Delete(user);
            userDB.SaveChanges();
        }
        public User SignUp(User user)
        {
            UserList userExcist = userDB.SelectUNameExist(user.Uname);
            if (userExcist.Count > 0)
            {
                return null;
            }
            userDB.Insert(user);
            userDB.SaveChanges();
            return user;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public Follow InsertFollow(Follow follow)
        {
            followDB.Insert(follow);
            followDB.SaveChanges();
            return follow;
        }
        public void DeleteFollow(Follow follow)
        {
            followDB.Delete(follow);
            followDB.SaveChanges();
        }
        public List<Follow> SelectFollowed(User follower)
        {
            return followDB.SelectFollowed(follower);
        }
        public Friend InsertFriend(Friend friend)
        {
            friendDB.Insert(friend);
            friendDB.SaveChanges();
            return friend;
        }
        public void DeleteFriend(Friend friend)
        {
            friendDB.Delete(friend);
            friendDB.SaveChanges();
            
        }
        public bool AreFriends(Friend friend)
        {
            return friendDB.AreFriends(friend);
        }
        public Friend SelectFriend(User user1, User user2)
        {
            return friendDB.SelectFriend(user1, user2);
        }
        public List<Friend> SelectRecivedReq(User user)
        {
            return friendDB.SelectRecivedReq(user);
        }
        public List<Friend> SelectUserFriends(User user)
        {
            return friendDB.SelectUserFriends(user);
        }
        public void ApproveFriend(Friend friend)
        {
            friend.Approved = true;
            friendDB.Update(friend);
            friendDB.SaveChanges();
        }
        public Like InsertLike(Like like)
        {
            likeDB.Insert(like);
            likeDB.SaveChanges();
            return like;
        }
        public void DeleteLike(Like like)
        {
            likeDB.Delete(like);
            likeDB.SaveChanges();
        }

        public int CountLikes(Post post)
        {
            return likeDB.CountLikes(post);
        }

        public bool IsLiked(Like like)
        {
            return likeDB.IsLiked(like);
        }

        public Post InsertPost(Post post)
        {
            postDB.Insert(post);
            postDB.SaveChanges();
            return post;
        }
        public void DeletePost(Post post)
        {
            postDB.Delete(post);
            postDB.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            postDB.Update(post);
            postDB.SaveChanges();
        }

        public List<Post> SelectUserFeed(User mainUser, User req)
        {
            return postDB.SelectUserFeed(mainUser, req);
        }

        public List<Post> SelectMyFeed(User user)
        {
            return postDB.SelectMyFeed(user);
        }

        public List<Post> SelectPostsByUser(User user)
        {
            return postDB.SelectByUser(user);
        }

        public List<Post> SelectPostsByUID(int ID)
        {
            return postDB.SelectByUID(ID);
        }
        public Post SelectPostByID(int id)
        {
            return postDB.SelectByID(id);
        }

        public void InsertDM(DMessage dMessage)
        {
            dMessageDB.Insert(dMessage);
            dMessageDB.SaveChanges();
        }

        public List<User> GetAllDMChats(User main)
        {
            return dMessageDB.GetAllDMChats(main);
        }

        public List<DMessage> SelectAllDMsChat(User main, User chatted)
        {
            return dMessageDB.SelectAllDMsChat(main, chatted);
        }

        public bool AreChatting(User main, User chatted)
        {
            return dMessageDB.AreChatting(main, chatted);
        }

        public List<DMessage> SelectUnSeenDM(User main, User chatted)
        {
            return dMessageDB.SelectUnSeen(main, chatted);
        }

        public int GetChatsDMUpdateState(User main)
        {
            return dMessageDB.GetChatsDMUpdateState(main);
        }


        public Group GetGroupByID(int groupID)
        {
            return groupDB.SelectGroup(groupID);
        }

        public List<Group> GetGroupsByName(string name)
        {
            return groupDB.SelectGroupByGName(name);
        }

        public void InsertGroup(Group group, User creator)
        {
            groupDB.Insert(group);
            groupDB.SaveChanges();
            InsertGMemeber(group, creator);
            UpdateGMemberStatus(new GMember(group, creator, true));
        }

        public void UpdateGroup(Group group)
        {
            groupDB.Update(group);
            groupDB.SaveChanges();
        }

        public void DeleteGroup(Group group)
        {
            groupDB.Delete(group);
            groupDB.SaveChanges();
        }

        public GMember InsertGMemeber(Group group, User user)
        {
            GMember gMember = new GMember(group, user, true);
            gMemberDB.Insert(gMember);
            gMemberDB.SaveChanges();
            return gMember;
        }

        public void DeleteGMember(GMember member)
        {
            gMemberDB.Delete(member);
            gMemberDB.SaveChanges();
        }

        public void UpdateGMemberStatus(GMember member)
        {
            gMemberDB.Update(member);
            gMemberDB.SaveChanges();
        }

        public GMemberList GetGroupMembers(Group group)
        {
            return gMemberDB.GetMembers(group);
        }

        public List<User> SelectGroupAdmins(Group group)
        {
            return gMemberDB.SelectAdmins(group);
        }

        public List<User> SelectGroupNonAdmins(Group group)
        {
            return gMemberDB.SelectNonAdmins(group);
        }

        public bool IsGroupAdmin(GMember member)
        {
            return gMemberDB.IsAdmin(member);
        }

        public bool IsGroupMember(GMember member)
        {
            return gMemberDB.IsMember(member);
        }

        public GMessage InsertGMessage(GMessage message)
        {
            gMessageDB.Insert(message);
            gMessageDB.SaveChanges();
            return message;
        }

        public void DeleteGMessage(GMessage message)
        {
            gMessageDB.Delete(message);
            gMessageDB.SaveChanges();
        }

        public void UpdateGMessage(GMessage message)
        {
            gMessageDB.Update(message);
            gMessageDB.SaveChanges();
        }

        public List<GMessage> SelectGMessages(User user, Group group)
        {
            return gMessageDB.GetMessages(user, group);
        }

        public List<GMessage> GetGMessages(User user, Group group)
        {
            return gMessageDB.GetMessages(user, group);
        }

        public List<GMessage> GetUnSeenGMessages(User user, Group group)
        {
            return unSeenGMDB.GetChatsUnSeenMessages(user, group);
        }

        public List<Group> GetMyGroups(User user)
        {
            return gMemberDB.GetUsersGroup(user);
        }
        public List<Group> SearchPublicGroups(string groupName)
        {
            return groupDB.SelectGroupByGName(groupName);
        }

        public int GetMyGroupsUpdateState(User user)
        {
            List<Group> groups = gMemberDB.GetUsersGroup(user);
            return groups != null ? groups.Sum(group => group.GNname[0]*group.GNname.Length) * groups.Count : 0;
        }

    }
}
