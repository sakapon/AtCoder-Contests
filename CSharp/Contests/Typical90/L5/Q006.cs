using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class Q006
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine();

		var r = new List<char>();

		var t = -1;
		var q = PQ<int>.Create(i => s[i] * 100000L + i);
		q.PushRange(Enumerable.Range(0, n - k).ToArray());

		for (int i = n - k; i < n; i++)
		{
			q.Push(i);
			while (q.First < t) q.Pop();

			t = q.Pop();
			r.Add(s[t]);
		}

		return string.Join("", r);
	}
}
