using System.Collections.Generic;
using System.Linq;

namespace Clasp
{
    internal class SyntaxTree
    {
        public Node Root { get; private set; }

        public static SyntaxTree Parse(string input)
        {
            return new SyntaxTree
            {
                Root = new Node(ParseList(new TokenStream(input)))
            };
        }

        private static List<Node> ParseList(TokenStream tokens)
        {
            var nodes = new List<Node>();
            while (true)
            {
                var token = tokens.GetNext();
                if (token == null || token.Type == TokenType.RightParen)
                    break;
                nodes.Add(token.Type == TokenType.LeftParen ? new Node(ParseList(tokens)) : new Leaf(token.Symbol));
            }
            return nodes;
        }

        public static implicit operator string(SyntaxTree tree)
        {
            return BuildString(tree.Root);
        }

        private static string BuildString(Node node)
        {
            var values = node.ChildNodes.Select(child => child.IsLeaf ? ((Leaf) child).Symbol : BuildString(child)).ToList();
            return "(" + string.Join(" ", values) + ")";
        }
    }
}