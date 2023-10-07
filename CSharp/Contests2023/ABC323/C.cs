using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var rn = Enumerable.Range(0, n).ToArray();
		var rm = Enumerable.Range(0, m).ToArray();

		var sums = rn.Select(i => i + 1 + rm.Sum(j => s[i][j] == 'o' ? a[j] : 0)).ToArray();
		var r = rn.Select(v =>
		{
			var d = rn.Where(i => i != v).Max(i => sums[i]) - sums[v];
			var b = rm.Where(j => s[v][j] == 'x').Select(j => a[j]).ToArray();
			Array.Sort(b);
			Array.Reverse(b);

			var sum = 0;
			for (int k = 0; k < b.Length; k++)
			{
				if (sum > d) return k;
				sum += b[k];
			}
			return b.Length;
		});
		return string.Join("\n", r);
	}
}
