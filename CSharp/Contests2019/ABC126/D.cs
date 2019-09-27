using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	struct R
	{
		public int P;
		public bool Odd;
	}

	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var map = new int[n + 1].Select(_ => new List<R>()).ToArray();
		foreach (var r in new int[n - 1].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()))
		{
			map[r[0]].Add(new R { P = r[1], Odd = r[2] % 2 == 1 });
			map[r[1]].Add(new R { P = r[0], Odd = r[2] % 2 == 1 });
		}

		var cs = new bool[n + 1];
		var q = new Queue<int>();
		q.Enqueue(1);
		while (q.Any())
		{
			var p = q.Dequeue();
			foreach (var r in map[p])
			{
				cs[r.P] = cs[p] ^ r.Odd;
				map[r.P].RemoveAll(x => x.P == p);
				q.Enqueue(r.P);
			}
		}
		foreach (var c in cs.Skip(1)) Console.WriteLine(c ? 1 : 0);
	}
}
