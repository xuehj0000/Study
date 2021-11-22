using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    public class User : BaseEntity
    {
        [VRequired]
        [VLength(2,4)]
        public string Name { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        [VIntLimit(1,2,4,8)]
        public int Status { get; set; }

        public int UserType { get; set; }

        public DateTime LastLoginTime { get; set; }
        
        public DateTime CreateTime { get; set; }
    }
}
