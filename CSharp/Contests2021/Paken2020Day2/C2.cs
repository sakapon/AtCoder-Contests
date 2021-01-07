using System;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var c = new int[n + 1];
		foreach (var (a, b) in es)
		{
			if (c[a] < 0) m--;
			c[a]++;
			if (c[b] > 0) m--;
			c[b]--;
		}
		return m;
	}
}
