using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo2_ORM
{
    public class BaseEntity
    {
        [PropIgnoreAttribute]
        public int Id { get; set; }
    }
}
