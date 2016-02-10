using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicnicOrm.Dapper.UnitTests
{
    public class ManyToManyItem
    {
        public int Id { get; set; }

        public IList<ParentItem> Parents { get; set; }
    }
}