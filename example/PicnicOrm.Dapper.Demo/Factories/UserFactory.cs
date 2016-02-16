using System;
using System.Data;

using PicnicOrm.Ado.Factories;
using PicnicOrm.Dapper.Demo.Models;

namespace PicnicOrm.Dapper.Demo.Factories
{
    public class UserFactory : IEntityFactory<User>
    {
        #region Interfaces

        public User Create(DataRow dataRow)
        {
            return new User
                   {
                       Id = (int)dataRow["Id"],
                       Name = dataRow["Name"].ToString(),
                       BirthDate = DateTime.Parse(dataRow["BirthDate"].ToString()),
                       AddressId = (int)dataRow["AddressId"],
                       EmployerId = (int)dataRow["EmployerId"]
                   };
        }

        #endregion
    }
}