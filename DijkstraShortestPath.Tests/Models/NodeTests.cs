using System;
using DijkstraShortestPath.Models;
using Xunit;

namespace DijkstraShortestPath.Tests.Models
{
    public class NodeTests
    {
        public NodeTests()
        {
        }

        [Theory, InlineData(""), InlineData(" "), InlineData(null)]
        public void Constructor_NameMissing_ThrowsArgumentNullException(string input)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Node(input));
        }

        [Fact]
        public void Constructor_NameNotMissing_SetsName()
        {
            // Arrange & Act
            var result = new Node("TEST");

            // Assert
            Assert.Equal("TEST", result.Name);
        }

        [Fact]
        public void AddRelatedNode_Called_AddsNode()
        {
            // Arrange
            var node1 = new Node("1");
            var node2 = new Node("2");

            // Act
            node1.AddRelatedNode(node2, 0);

            // Assert
            Assert.Single(node1.RelatedNodes);
        }

        [Fact]
        public void AddRelatedNode_SameRelatedNodeAddedMoreThanOnce_ThrowsInvalidOperationException()
        {
            // Arrange
            var node1 = new Node("1");
            var node2 = new Node("1");
            node1.AddRelatedNode(node2, 0);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => node1.AddRelatedNode(node2, 0));
        }
    }
}

