using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;

class D2Q
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();

		if (a.Distinct().Count() == m) return 0;

		Array.Sort(a);
		var q = new ArrayDeque<long>(a);

		if (a[0] == 0 && a[^1] == m - 1)
		{
			q.Last += q.PopFirst();
		}
		else
		{
			q.AddLast(q.PopFirst());
		}

		for (int i = 1; i < n; i++)
		{
			if (a[i] - a[i - 1] <= 1)
			{
				q.Last += q.PopFirst();
			}
			else
			{
				q.AddLast(q.PopFirst());
			}
		}

		return a.Sum() - q.Max();
	}
}
