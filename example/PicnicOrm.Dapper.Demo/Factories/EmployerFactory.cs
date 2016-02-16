using System.Data;

using PicnicOrm.Ado.Factories;
using PicnicOrm.Dapper.Demo.Models;

namespace PicnicOrm.Dapper.Demo.Factories
{
    public class EmployerFactory : IEntityFactory<Employer>
    {
        #region Interfaces

        public Employer Create(DataRow dataRow)
        {
            return new Employer
                   {
                       Id = (int)dataRow["Id"],
                       Name = dataRow["Name"].ToString(),
                       EmployeeCount = (int)dataRow["EmployeeCount"],
                       Sector = (Sector)dataRow["Sector"],
                       AddressId = (int)dataRow["AddressId"]
                   };
        }

        #endregion
    }
}