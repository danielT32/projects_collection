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
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        void DeleteComment(Comment entity);
        [OperationContract]
        void InsertComment(Comment entity);
        [OperationContract]
        void UpdateComment(Comment entity);
        [OperationContract]
        List<Comment> SelectCommentByPost(Post post);
        [OperationContract]
        User InsertUser(User entity);
        [OperationContract]
        User Login(string userName, string password);
        [OperationContract]
        User SelectByUserID(int ID);
        [OperationContract]
        List<User> SelectUserByUName(string Uname);
        [OperationContract]
        void UpdateUser(User user);
        [OperationContract]
        void DeleteUser(string userName, string password);
        [OperationContract]
        User SignUp(User user);
        [OperationContract]
        Follow InsertFollow(Follow follow);
        [OperationContract]
        void DeleteFollow(Follow follow);
        [OperationContract]
        List<Follow> SelectFollowed(User follower);
        [OperationContract]
        bool IsFollowing(Follow follower);
        [OperationContract]
        Friend InsertFriend(Friend friend);
        [OperationContract]
        void DeleteFriend(Friend friend);
        [OperationContract]
        bool AreFriends(Friend friend);
        [OperationContract]
        Friend SelectFriend(User user1, User user2);
        [OperationContract]
        List<Friend> SelectRecivedReq(User user);
        [OperationContract]
        List<Friend> SelectUserFriends(User user);
        [OperationContract]
        void ApproveFriend(Friend friend);
        [OperationContract]
        Like InsertLike(Like like);
        [OperationContract]
        void DeleteLike(Like like);
        [OperationContract]
        int CountLikes(Post post);
        [OperationContract]
        bool IsLiked(Like like);
        [OperationContract]
        Post InsertPost(Post post);
        [OperationContract]
        void DeletePost(Post post);
        [OperationContract]
        void UpdatePost(Post post);
        [OperationContract]
        List<Post> SelectUserFeed(User mainUser, User req);
        [OperationContract]
        List<Post> SelectMyFeed(User user);
        [OperationContract]
        List<Post> SelectPostsByUser(User user);
        [OperationContract]
        List<Post> SelectPostsByUID(int ID);
        [OperationContract]
        Post SelectPostByID(int id);
        [OperationContract]
        void InsertDM(DMessage dMessage);
        [OperationContract]
        List<User> GetAllDMChats(User main);
        [OperationContract]
        List<DMessage> SelectAllDMsChat(User main, User chatted);
        [OperationContract]
        bool AreChatting(User main, User chatted);
        [OperationContract]
        List<DMessage> SelectUnSeenDM(User main, User chatted);
        [OperationContract]
        int GetChatsDMUpdateState(User main);

        [OperationContract]
        Group GetGroupByID(int groupID);
        [OperationContract]
        List<Group> GetGroupsByName(string name);
        [OperationContract]
        void InsertGroup(Group group, User creator);
        [OperationContract]
        void UpdateGroup(Group group);
        [OperationContract]
        void DeleteGroup(Group group);
        [OperationContract]
        GMember InsertGMemeber(Group group, User user);
        [OperationContract]
        void DeleteGMember(GMember member);
        [OperationContract]
        void UpdateGMemberStatus(GMember member);
        [OperationContract]
        GMemberList GetGroupMembers(Group group);
        [OperationContract]
        List<User> SelectGroupAdmins(Group group);
        [OperationContract]
        List<User> SelectGroupNonAdmins(Group group);
        [OperationContract]
        bool IsGroupAdmin(GMember member);
        [OperationContract]
        bool IsGroupMember(GMember member);
        [OperationContract]
        GMessage InsertGMessage(GMessage message);
        [OperationContract]
        void DeleteGMessage(GMessage message);
        [OperationContract]
        void UpdateGMessage(GMessage message);
        [OperationContract]
        List<GMessage> SelectGMessages(User user, Group group);
        [OperationContract]
        List<GMessage> GetGMessages(User user, Group group);
        [OperationContract]
        List<GMessage> GetUnSeenGMessages(User user, Group group);

        [OperationContract]
        List<Group> GetMyGroups(User user);
        [OperationContract]
        int GetMyGroupsUpdateState(User user);
        [OperationContract]
        List<Group> SearchPublicGroups(string groupName);


    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfServiceWCF.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
