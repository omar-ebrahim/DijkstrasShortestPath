using DijkstraShortestPath.Models;

namespace DijkstraShortestPath
{
    public class ShortestPathFinder
    {
        private readonly NodeMap _nodeMap;
        private readonly List<Node> _checkedNodes;
        private int _distance;

        public ShortestPathFinder(NodeMap nodeMap)
        {
            _nodeMap = nodeMap;
            _checkedNodes = new();
        }

        public (int TotalDistance, Dictionary<Node, int> NodeJourney, Node StartNode, Node EndNode) GetShortestPathBetween(Node start, Node end)
        {
            var startNode = _nodeMap.Nodes.SingleOrDefault(x => x.Name == start.Name);

            if (startNode == null)
            {
                return (0, new Dictionary<Node, int>(), start, end);
            }
            else if (start.Hash == end.Hash) // we haven't gone anywhere but we have reached the end node.
            {
                return (0, new Dictionary<Node, int> { { start, 0 } }, start, end);
            }

            _checkedNodes.Add(startNode);

            Dictionary<Node, int> shortestPath = new();

            // find the next node with the shortest path. If there are no next nodes, then we just return.
            KeyValuePair<Node, int> current = new(startNode, 0);

            var endFound = false;

            while (_checkedNodes.Count <= _nodeMap.Nodes.Count || endFound)
            {

                if (!current.Key.RelatedNodes.Any() && !endFound) return (0, new Dictionary<Node, int>(), start, end);
                else if (!current.Key.RelatedNodes.Any()) return (_distance, shortestPath, start, end);

                current = current.Key.RelatedNodes.OrderBy(x => x.Value).First();

                _distance += current.Value;
                shortestPath.Add(current.Key, current.Value);

                _checkedNodes.AddRange(current.Key.RelatedNodes.Select(x => x.Key));

                if (current.Key.Hash == end.Hash)
                {
                    endFound = true;
                }
            }

            return endFound ? (_distance, shortestPath, start, end) : (0, new Dictionary<Node, int>(), start, end);
        }
    }
}

