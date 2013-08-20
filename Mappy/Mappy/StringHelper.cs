using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Mappy
{
    internal static class StringHelper
    {
        public static int GetSymbolIndex(string symbol, string[] lines)
        {
            int index = GetNextSymbol(0, symbol, lines);
            if (index == -1)
                Console.WriteLine("The symbol '" + symbol
                    + "' was not found. source:StringHelper.GetSymbolIndex()");
            return index;
        }

        public static int GetNextSymbol(int startIndex, string symbol, string[] lines)
        {
            for (int i = startIndex; i < lines.Length; i++)
                if (lines[i].StartsWith(symbol)) return i;
            Console.WriteLine("The symbol '" + symbol + "' was not found. " +
                "source:StringHelper.GetNextSymbol()");
            return -1;
        }

        public static string[] GetCleanLines(string path)
        {
            string[] lines = File.ReadAllLines(path);

            IEnumerable<string> cleanLines =
                from line in lines
                where !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#")
                select line.Trim();

            return cleanLines.ToArray();
        }
    }
}
