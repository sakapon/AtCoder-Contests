using System;
using System.Linq;

class Q007
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read().Append(-1 << 30).Append(int.MaxValue).ToArray();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		Array.Sort(a);
		return string.Join("\n", qs.Select(GetMin));

		int GetMin(int b)
		{
			var i = Min(0, a.Length, j => a[j] >= b);
			return Math.Min(b - a[i - 1], a[i] - b);
		}
	}

	static int Min(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
