using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicnicOrm.Dapper.Demo.Models
{
    public class Employer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int EmployeeCount { get; set; }

        public Sector Sector { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

    }
}
