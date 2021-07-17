using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"Case #{i + 1}: {Solve()}")));
	static object Solve()
	{
		var (n, c) = Read2();
		if (c < n - 1 || c >= n * (n + 1) / 2) return "IMPOSSIBLE";

		c -= n - 1;
		var a = Enumerable.Range(1, n).ToArray();

		for (int i = n - 2; i >= 0; i--)
		{
			var l = Math.Min(c, n - 1 - i);
			c -= l;
			Array.Reverse(a, i, l + 1);
		}
		return string.Join(" ", a);
	}
}
