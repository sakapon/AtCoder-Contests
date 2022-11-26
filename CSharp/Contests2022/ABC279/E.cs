using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		var rm = Enumerable.Range(1, m).ToArray();

		var sets = Array.ConvertAll(new bool[n + 1], _ => new HashSet<int>());
		sets[1] = rm.ToHashSet();

		for (int i = 0; i < m; i++)
		{
			if (sets[a[i]].Contains(i + 1))
			{
				sets[a[i]].Remove(i + 1);
				sets[a[i] + 1].Add(i + 1);
			}
			else if (sets[a[i] + 1].Contains(i + 1))
			{
				sets[a[i] + 1].Remove(i + 1);
				sets[a[i]].Add(i + 1);
			}

			(sets[a[i]], sets[a[i] + 1]) = (sets[a[i] + 1], sets[a[i]]);
		}

		var r = new int[m];
		for (int i = 1; i <= n; i++)
		{
			foreach (var j in sets[i])
			{
				r[j - 1] = i;
			}
		}
		return string.Join("\n", r);
	}
}
