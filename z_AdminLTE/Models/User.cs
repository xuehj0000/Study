﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace z_AdminLTE
{
    public class User
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
