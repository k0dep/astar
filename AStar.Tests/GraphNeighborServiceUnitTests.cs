using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace AStar.UnitTests
{
    /// <summary>
    ///     Unit Test for <see cref="GraphGridNeighborsService"/> class
    /// </summary>
    public class GraphGridNeighborServiceUnitTests
    {
        [Test]
        [TestCase(-1, -1)]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        public void GraphGridNeighborService_ShouldThrowIfCurrentNotInBounds(int x, int y)
        {
            // Arrange
            var node = new Vector2Int(x, y);
            var graphMock = new Mock<IGraph>();
            graphMock.Setup(g => g.Width).Returns(1);
            graphMock.Setup(g => g.Height).Returns(1);

            var neighborService = new GraphGridNeighborsService(1);
            
            // Act/Assert
            Assert.Throws<ArgumentException>(() =>
            {
                neighborService.GetNeighbors(graphMock.Object, node, new List<Vector2Int>());
            });
        }
        
        [Test]
        public void GraphGridNeighborService_ShouldThrowIfListIsNull()
        {
            // Arrange
            var neighborService = new GraphGridNeighborsService(1);
            
            // Act/Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                neighborService.GetNeighbors(null, new Vector2Int(), null);
            });
        }
        
        [Test]
        public void GraphGridNeighborService_ShouldThrowIfGraphIsNull()
        {
            // Arrange
            var neighborService = new GraphGridNeighborsService(1);
            
            // Act/Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                neighborService.GetNeighbors(null, new Vector2Int(), new List<Vector2Int>());
            });
        }
        
        [Test]
        public void GraphGridNeighborService_ShouldFindAllValidFull()
        {
            // Arrange
            var node = new Vector2Int(1, 1);
            var graphMock = new Mock<IGraph>();
            graphMock.Setup(g => g.Width).Returns(3);
            graphMock.Setup(g => g.Height).Returns(3);
            graphMock.Setup(g => g.GetTransition(node, It.IsAny<Vector2Int>())).Returns(0);

            var neighborService = new GraphGridNeighborsService(1);
            
            // Act
            var result = new List<Vector2Int>();
            neighborService.GetNeighbors(graphMock.Object, node, result);
            
            // Assert
            Assert.True(result.Distinct().Count(n => n != node) == 8);
            var distances = result.Select(n => Vector2Int.Distance(n, node)).Distinct().ToArray();
            Assert.True(distances.Count() == 2);
            Assert.True(Math.Abs(distances.Min() - 1) < 0.00001);
            Assert.True(Math.Abs(distances.Max() - Math.Sqrt(2)) < 0.00001);
        }
        
        [Test]
        [TestCase(0, 0)]
        [TestCase(2, 2)]
        [TestCase(0, 2)]
        [TestCase(2, 0)]
        public void GraphGridNeighborService_ShouldFindAllValidInCorner(int x, int y)
        {
            // Arrange
            var node = new Vector2Int(x, y);
            var graphMock = new Mock<IGraph>();
            graphMock.Setup(g => g.Width).Returns(3);
            graphMock.Setup(g => g.Height).Returns(3);
            graphMock.Setup(g => g.GetTransition(node, It.IsAny<Vector2Int>())).Returns(0);

            var neighborService = new GraphGridNeighborsService(1);
            
            // Act
            var result = new List<Vector2Int>();
            neighborService.GetNeighbors(graphMock.Object, node, result);
            
            // Assert
            Assert.True(result.Distinct().Count(n => n != node) == 3);
            var distances = result.Select(n => Vector2Int.Distance(n, node)).Distinct().ToArray();
            Assert.True(distances.Count() == 2);
            Assert.True(Math.Abs(distances.Min() - 1) < 0.00001);
            Assert.True(Math.Abs(distances.Max() - Math.Sqrt(2)) < 0.00001);
        }
        
        [Test]
        public void GraphGridNeighborService_ShouldFindWithDropUnreachable()
        {
            // Arrange
            var node = new Vector2Int(1, 1);
            var graphMock = new Mock<IGraph>();
            graphMock.Setup(g => g.Width).Returns(3);
            graphMock.Setup(g => g.Height).Returns(3);
            graphMock.SetupSequence(g => g.GetTransition(node, It.IsAny<Vector2Int>()))
                .Returns(0)
                .Returns(2)
                .Returns(0)
                .Returns(2)
                .Returns(0)
                .Returns(2)
                .Returns(0)
                .Returns(2);

            var neighborService = new GraphGridNeighborsService(1);
            
            // Act
            var result = new List<Vector2Int>();
            neighborService.GetNeighbors(graphMock.Object, node, result);
            
            // Assert
            Assert.True(result.Distinct().Count(n => n != node) == 4);
            var distances = result.Select(n => Vector2Int.Distance(n, node)).Distinct().ToArray();
            Assert.True(distances.Count() == 1);
            Assert.True(Math.Abs(distances.First() - 1) < 0.00001);
        }
    }
}