using Castle.Components.DictionaryAdapter.Xml;
using NUnit.Framework;
using UnityEngine;

namespace AStar.UnitTests
{
    public class MatrixGraphUnitTests
    {
        [Test]
        public void MatrixGraphUnitTests_ShouldGetTransition()
        {
            // Arrange
            var graph = new MatrixFullGraph(1, 2, 1f);

            // Act
            var transition = graph.GetTransition(new Vector2Int(0, 0), new Vector2Int(1, 0));
            
            // Assert
            Assert.AreEqual(1f, transition);
        }
        
        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(1, 0)]
        public void MatrixGraphUnitTests_ShouldGetTransitionInCorners(int x, int y)
        {
            // Arrange
            var graph = new MatrixFullGraph(2, 2, 1f);

            // Act
            var transition = graph.GetTransition(new Vector2Int(0, 0), new Vector2Int(x, y));
            
            // Assert
            Assert.AreEqual(1f, transition);
        }
        
        [Test]
        public void MatrixGraphUnitTests_ShouldSetTransition()
        {
            // Arrange
            var graph = new MatrixFullGraph(1, 2, 1f);

            // Act
            graph.SetTransition(new Vector2Int(0, 0), new Vector2Int(1, 0), 0f);
            
            // Assert
            Assert.AreEqual(0f, graph.GetTransition(new Vector2Int(0, 0), new Vector2Int(1, 0)));
        }
    }
}