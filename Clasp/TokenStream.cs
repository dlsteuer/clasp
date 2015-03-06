using System.Collections.Generic;

namespace Clasp
{
    internal class TokenStream
    {
        private readonly Queue<char> _input;

        public TokenStream(string input)
        {
            _input = new Queue<char>(input);
        }

        public Token GetNext()
        {
            if (_input.Count == 0)
                return null;

            if (_input.Peek() == '(')
            {
                _input.Dequeue();
                return new Token("(", TokenType.LeftParen);
            }
            if (_input.Peek() == ')')
            {
                _input.Dequeue();
                return new Token(")", TokenType.LeftParen);
            }

            while (_input.Peek() == ' ' || _input.Peek() == '(' || _input.Peek() == ')')
                _input.Dequeue();

            var symbol = "";
            while (_input.Peek() != ' ' && _input.Peek() != '(' && _input.Peek() != ')')
                symbol += _input.Dequeue();
            return new Token(symbol, TokenType.Symbol);
        }
    }
}