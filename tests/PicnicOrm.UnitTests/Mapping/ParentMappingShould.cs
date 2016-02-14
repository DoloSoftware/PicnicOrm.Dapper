using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using PicnicOrm.Data;
using PicnicOrm.Mapping;
using PicnicOrm.TestModels;

namespace PicnicOrm.UnitTests.Mapping
{
    [TestClass]
    public class ParentMappingShould
    {
        #region Properties

        public Mock<IChildMapping<ParentItem>> MockChildMapping { get; set; }

        public Mock<IGridReader> MockGridReader { get; set; }

        public ParentMapping<ParentItem> ParentMapping { get; private set; }

        #endregion

        #region Public Methods

        [TestInitialize]
        public void Initialize()
        {
            MockChildMapping = new Mock<IChildMapping<ParentItem>>();
            MockGridReader = new Mock<IGridReader>();
            ParentMapping = new ParentMapping<ParentItem>(parentItem => parentItem.Id);
        }

        [TestMethod]
        public void Read_GridReaderReturnsEmptyList_ReturnsEmptyList()
        {
            //Arrange
            MockGridReader.Setup(gridReader => gridReader.Read<ParentItem>()).Returns(new List<ParentItem>());

            //Act
            var results = ParentMapping.Read(MockGridReader.Object);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        public void Read_GridReaderReturnsNull_ReturnsEmptyList()
        {
            //Arrange
            List<ParentItem> list = null;
            MockGridReader.Setup(gridReader => gridReader.Read<ParentItem>()).Returns(list);

            //Act
            var results = ParentMapping.Read(MockGridReader.Object);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        public void Read_HasNoResults_DoesNotMapChildren()
        {
            //Arrange
            List<ParentItem> list = null;
            MockGridReader.Setup(gridReader => gridReader.Read<ParentItem>()).Returns(list);
            ParentMapping.AddMapping(MockChildMapping.Object);

            //Act
            ParentMapping.Read(MockGridReader.Object);

            //Assert
            MockChildMapping.Verify(childMapping => childMapping.Map(It.IsAny<IGridReader>(), It.IsAny<IDictionary<int, ParentItem>>(), It.IsAny<bool>()), Times.Never);
        }

        [TestMethod]
        public void Read_HasResultsAndHasChildMappings_MapsChildren()
        {
            //Arrange
            var parentList = GetParentListWithSingleItem();
            MockGridReader.Setup(gridReader => gridReader.Read<ParentItem>()).Returns(parentList);
            ParentMapping.AddMapping(MockChildMapping.Object);

            //Act
            var results = ParentMapping.Read(MockGridReader.Object);

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count());
            MockChildMapping.Verify(childMapping => childMapping.Map(MockGridReader.Object, It.IsAny<IDictionary<int, ParentItem>>(), true), Times.Once);
        }

        #endregion

        #region Private Methods

        private List<ParentItem> GetParentListWithSingleItem()
        {
            var parentItem = new ParentItem();
            var parentList = new List<ParentItem> { parentItem };

            return parentList;
        }

        #endregion
    }
}