using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper
{
    public class Post:BaseEntity
    {
        public string PostTitle { get; set; }
        public string PostIcon { get; set; }
        public string PostType { get; set; }
        public string PostContent { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateUser { get; set; }
        public DateTime EditTime { get; set; }
        public int EditUser { get; set; }
        public DateTime LastReplyTime { get; set; }
        public int LastReplyUser { get; set; }
        public User CreateUserInfo{get;set;}
        public User EditUserInfo { get; set; }
        public User LastReplyUserInfo { get; set; }
    }
}
