using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	struct P
	{
		public int c, v;
	}

	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).OrderByDescending(x => x));

		var q = new List<P> { new P { c = n, v = s.Dequeue() } };
		while (q.Any())
		{
			var ps = q.ToArray();
			q.Clear();

			foreach (var p in ps)
				for (var i = p.c - 1; i >= 0; i--)
				{
					var sv = s.Dequeue();
					if (sv >= p.v) { Console.WriteLine("No"); return; }
					if (i > 0) q.Add(new P { c = i, v = sv });
				}
		}
		Console.WriteLine("Yes");
	}
}
