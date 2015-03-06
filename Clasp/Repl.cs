using System;

namespace Clasp
{
    internal class Repl
    {
        private SyntaxTree Read(string input)
        {
            return SyntaxTree.Parse(input);
        }

        private SyntaxTree Eval(SyntaxTree input)
        {
            return input;
        }

        private void Print(SyntaxTree input)
        {
            Console.WriteLine(input);
        }

        public void Run(string input)
        {
            Print(Eval(Read(input)));
        }
    }
}