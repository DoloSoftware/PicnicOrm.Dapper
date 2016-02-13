using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PicnicOrm.Dapper.Factories;

namespace PicnicOrm.Dapper.UnitTests.Factories
{
    [TestClass]
    public class SqlConnectionFactoryShould
    {
        #region Test Methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_PassNullParameter_ThrowsException()
        {
            //Arrange
            string parameter = null;
            var factory = new SqlConnectionFactory();

            //Act
            var result = factory.Create(parameter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_PassEmptyStringParameter_ThrowsException()
        {
            //Arrange
            var parameter = string.Empty;
            var factory = new SqlConnectionFactory();

            //Act
            var result = factory.Create(parameter);
        }

        [TestMethod]
        public void Create_PassValidConnectionString_ThrowsException()
        {
            //Arrange
            var parameter = @"Server=(localdb)\ProjectsV12;Database=DemoDatabase;Integrated security=True";
            var factory = new SqlConnectionFactory();

            //Act
            var result = factory.Create(parameter);

            //Assert
            Assert.IsNotNull(result);
        }

        #endregion
    }
}