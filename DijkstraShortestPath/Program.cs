using DijkstraShortestPath;
using DijkstraShortestPath.Models;

/*
 * 
 * Here we are going to get the shortest path between a start and end node.
 * 
 * 1. All nodes are connected
 * 2. The path between start and end is possible
 * 
 */

// Let's create the nodes and build the node map
Node startingNode = new("START");
Node nodeA = new("A");
Node nodeB = new("B");
Node endingNode = new("END");

startingNode.AddRelatedNode(nodeA, 6).AddRelatedNode(nodeB, 2);
nodeA.AddRelatedNode(endingNode, 1);
nodeB.AddRelatedNode(nodeA, 3).AddRelatedNode(endingNode, 5);

NodeMap nodeMap = new();

nodeMap.AddNode(startingNode).AddNode(nodeA).AddNode(nodeB).AddNode(endingNode);

// Here, we assume that we can get to the end and there is definitely a path to the end node

var finder = new ShortestPathFinder(nodeMap);

var (TotalDistance, NodeJourney, StartNode, EndNode) = finder.GetShortestPathBetween(startingNode, endingNode);

if (TotalDistance == 0 && NodeJourney.Count == 0)
{
    Console.WriteLine($"Cannot find the path between '{StartNode.Name}' and '{EndNode.Name}'");
}
else if (TotalDistance == 0 && NodeJourney.Any())
{
    Console.WriteLine($"Path between '{StartNode.Name}' and '{EndNode.Name}' is 0.");
}
else
{
    Console.WriteLine($"Shortest path between '{StartNode.Name}' and '{EndNode.Name}' is '{StartNode.Name}' ==> {string.Join(" ==> ", NodeJourney.Select(x => $"'{x.Key.Name}' ({x.Value})"))} ==> {EndNode.Name}. Total distance: {TotalDistance}");
}
