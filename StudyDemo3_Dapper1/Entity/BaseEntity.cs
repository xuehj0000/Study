using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
