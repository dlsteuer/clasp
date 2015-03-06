using System.Collections.Generic;

namespace Clasp
{
    internal class Leaf : Node
    {
        public string Symbol { get; private set; }

        public Leaf(string symbol)
        {
            Symbol = symbol;
        }

        public override List<Node> ChildNodes
        {
            get { return new List<Node>(); }
        }

        public override bool IsLeaf
        {
            get { return true; }
        }
    }
}