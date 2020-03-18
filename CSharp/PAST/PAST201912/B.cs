using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToArray();

		var q = Enumerable.Range(1, n - 1).Select(i => a[i] - a[i - 1]).Select(x => x == 0 ? "stay" : x < 0 ? $"down {-x}" : $"up {x}");
		Console.WriteLine(string.Join("\n", q));
	}
}
