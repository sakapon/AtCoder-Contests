using System;
using System.Collections.Generic;
using System.Linq;

class Q048
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var r = 0L;
		var q = PQ<(int i, int x)>.Create(_ => _.x, true);
		q.PushRange(ps.Select((_, i) => (i, _.b)).ToArray());

		for (int j = 0; j < k; j++)
		{
			var (i, x) = q.Pop();
			r += x;
			if (i != -1)
			{
				var (a, b) = ps[i];
				q.Push((-1, a - b));
			}
		}
		return r;
	}
}
