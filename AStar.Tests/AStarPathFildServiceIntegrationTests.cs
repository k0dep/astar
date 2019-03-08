using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace AStar.UnitTests
{
    public class AStarPathFildServiceIntegrationTests
    {
        [Test]
        public void AStarPathFindServiceUnitTests_ShouldFindOptimalPath_From0To4()
        {
            // Arrange
            var start = new Vector2Int(0, 0);
            var end = new Vector2Int(4, 4);
            var options = new PathFindingOptions()
            {
                Start = start,
                End = end
            };
            
            var graph = new MatrixFullGraph(5, 5, 0);
            var neighborService = new GraphGridNeighborsService(1);
            var astar_finder = new AStarPathFindService(neighborService);
            
            // Act
            var result = astar_finder.Find(graph, options);
            
            // Assert
            var expected = new GraphPath()
            {
                Path = new List<Vector2Int>()
                {
                    start,
                    new Vector2Int(1, 1),
                    new Vector2Int(2, 2),
                    new Vector2Int(3, 3),
                    end
                }
            };
            Assert.True(Enumerable.SequenceEqual(expected.Path, result.Path));
        }
        
        [Test]
        public void AStarPathFindServiceUnitTests_ShouldFindOptimalPath_From40To04()
        {
            // Arrange
            var start = new Vector2Int(4, 0);
            var end = new Vector2Int(0, 4);
            var options = new PathFindingOptions()
            {
                Start = start,
                End = end
            };
            
            var graph = new MatrixFullGraph(5, 5, 0);
            var neighborService = new GraphGridNeighborsService(1);
            var astar_finder = new AStarPathFindService(neighborService);
            
            // Act
            var result = astar_finder.Find(graph, options);
            
            // Assert
            var expected = new GraphPath()
            {
                Path = new List<Vector2Int>()
                {
                    start,
                    new Vector2Int(3, 1),
                    new Vector2Int(2, 2),
                    new Vector2Int(1, 3),
                    end
                }
            };
            Assert.True(Enumerable.SequenceEqual(expected.Path, result.Path));
        }
        
        [Test]
        public void AStarPathFindServiceUnitTests_ShouldFindOptimalPath_From00To04()
        {
            // Arrange
            var start = new Vector2Int(0, 0);
            var end = new Vector2Int(0, 4);
            var options = new PathFindingOptions()
            {
                Start = start,
                End = end
            };
            
            var graph = new MatrixFullGraph(5, 5, 0);
            var neighborService = new GraphGridNeighborsService(1);
            var astar_finder = new AStarPathFindService(neighborService);
            
            // Act
            var result = astar_finder.Find(graph, options);
            
            // Assert
            var expected = new GraphPath()
            {
                Path = new List<Vector2Int>()
                {
                    start,
                    new Vector2Int(0, 1),
                    new Vector2Int(0, 2),
                    new Vector2Int(0, 3),
                    end
                }
            };
            Assert.True(Enumerable.SequenceEqual(expected.Path, result.Path));
        }
        
        [Test]
        public void AStarPathFindServiceUnitTests_ShouldFindOptimalPath_From0To40()
        {
            // Arrange
            var start = new Vector2Int(0, 0);
            var end = new Vector2Int(4, 0);
            var options = new PathFindingOptions()
            {
                Start = start,
                End = end
            };
            
            var graph = new MatrixFullGraph(5, 5, 0);
            var neighborService = new GraphGridNeighborsService(1);
            var astar_finder = new AStarPathFindService(neighborService);
            
            // Act
            var result = astar_finder.Find(graph, options);
            
            // Assert
            var expected = new GraphPath()
            {
                Path = new List<Vector2Int>()
                {
                    start,
                    new Vector2Int(1, 0),
                    new Vector2Int(2, 0),
                    new Vector2Int(3, 0),
                    end
                }
            };
            Assert.True(Enumerable.SequenceEqual(expected.Path, result.Path));
        }
    }
}