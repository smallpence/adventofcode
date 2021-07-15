using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input");
            
            int lineCount = lines.Where(line => checkPassword(parseLine(line))).Count();

            Console.WriteLine(checkPassword(parseLine("2-9 c: ccccccccc")));

            Console.WriteLine(lineCount);
        }

        static bool checkPassword(Tuple<Rule,string> parsedLine)
        {
            Rule rule = parsedLine.Item1;
            string password = parsedLine.Item2;
            // int occurences = password.ToCharArray().Sum(checkChar => checkChar == rule.letter ? 1 : 0);
            // return occurences >= rule.pos1 && occurences <= rule.pos2;

            return 
                (password[rule.pos1-1] == rule.letter || password[rule.pos2-1] == rule.letter)
                && password[rule.pos1-1] != password[rule.pos2-1];
        }

        static Tuple<Rule,string> parseLine(string line) {
            string[] parts = line.Split(":");
            return new Tuple<Rule, string>(new Rule(parts[0]),parts[1].Trim());
        }
    }

    struct Rule
    {
        public char letter;
        public int pos1;
        public int pos2;

        public Rule(string format)
        {
            Match match = Regex.Match(format,@"(\d+)-(\d+) (\w)");

            this.pos1 = int.Parse(match.Groups[1].ToString());
            this.pos2 = int.Parse(match.Groups[2].ToString());
            this.letter = match.Groups[3].ToString()[0];
        }
    }
}
