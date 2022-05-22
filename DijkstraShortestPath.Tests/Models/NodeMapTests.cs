using DijkstraShortestPath.Models;
using System;
using Xunit;

namespace DijkstraShortestPath.Tests.Models
{
    public class NodeMapTests
    {
        [Fact]
        public void AddNode_Called_AddsNode()
        {
            // Arrange
            var nodeMap = new NodeMap();

            // Act
            nodeMap.AddNode(new Node("TEST"));

            // Assert
            Assert.Single(nodeMap.Nodes);
        }

        [Fact]
        public void AddNode_NodeWithSameNameAddedButDifferentHash_AddsNode()
        {
            // Arrange
            var nodeMap = new NodeMap();
            var node = new Node("TEST");
            nodeMap.AddNode(node);

            var copyNode = new Node("TEST");

            // Act
            nodeMap.AddNode(copyNode);

            // Assert
            Assert.Equal(2, nodeMap.Nodes.Count);
        }

        [Fact]
        public void AddNode_NodeAlreadyAdded_ThrowsInvalidOperationException()
        {
            // Arrange
            var nodeMap = new NodeMap();
            var node = new Node("TEST");
            nodeMap.AddNode(node);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => nodeMap.AddNode(node));
        }
    }
}

