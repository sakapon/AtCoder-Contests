using System;
using System.Collections.Generic;
using System.Linq;

class N
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		int n = h[0], w = h[1], c = h[2];
		var ps = new int[n].Select(_ => read()).OrderBy(x => x[0]);

		var lq = new Queue<int[]>(ps);
		var rq = PQ<int[]>.Create(p => p[1]);

		long m = long.MaxValue, s = 0;
		int l = 0, r = c;

		while (r <= w)
		{
			while (rq.Any && rq.First[1] <= l)
			{
				var p = rq.Pop();
				s -= p[2];
			}
			while (lq.Any() && lq.Peek()[0] < r)
			{
				var p = lq.Dequeue();
				rq.Push(p);
				s += p[2];
			}
			m = Math.Min(m, s);

			var d = Math.Min(rq.Any ? rq.First[1] - l : int.MaxValue, lq.Any() ? lq.Peek()[0] + 1 - r : int.MaxValue);
			if (d == int.MaxValue) break;
			l += d;
			r += d;
		}
		Console.WriteLine(m);
	}
}
