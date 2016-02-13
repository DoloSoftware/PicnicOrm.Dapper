using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using PicnicOrm.Dapper.Data;
using PicnicOrm.Dapper.Factories;
using PicnicOrm.Dapper.Mapping;

namespace PicnicOrm.Dapper.UnitTests
{
    [TestClass]
    public class DapperDataBrokerShould
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public DapperDataBroker DapperDataBroker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Mock<IGridReaderFactory> MockGridReaderFactory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Mock<IParentMapping<ParentItem>> MockParentMapping { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Mock<ISqlConnectionFactory> MockSqlConnectionFactory { get; set; }

        #endregion

        #region Public Methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddMapping_AddSameMappingKeyTwice_ThrowsException()
        {
            //Arrange
            DapperDataBroker.AddMapping(3, MockParentMapping.Object);
            
            //Act
            DapperDataBroker.AddMapping(3, new Mock<IParentMapping<OneToOneItem>>().Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void AddMapping_AddSameTypeTwice_ThrowsException()
        {
            //Arrange
            DapperDataBroker.AddMapping(MockParentMapping.Object);

            //Act
            DapperDataBroker.AddMapping(MockParentMapping.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExecuteStoredProcedure_PassInvalidMappingKey_ThrowsException()
        {
            //Arrange
            DapperDataBroker.AddMapping(7, MockParentMapping.Object);

            //Act
            var result = DapperDataBroker.ExecuteStoredProcedure<ParentItem>("FakeProcName", 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExecuteStoredProcedure_PassInvalidKeyType_ThrowsException()
        {
            //Arrange
            DapperDataBroker.AddMapping<ParentItem>(MockParentMapping.Object);

            //Act
            var result = DapperDataBroker.ExecuteStoredProcedure<OneToOneItem>("FakeProcName");
        }

        [TestMethod]
        public void ExecuteStoredProcedure_PassValidConfigKey_UsesCorrectMapping()
        {
            //Arrange
            var mockSecondMapping = new Mock<IParentMapping<OneToOneItem>>();
            DapperDataBroker.AddMapping(3, mockSecondMapping.Object);
            DapperDataBroker.AddMapping(7, MockParentMapping.Object);

            //Act
            var result = DapperDataBroker.ExecuteStoredProcedure<ParentItem>("FakeProcName", 7);

            //Assert
            MockParentMapping.Verify(mapping => mapping.Read(It.IsAny<IGridReader>(), true), Times.Once);
            mockSecondMapping.Verify(mapping => mapping.Read(It.IsAny<IGridReader>(), It.IsAny<bool>()), Times.Never);
        }

        [TestMethod]
        public void ExecuteStoredProcedure_PassValidKeyType_UsesCorrectMapping()
        {
            //Arrange
            var mockSecondMapping = new Mock<IParentMapping<OneToOneItem>>();
            DapperDataBroker.AddMapping<OneToOneItem>(mockSecondMapping.Object);
            DapperDataBroker.AddMapping<ParentItem>(MockParentMapping.Object);

            //Act
            var result = DapperDataBroker.ExecuteStoredProcedure<ParentItem>("FakeProcName");

            //Assert
            MockParentMapping.Verify(mapping => mapping.Read(It.IsAny<IGridReader>(), true), Times.Once);
            mockSecondMapping.Verify(mapping => mapping.Read(It.IsAny<IGridReader>(), It.IsAny<bool>()), Times.Never);
        }

        [TestMethod]
        public void Constructor_OnlyPassConnectionString_DefaultConnectionAndGridReaderFactoryCreated()
        {
            //Act
            var result = new DapperDataBroker("FakeString");

            var privateObject = new PrivateObject(result);
            var connectionFactory = (ISqlConnectionFactory)privateObject.GetField("_sqlConnectionFactory");
            var gridReaderFactory = (IGridReaderFactory)privateObject.GetField("_gridReaderFactory");

            //Assert
            Assert.IsNotNull(connectionFactory);
            Assert.IsNotNull(gridReaderFactory);
        }

        [TestMethod]
        public void Constructor_PassConnectionStringAndGridReaderFactory_DefaultConnectionFactoryIsCreated()
        {
            //Act
            var result = new DapperDataBroker("FakeString", MockGridReaderFactory.Object);

            var privateObject = new PrivateObject(result);
            var connectionFactory = (ISqlConnectionFactory)privateObject.GetField("_sqlConnectionFactory");

            //Assert
            Assert.IsNotNull(connectionFactory);
        }

        [TestMethod]
        public void Constructor_PassConnectionStringAndConnectionFactory_DefaultGridReaderFactoryIsCreated()
        {
            //Act
            var result = new DapperDataBroker("FakeString", MockSqlConnectionFactory.Object);

            var privateObject = new PrivateObject(result);
            var gridReaderFactory = (IGridReaderFactory)privateObject.GetField("_gridReaderFactory");

            //Assert
            Assert.IsNotNull(gridReaderFactory);
        }

        [TestInitialize]
        public void Initialize()
        {
            MockSqlConnectionFactory = new Mock<ISqlConnectionFactory>();
            MockParentMapping = new Mock<IParentMapping<ParentItem>>();
            MockGridReaderFactory = new Mock<IGridReaderFactory>();
            DapperDataBroker = new DapperDataBroker("FakeConnectionString", MockSqlConnectionFactory.Object, MockGridReaderFactory.Object);
        }

        #endregion
    }
}