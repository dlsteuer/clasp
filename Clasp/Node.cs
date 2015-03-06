using System.Collections.Generic;

namespace Clasp
{
    internal class Node
    {
        public virtual List<Node> ChildNodes { get; private set; }

        protected Node()
        {
            
        }

        public Node(List<Node> childNodes)
        {
            ChildNodes = childNodes;
        }

        public virtual bool IsLeaf 
        {
            get { return false; }
        }
    }
}