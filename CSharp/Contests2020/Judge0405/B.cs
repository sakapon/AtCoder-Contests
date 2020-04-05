using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split())
			.Select(x => (v: int.Parse(x[0]), g: x[1] == "R" ? 0 : 1))
			.ToArray();

		Console.WriteLine(string.Join("\n", ps.OrderBy(x => x.g).ThenBy(x => x.v).Select(x => x.v)));
	}
}
