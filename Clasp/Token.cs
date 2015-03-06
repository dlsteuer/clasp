namespace Clasp
{
    internal class Token
    {
        public Token(string symbol, TokenType type)
        {
            Symbol = symbol;
            Type = type;
        }

        public TokenType Type { get; private set; }
        public string Symbol { get; private set; }
    }
}