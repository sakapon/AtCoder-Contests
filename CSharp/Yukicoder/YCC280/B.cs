using System;
using System.Text.RegularExpressions;

class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve() => Regex.IsMatch(Console.ReadLine(), "^k?a?d?o?m?a?t?s?u?$");
}
