using System;

namespace Clasp
{
    internal class Repl
    {
        private string Read(string input)
        {
            return input;
        }

        private string Eval(string input)
        {
            return input;
        }

        private void Print(string input)
        {
            Console.WriteLine(input);
        }

        public void Run(string input)
        {
            Print(Eval(Read(input)));
        }
    }
}