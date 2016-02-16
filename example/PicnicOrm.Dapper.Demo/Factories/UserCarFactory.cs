using System.Data;

using PicnicOrm.Ado.Factories;
using PicnicOrm.Dapper.Demo.Models;

namespace PicnicOrm.Dapper.Demo.Factories
{
    public class UserCarFactory : IEntityFactory<UserCar>
    {
        #region Interfaces

        public UserCar Create(DataRow dataRow)
        {
            return new UserCar { CarId = (int)dataRow["CarId"], UserId = (int)dataRow["UserId"] };
        }

        #endregion
    }
}