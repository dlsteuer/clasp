using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Clasp
{
	public static class Program
	{
	    private static IList<string> _commandHistory = new List<string>();
	    private static int _currentHistoryIndex = -1;
	    private static string _historyFilename;
	    private const string Prompt = "user> ";

	    static void Main(string[] args)
		{
		    AppDomain.CurrentDomain.ProcessExit += SaveCommandHistory;
	        _historyFilename = "history.clasp";
	        if (File.Exists(_historyFilename))
	        {
	            _commandHistory = File.ReadLines(_historyFilename).ToList();
	            _currentHistoryIndex = _commandHistory.Count - 1;
	        }

			var repl = new Repl();
			while (true)
			{
			    var applicationExit = false;
                var input = new ConsoleLine();
                Console.Write(Prompt);
			    while (true)
			    {
                    var lastChar = Console.ReadKey(true);
			        if (lastChar.Key == ConsoleKey.Enter)
			        {
                        Console.WriteLine();
			            break;
			        }
			        if (lastChar.Modifiers == ConsoleModifiers.Control && lastChar.Key == ConsoleKey.D)
			        {
			            applicationExit = true;
			            break;
			        }

			        if (lastChar.Key == ConsoleKey.Backspace)
			        {
			            input.RemoveLastChar();
			        }
                    else if (lastChar.Key == ConsoleKey.LeftArrow)
                    {
                        input.MoveCaretLeft();
                    }
                    else if (lastChar.Key == ConsoleKey.RightArrow)
                    {
                        input.MoveCaretRight();
                    }
                    else if (lastChar.Key == ConsoleKey.UpArrow)
                    {
                        input = new ConsoleLine(_commandHistory[_currentHistoryIndex]);
                        _currentHistoryIndex--;
                        if (_currentHistoryIndex == -1)
                            _currentHistoryIndex = 0;
                    }
                    else if (lastChar.Key == ConsoleKey.DownArrow)
                    {
                        input = new ConsoleLine(_commandHistory[_currentHistoryIndex]);
                        _currentHistoryIndex++;
                        if (_currentHistoryIndex == _commandHistory.Count)
                            _currentHistoryIndex = _commandHistory.Count - 1;
                    }
                    else if (lastChar.Modifiers == ConsoleModifiers.Control && lastChar.Key == ConsoleKey.B)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Command History");
                        var foreground = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("===============");
                        Console.ForegroundColor = foreground;
                        foreach (var str in _commandHistory)
                            Console.WriteLine(str);
                        input = new ConsoleLine();
                        break;
                    }
			        else
			        {
                        input += lastChar.KeyChar;   
			        }

                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(Prompt);
                    Console.Write(input + new string(' ', Console.WindowWidth - input.Length - Prompt.Length - 1));
                    Console.SetCursorPosition(Prompt.Length + input.CaretPosition, Console.CursorTop);
			    }

				if (applicationExit)
					break;

			    if (string.IsNullOrWhiteSpace(input))
			        continue;

			    if (_commandHistory.Contains(input))
			    {
			        _commandHistory.Remove(input);
			        _currentHistoryIndex--;
			    }
			    _commandHistory.Add(input);
			    _currentHistoryIndex++;
				repl.Run(input);
			}
		}

	    private static void SaveCommandHistory(object sender, EventArgs args)
	    {
	        File.WriteAllLines(_historyFilename, _commandHistory);
	    }
	}
}
