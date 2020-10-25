using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], m = h[1];

		if (m < 0 || n > 1 && m >= n - 1) { Console.WriteLine(-1); return; }

		Console.WriteLine(string.Join("\n", Enumerable.Range(1, n - 1).Select(i => $"{3 * i} {3 * i + 2}")));
		Console.WriteLine($"1 {3 * m + 4}");
	}
}
