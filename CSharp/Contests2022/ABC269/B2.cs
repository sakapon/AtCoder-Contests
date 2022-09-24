using System;
using System.Linq;

class B2
{
	static void Main()
	{
		var s = Array.ConvertAll(new bool[10], _ => Console.ReadLine());

		var ps = Enumerable.Range(0, 100).Select(x => (i: x / 10, j: x % 10)).Where(p => s[p.i][p.j] == '#').ToArray();
		Console.WriteLine($"{ps.Min(p => p.i) + 1} {ps.Max(p => p.i) + 1}");
		Console.WriteLine($"{ps.Min(p => p.j) + 1} {ps.Max(p => p.j) + 1}");
	}
}
