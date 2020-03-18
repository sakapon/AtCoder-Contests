using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	struct P
	{
		public long r, count;
	}

	static void Main()
	{
		Func<long[]> read = () => Console.ReadLine().Split().Select(long.Parse).ToArray();
		var h = read();
		long n = h[0], d = h[1], a = h[2];
		var ms = new int[n].Select(_ => read()).OrderBy(x => x[0]).ToArray();

		long sc = 0, tc = 0;
		var q = new Queue<P>();
		foreach (var m in ms)
		{
			P p;
			while (q.Any() && (p = q.Peek()).r < m[0])
			{
				tc -= p.count;
				q.Dequeue();
			}

			var c = (m[1] - 1) / a + 1 - tc;
			if (c <= 0) continue;
			sc += c;
			tc += c;
			q.Enqueue(new P { r = m[0] + 2 * d, count = c });
		}
		Console.WriteLine(sc);
	}
}
