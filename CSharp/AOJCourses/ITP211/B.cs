using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var f = Console.ReadLine().Split().Select(int.Parse).Skip(1).Aggregate(0, (t, i) => t | (1 << i));
		var rn = Enumerable.Range(0, n).ToArray();
		Console.WriteLine(string.Join("\n", Enumerable.Range(0, 1 << n).Where(x => (x & f) == f).Select(x => x == 0 ? "0:" : $"{x}: {string.Join(" ", rn.Where(i => (x & (1 << i)) != 0))}")));
	}
}
