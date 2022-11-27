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

		var rm = Enumerable.Range(0, m).ToArray();

		var sets = Array.ConvertAll(new bool[n + 1], _ => new HashSet<int>());
		sets[1] = rm.ToHashSet();

		for (int j = 0; j < m; j++)
		{
			if (sets[a[j]].Contains(j))
			{
				sets[a[j]].Remove(j);
				sets[a[j] + 1].Add(j);
			}
			else if (sets[a[j] + 1].Contains(j))
			{
				sets[a[j] + 1].Remove(j);
				sets[a[j]].Add(j);
			}

			(sets[a[j]], sets[a[j] + 1]) = (sets[a[j] + 1], sets[a[j]]);
		}

		var r = new int[m];
		for (int i = 1; i <= n; i++)
		{
			foreach (var j in sets[i])
			{
				r[j] = i;
			}
		}
		return string.Join("\n", r);
	}
}
