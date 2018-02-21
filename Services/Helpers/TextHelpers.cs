using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helpers
{
    public static class TextHelpers
    {
        public static string GetWithoutWhiteSpace(this string line)
        {
            string newLine = null;
            for (int str = 0; str < line.Length; str++)
            {
                if (line[str] == ' ')
                {
                    continue;
                }
                newLine += line[str];
            }
            return newLine;
        }
    }
}
