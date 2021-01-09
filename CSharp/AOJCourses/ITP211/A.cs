using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var rn = Enumerable.Range(0, n).ToArray();
		Console.WriteLine(string.Join("\n", Enumerable.Range(0, 1 << n).Select(x => x == 0 ? "0:" : $"{x}: {string.Join(" ", rn.Where(i => (x & (1 << i)) != 0))}")));
	}
}
