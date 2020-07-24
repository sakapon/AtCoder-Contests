using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())]
		.Select(_ => Console.ReadLine().Split())
		.Select(r => (x: int.Parse(r[0]), c: r[1] == "R" ? 0 : 1))
		.OrderBy(b => b.c).ThenBy(b => b.x)
		.Select(b => b.x)));
}
