using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using PicnicOrm.Data;
using PicnicOrm.Mapping;
using PicnicOrm.TestModels;

namespace PicnicOrm.UnitTests.Mapping
{
    [TestClass]
    public class OneToOneMappingShould
    {
        #region Properties

        public Mock<IChildMapping<OneToOneItem>> MockChildMapping { get; set; }

        public Mock<IGridReader> MockGridReader { get; set; }

        public OneToOneMapping<ParentItem, OneToOneItem> OneToOneMapping { get; set; }

        #endregion

        #region Public Methods

        [TestInitialize]
        public void Initialize()
        {
            MockChildMapping = new Mock<IChildMapping<OneToOneItem>>();
            MockGridReader = new Mock<IGridReader>();

            OneToOneMapping = new OneToOneMapping<ParentItem, OneToOneItem>(child => child.Id,
                parent => parent.ChildId,
                (parent, child) =>
                {
                    parent.Child = child;
                    child.Parent = parent;
                });
        }

        [TestMethod]
        public void Map_HasNoResultsAndShouldContinueThroughEmptyTablesIsFalse_DoesNotMapChildren()
        {
            //Arrange
            var oneToOneItemList = new List<OneToOneItem>();
            MockGridReader.Setup(gridReader => gridReader.Read<OneToOneItem>()).Returns(oneToOneItemList);
            OneToOneMapping.AddMapping(MockChildMapping.Object);
            IDictionary<int, ParentItem> parents = null;

            //Act
            OneToOneMapping.Map(MockGridReader.Object, parents, false);

            //Assert
            MockChildMapping.Verify(childMapping => childMapping.Map(It.IsAny<IGridReader>(), It.IsAny<IDictionary<int, OneToOneItem>>(), It.IsAny<bool>()), Times.Never);
        }

        [TestMethod]
        public void Map_HasNoResultsAndShouldContinueThroughEmptyTablesIsTrue_MapsChildren()
        {
            //Arrange
            var oneToOneItemList = new List<OneToOneItem>();
            MockGridReader.Setup(gridReader => gridReader.Read<OneToOneItem>()).Returns(oneToOneItemList);
            OneToOneMapping.AddMapping(MockChildMapping.Object);
            IDictionary<int, ParentItem> parents = null;

            //Act
            OneToOneMapping.Map(MockGridReader.Object, parents, true);

            //Assert
            MockChildMapping.Verify(childMapping => childMapping.Map(It.IsAny<IGridReader>(), It.IsAny<IDictionary<int, OneToOneItem>>(), It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void Map_HasResultsAndParents_MapsParents()
        {
            //Arrange
            var oneToOneItem = new OneToOneItem { Id = 5 };
            var oneToOneItemList = new List<OneToOneItem> { oneToOneItem };
            MockGridReader.Setup(gridReader => gridReader.Read<OneToOneItem>()).Returns(oneToOneItemList);
            var parent = new ParentItem { ChildId = 5 };
            var parentDictionary = new Dictionary<int, ParentItem> { [1] = parent };

            //Act
            OneToOneMapping.Map(MockGridReader.Object, parentDictionary, true);

            //Assert
            Assert.AreEqual(oneToOneItem, parentDictionary[1].Child);
        }

        [TestMethod]
        public void Map_HasResultsAndShouldContinueThroughEmptyTablesIsFalse_MapsChildren()
        {
            //Arrange
            var oneToOneItem = new OneToOneItem();
            var oneToOneItemList = new List<OneToOneItem> { oneToOneItem };
            MockGridReader.Setup(gridReader => gridReader.Read<OneToOneItem>()).Returns(oneToOneItemList);
            OneToOneMapping.AddMapping(MockChildMapping.Object);
            IDictionary<int, ParentItem> parents = null;

            //Act
            OneToOneMapping.Map(MockGridReader.Object, parents, false);

            //Assert
            MockChildMapping.Verify(childMapping => childMapping.Map(It.IsAny<IGridReader>(), It.IsAny<IDictionary<int, OneToOneItem>>(), It.IsAny<bool>()), Times.Once);
        }

        #endregion
    }
}