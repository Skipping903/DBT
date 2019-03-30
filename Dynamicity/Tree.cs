using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DBTMod.Dynamicity
{
    public class Tree<T> where T : IHasParents<T>
    {
        private readonly Dictionary<T, Node<T>> _nodes = new Dictionary<T, Node<T>>();

        public Tree(List<T> items)
        {
            Items = items;

            foreach (T item in Items)
            {
                if (_nodes.ContainsKey(item)) continue;

                _nodes.Add(item, new Node<T>(GetAllParentNodes(_nodes, item).ToArray(), item));
            }

            foreach (T item in Items)
                foreach (T parent in item.Parents)
                    _nodes[parent].AddChild(_nodes[item]);


            Dictionary<T, Node<T>> rootedNodes = new Dictionary<T, Node<T>>();

            foreach (KeyValuePair<T, Node<T>> kvp in Nodes)
            {
                if (kvp.Key.Parents.Length > 0) continue;
                rootedNodes.Add(kvp.Key, kvp.Value);
            }

            RootedNodes = new ReadOnlyDictionary<T, Node<T>>(rootedNodes);
        }

        private static List<Node<T>> GetAllParentNodes(Dictionary<T, Node<T>> nodes, T item)
        {
            List<Node<T>> parentNodes = new List<Node<T>>();

            for (int i = 0; i < item.Parents.Length; i++)
                parentNodes.Add(nodes[item.Parents[i]]);

            return parentNodes;
        }

        public List<T> Items { get; }

        public ReadOnlyDictionary<T, Node<T>> Nodes => new ReadOnlyDictionary<T, Node<T>>(_nodes);

        public ReadOnlyDictionary<T, Node<T>> RootedNodes { get; private set; }
    }
}
