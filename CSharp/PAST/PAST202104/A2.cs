using System;
using System.Text.RegularExpressions;

class A2
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve() => Regex.IsMatch(Console.ReadLine(), @"\d{3}-\d{4}");
}
