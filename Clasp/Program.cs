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
				Console.Write("user> ");
			    var applicationExit = false;
			    var input = "";
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
                        Console.Write(lastChar.KeyChar);
                        Console.Write(" ");
                        Console.Write(lastChar.KeyChar);
			            input = input.Substring(0, input.Length - 1);
			        }
                    else if (lastChar.Key == ConsoleKey.LeftArrow)
                    {
                        Console.CursorLeft = Console.CursorLeft - 1;
                    }
                    else if (lastChar.Key == ConsoleKey.RightArrow)
                    {
                        Console.CursorLeft = Console.CursorLeft + 1;
                    }
                    else if (lastChar.Key == ConsoleKey.UpArrow)
                    {
                        input = UpdateInput(input);
                        _currentHistoryIndex--;
                        if (_currentHistoryIndex == -1)
                            _currentHistoryIndex = 0;
                    }
                    else if (lastChar.Key == ConsoleKey.DownArrow)
                    {
                        input = UpdateInput(input);
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
                        input = "";
                        break;
                    }
			        else
			        {
                        Console.Write(lastChar.KeyChar);
                        input += lastChar.KeyChar;   
			        }
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

	    private static string UpdateInput(string input)
	    {
	        Console.CursorLeft = Console.CursorLeft - input.Length;
	        Console.Write(new string(' ', input.Length));
	        Console.CursorLeft = Console.CursorLeft - input.Length;
	        input = _commandHistory[_currentHistoryIndex];
	        Console.Write(input);
	        return input;
	    }

	    private static void SaveCommandHistory(object sender, EventArgs args)
	    {
	        File.WriteAllLines(_historyFilename, _commandHistory);
	    }
	}
}
