using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace AStar.UnitTests
{
    /// <summary>
    ///     Test for <see cref="AStarPathFindService"/> class
    /// </summary>
    public class AStarPathFindServiceUnitTests
    {
        [Test]
        public void AStarPathFindServiceUnitTests_ShouldFindOncePath()
        {
            // Arrange
            var start = new Vector2Int(0, 0);
            var end = new Vector2Int(1, 0);
            var options = new PathFindingOptions()
            {
                Start = start,
                End = end
            };
            
            var graphMock = new Mock<IGraph>();
            graphMock.Setup(g => g.GetTransition(end, start)).Returns(0);
            
            var neighborService = new Mock<IGraphNeighborsService>();
            neighborService.Setup(n => n.GetNeighbors(graphMock.Object, end, It.IsAny<IList<Vector2Int>>()))
                .Callback((IGraph _g, Vector2Int _v, IList<Vector2Int> res) =>
                {
                    res.Clear();
                    res.Add(new Vector2Int(0, 0));
                });
            
            var astar_finder = new AStarPathFindService(neighborService.Object);
            
            // Act
            var result = astar_finder.Find(graphMock.Object, options);
            
            // Assert
            var expected = new GraphPath()
            {
                Path = new List<Vector2Int>() {start, end}
            };
            Assert.True(Enumerable.SequenceEqual(expected.Path, result.Path));
        }
        
        [Test]
        public void AStarPathFindServiceUnitTests_ShouldCantFindPath()
        {
            // Arrange
            var start = new Vector2Int(0, 0);
            var end = new Vector2Int(1, 0);
            var options = new PathFindingOptions()
            {
                Start = start,
                End = end
            };
            
            var graphMock = new Mock<IGraph>();
            graphMock.Setup(g => g.GetTransition(end, start)).Returns(0);
            
            var neighborService = new Mock<IGraphNeighborsService>();
            neighborService.Setup(n => n.GetNeighbors(graphMock.Object, end, It.IsAny<IList<Vector2Int>>()))
                .Callback((IGraph _g, Vector2Int _v, IList<Vector2Int> res) =>
                {
                    res.Clear();
                });
            
            var astar_finder = new AStarPathFindService(neighborService.Object);
            
            // Act
            var result = astar_finder.Find(graphMock.Object, options);
            
            // Assert
            Assert.Null(result);
        }
        
        [Test]
        public void AStarPathFindServiceUnitTests_ShouldFindOptimalPath()
        {
            // Arrange
            var start = new Vector2Int(0, 0);
            var end = new Vector2Int(1, 1);
            var options = new PathFindingOptions()
            {
                Start = start,
                End = end
            };
            
            var graphMock = new Mock<IGraph>();
            graphMock.Setup(g => g.GetTransition(end, start)).Returns(1);
            graphMock.Setup(g => g.GetTransition(end, new Vector2Int(1, 0))).Returns(1);
            graphMock.Setup(g => g.GetTransition(end, new Vector2Int(0, 1))).Returns(0);
            graphMock.Setup(g => g.GetTransition(new Vector2Int(0, 1), start)).Returns(0);
            graphMock.Setup(g => g.GetTransition(new Vector2Int(1, 0), start)).Returns(1);
            
            var neighborService = new Mock<IGraphNeighborsService>();
            neighborService.Setup(n => n.GetNeighbors(graphMock.Object, end, It.IsAny<IList<Vector2Int>>()))
                .Callback((IGraph _g, Vector2Int _v, IList<Vector2Int> res) =>
                {
                    res.Clear();
                    res.Add(new Vector2Int(1, 0));
                    res.Add(new Vector2Int(0, 1));
                    res.Add(new Vector2Int(0, 0));
                });
            
            neighborService.Setup(n => n.GetNeighbors(graphMock.Object, new Vector2Int(0, 1), It.IsAny<IList<Vector2Int>>()))
                .Callback((IGraph _g, Vector2Int _v, IList<Vector2Int> res) =>
                {
                    res.Clear();
                    res.Add(new Vector2Int(1, 1));
                    res.Add(new Vector2Int(1, 0));
                    res.Add(new Vector2Int(0, 0));
                });
            
            var astar_finder = new AStarPathFindService(neighborService.Object);
            
            // Act
            var result = astar_finder.Find(graphMock.Object, options);
            
            // Assert
            var expected = new GraphPath()
            {
                Path = new List<Vector2Int>() {start, new Vector2Int(0, 1), end}
            };
            Assert.True(Enumerable.SequenceEqual(expected.Path, result.Path));
            neighborService.Verify(
                n => n.GetNeighbors(It.IsAny<IGraph>(), It.IsAny<Vector2Int>(), It.IsAny<IList<Vector2Int>>()),
                Times.Exactly(2));
        }
        
        [Test]
        public void AStarPathFindServiceUnitTests_ShouldThrowIfGraphIsNull()
        {
            // Arrange
            var astar_finder = new AStarPathFindService(Mock.Of<IGraphNeighborsService>());
            
            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => astar_finder.Find(null, null));
        }
        
        [Test]
        public void AStarPathFindServiceUnitTests_ShouldThrowIfOptionsIsNull()
        {
            // Arrange
            var astar_finder = new AStarPathFindService(Mock.Of<IGraphNeighborsService>());
            
            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => astar_finder.Find(Mock.Of<IGraph>(), null));
        }
    }
}