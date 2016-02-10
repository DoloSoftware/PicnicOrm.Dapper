using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicnicOrm.Dapper.UnitTests
{
    public class ManyToManyLinker
    {
        public int ParentId { get; set; }

        public int ChildId { get; set; }
    }
}