using System;
using System.Linq;
using DijkstraShortestPath.Models;
using Xunit;

namespace DijkstraShortestPath.Tests
{
    public class ShortestPathFinderTests
    {
        public ShortestPathFinderTests()
        {
        }

        [Fact]
        public void GetShortestPathBetween_CannotFindPath_ReturnsZeroAndEmptyEnumerable()
        {
            // Arange
            Node start = new("start");
            Node middle = new("middle");
            Node end = new("end");

            start.AddRelatedNode(middle, 1);

            NodeMap map = new();
            map.AddNode(start).AddNode(end);

            // Act
            var (TotalDistance, NodeJourney, _, _) = new ShortestPathFinder(map).GetShortestPathBetween(start, end);

            // Assert
            Assert.Equal(0, TotalDistance);
            Assert.Empty(NodeJourney);
        }

        [Fact]
        public void GetShortestPathBetween_SameNodeReferenced_ReturnsZeroAndNodeReturned()
        {
            // Arrange
            Node start = new("start");
            start.AddRelatedNode(start, 0);

            NodeMap map = new();
            map.AddNode(start);

            // Act
            var (TotalDistance, NodeJourney, _, _) = new ShortestPathFinder(map).GetShortestPathBetween(start, start);

            // Assert
            Assert.Equal(0, TotalDistance);
            Assert.Single(NodeJourney);
        }

        [Fact]
        public void GetShortestPathBetween_CanFindPath_Returns()
        {
            // Arrange
            Node startingNode = new("START");
            Node nodeA = new("A");
            Node nodeB = new("B");
            Node endingNode = new("END");

            startingNode.AddRelatedNode(nodeA, 6).AddRelatedNode(nodeB, 2);
            nodeA.AddRelatedNode(endingNode, 1);
            nodeB.AddRelatedNode(nodeA, 3).AddRelatedNode(endingNode, 5);

            NodeMap nodeMap = new();

            nodeMap.AddNode(startingNode).AddNode(nodeA).AddNode(nodeB).AddNode(endingNode);

            var finder = new ShortestPathFinder(nodeMap);

            // Act
            var (TotalDistance, NodeJourney, StartNode, EndNode) = finder.GetShortestPathBetween(startingNode, endingNode);

            // Assert
            Assert.Equal(6, TotalDistance);
            Assert.Equal(3, NodeJourney.Count);

            Assert.Equal(nodeB.Hash, NodeJourney.ElementAt(0).Key.Hash);
            Assert.Equal(2, NodeJourney.ElementAt(0).Value);

            Assert.Equal(nodeA.Hash, NodeJourney.ElementAt(1).Key.Hash);
            Assert.Equal(3, NodeJourney.ElementAt(1).Value);

            Assert.Equal(endingNode.Hash, NodeJourney.ElementAt(2).Key.Hash);
            Assert.Equal(1, NodeJourney.ElementAt(2).Value);

            Assert.Equal(startingNode.Hash, StartNode.Hash);
            Assert.Equal(endingNode.Hash, EndNode.Hash);
        }
    }
}

