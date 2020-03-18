using System;
using System.Linq;
using System.Text.RegularExpressions;

class F2
{
	static void Main() => Console.WriteLine(string.Join("", Regex.Matches(Console.ReadLine(), "[A-Z][a-z]*[A-Z]").Cast<Match>().OrderBy(x => x.Value)));
}
