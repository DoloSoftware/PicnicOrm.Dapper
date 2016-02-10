using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicnicOrm.Dapper.Demo.Models
{
    public class Car
    {
        public int Id { get; set; }

        public MakeModel MakeModel { get; set; }

        public int Year { get; set; }

        public IList<User> Users { get; set; }
        
        public Car() { Users = new List<User>(); } 
    }
}
