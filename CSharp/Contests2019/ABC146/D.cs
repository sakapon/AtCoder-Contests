using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	class C
	{
		public int Color;
	}

	class R
	{
		public int P;
		public C C;
	}

	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var rs = new int[n - 1].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).Select(r => new { r, c = new C() }).ToArray();

		var map = new int[n + 1].Select(_ => new List<R>()).ToArray();
		foreach (var r in rs)
		{
			map[r.r[0]].Add(new R { P = r.r[1], C = r.c });
			map[r.r[1]].Add(new R { P = r.r[0], C = r.c });
		}

		var q = new Queue<int>();
		q.Enqueue(1);
		var u = new int[n + 1];

		while (q.Any())
		{
			var p = q.Dequeue();
			var t = 1;
			foreach (var r in map[p])
			{
				if (r.C.Color > 0) continue;
				if (u[p] == t) t++;

				q.Enqueue(r.P);
				u[r.P] = t;
				r.C.Color = t;
				t++;
			}
		}
		Console.WriteLine(rs.Max(x => x.c.Color));
		Console.WriteLine(string.Join("\n", rs.Select(x => x.c.Color)));
	}
}
