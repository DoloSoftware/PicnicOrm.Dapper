using System.Data;

using PicnicOrm.Ado.Factories;
using PicnicOrm.Dapper.Demo.Models;

namespace PicnicOrm.Dapper.Demo.Factories
{
    public class CarFactory : IEntityFactory<Car>
    {
        #region Interfaces

        public Car Create(DataRow dataRow)
        {
            return new Car { Id = (int)dataRow["Id"], MakeModel = (MakeModel)dataRow["MakeModel"], Year = (int)dataRow["Year"] };
        }

        #endregion
    }
}