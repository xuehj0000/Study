﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper
{
    public class Users : BaseEntity
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public int UserLevel { get; set; }
        public string Password { get; set; }
    }
}
