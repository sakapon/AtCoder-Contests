using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var qs = new int[int.Parse(Console.ReadLine())].Select(_ => int.Parse(Console.ReadLine())).ToArray();
		Console.WriteLine(string.Join("\n", qs.Select(k => First(0, n, i => a[i] >= k))));
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
