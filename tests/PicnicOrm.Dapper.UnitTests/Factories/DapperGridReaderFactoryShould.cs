using System.Collections.Generic;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PicnicOrm.Dapper.Factories;
using PicnicOrm.Data;

namespace PicnicOrm.Dapper.UnitTests.Factories
{
    [TestClass]
    public class DapperGridReaderFactoryShould
    {
        #region Properties

        public DapperGridReaderFactory GridReaderFactory { get; set; }

        #endregion

        #region Public Methods

        [TestMethod]
        public void ConvertToDynamicParameters_PassEmptyList_ReturnsNull()
        {
            //Arrange
            var parameters = new List<IDbParameter>();

            //Act
            var result = GridReaderFactory.ConvertToDynamicParameters(parameters);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertToDynamicParameters_PassNull_ReturnsNull()
        {
            //Act
            var result = GridReaderFactory.ConvertToDynamicParameters(null);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertToDynamicParameters_PassParametersAllScenarios_ReturnsDynamicParameters()
        {
            //Arrange
            var smallParameter = new DbParameter("TestName", "TestValue");
            var dbTypeParameter = new DbParameter("TestDbType", "TestValue", DbType.String);
            var directionParameter = new DbParameter("TestDirection", "TestAnotherValue", direction: ParameterDirection.Input);
            var sizeParameter = new DbParameter("TestSize", "TestValue", size: 5);
            var precisionParameter = new DbParameter("TestPrecision", "TestValue", precision: 1);
            var scaleParameter = new DbParameter("TestScale", "TestValue", scale: 1);
            var parameters = new List<IDbParameter> { smallParameter, dbTypeParameter, directionParameter, sizeParameter, precisionParameter, scaleParameter };

            //Act
            var result = GridReaderFactory.ConvertToDynamicParameters(parameters);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestInitialize]
        public void Initialize()
        {
            GridReaderFactory = new DapperGridReaderFactory();
        }

        #endregion
    }
}