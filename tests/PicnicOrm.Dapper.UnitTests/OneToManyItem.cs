using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicnicOrm.Dapper.UnitTests
{
    public class OneToManyItem
    {
        public int Id { get; set; }

        public ParentItem Parent { get; set; }

        public int ParentId { get; set; }
    }
}