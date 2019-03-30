using System.Collections.Generic;

namespace DBTMod.Dynamicity
{
    public sealed class Node<T> where T : IHasParents<T>
    {
        private readonly List<Node<T>> 
            _children, 
            _allChildren;

        public Node(Node<T>[] parents, T current)
        {
            Parents = parents;
            Current = current;
            
            _children = new List<Node<T>>();
            _allChildren = new List<Node<T>>();
        }

        public void AddChild(Node<T> item)
        {
            for (int i = 0; i < _children.Count; i++)
                if (_children[i] == item)
                    return;

            _children.Add(item);
            RecursizeAddChild(item);
        }

        private void RecursizeAddChild(Node<T> item)
        {
            _allChildren.Add(item);

            for (int i = 0; i < Parents.Length; i++)
                Parents[i].RecursizeAddChild(item);
        }

        public Node<T>[] Parents { get; }

        public T Current { get; }

        public List<Node<T>> Children => _children;
        public List<Node<T>> AllChildren => _allChildren;
    }
}
