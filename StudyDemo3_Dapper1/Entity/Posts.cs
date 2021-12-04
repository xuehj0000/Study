using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper
{
    [Table("Posts")]
    public class Posts:BaseEntity
    {
        public string PostTitle { get; set; }
        public string PostIcon { get; set; }
        public string PostType { get; set; }
        public string PostContent { get; set; }
        public int Clicks { get; set; }
        public int Replys { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateUser { get; set; }
        public DateTime EditTime { get; set; }
        public int EditUser { get; set; }
        public DateTime LastReplyTime { get; set; }
        public int LastReplyUser { get; set; }
        public Users CreateUserInfo{get;set;}
        public Users EditUserInfo { get; set; }
        public Users LastReplyUserInfo { get; set; }
    }
}
