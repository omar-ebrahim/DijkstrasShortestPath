namespace DijkstraShortestPath.Models
{
    public class Node
    {
        public Guid Hash { get; }

        private readonly Dictionary<Node, int> _relatedNodes;

        public Dictionary<Node, int> RelatedNodes => _relatedNodes;

        public Node(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            Name = name;
            _relatedNodes = new();
            Hash = Guid.NewGuid();
        }

        public string Name { get; set; }

        public Node AddRelatedNode(Node node, int distance)
        {
            if (_relatedNodes.Any(x => x.Key.Hash == node.Hash))
                throw new InvalidOperationException("This node has already been added to the related nodes");

            _relatedNodes.Add(node, distance);
            return this;
        }
    }
}

