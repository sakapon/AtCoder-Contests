using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var b = Console.ReadLine().Split().Select(int.Parse).Skip(1).ToArray();
		var k = b.Length;
		var rk = Enumerable.Range(0, k).ToArray();
		Console.WriteLine(string.Join("\n", Enumerable.Range(0, 1 << k).Select(x => rk.Where(i => (x & (1 << i)) != 0).Select(i => b[i]).ToArray()).Select(a => !a.Any() ? "0:" : $"{a.Aggregate(0, (t, i) => t | (1 << i))}: {string.Join(" ", a)}")));
	}
}
