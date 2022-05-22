namespace DijkstraShortestPath.Models
{
    public class NodeMap
    {
        private List<Node> _nodes;

        public List<Node> Nodes => _nodes;

        public NodeMap()
        {
            _nodes = new();
        }

        public NodeMap AddNode(Node newNode)
        {
            if (_nodes.Any(x => x.Hash == newNode.Hash))
                throw new InvalidOperationException($"This node has already been added to the node map.");

            _nodes.Add(newNode);
            return this;
        }
    }
}

