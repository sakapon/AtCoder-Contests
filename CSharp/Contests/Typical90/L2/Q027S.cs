using System;
using System.Linq;

class Q027S
{
	static void Main() => Console.WriteLine(string.Join("\n", Enumerable.Range(1, int.Parse(Console.ReadLine())).GroupBy(_ => Console.ReadLine()).Select(g => g.First())));
}
