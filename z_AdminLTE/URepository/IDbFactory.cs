﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace z_AdminLTE
{
    public interface IDbFactory
    {
        MyDbBase CreateClient(string name);
    }
}
