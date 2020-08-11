using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())]
		.Select(_ => Console.ReadLine().Split())
		.Select(x => (x[1] == "B", v: int.Parse(x[0])))
		.OrderBy(x => x)
		.Select(b => b.v)));
}
