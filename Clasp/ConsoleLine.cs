using System;
using System.Collections.Generic;
using System.Linq;

namespace Clasp
{
    internal class ConsoleLine
    {
        private readonly List<char> _input = new List<char>();
        private int _caretPosition;

        public ConsoleLine()
        {
            _caretPosition = 0;
        }

        public ConsoleLine(string str)
        {
            _input = str.ToList();
            _caretPosition = str.Length;
        }

        public int Length
        {
            get { return _input.Count; }
        }

        public int CaretPosition
        {
            get { return _caretPosition; }
        }

        private void Add(char c)
        {
            _input.Insert(_caretPosition, c);
            _caretPosition++;
        }

        public void RemoveLastChar()
        {
            _caretPosition--;
            _input.RemoveAt(_caretPosition);
        }

        public override string ToString()
        {
            return new string(_input.ToArray());
        }

        public static ConsoleLine operator +(ConsoleLine cl, char c)
        {
            cl.Add(c);
            return cl;
        }

        public static implicit operator string(ConsoleLine line)
        {
            return line.ToString();
        }

        public void MoveCaretLeft()
        {
            _caretPosition--;
            if (_caretPosition < 0)
                _caretPosition = 0;
        }

        public void MoveCaretRight()
        {
            _caretPosition++;
            if (_caretPosition > _input.Count)
                _caretPosition = _input.Count;
        }
    }
}