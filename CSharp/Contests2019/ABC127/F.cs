using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var qs = new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray());

		long a = 0, b = 0;
		var l = new PQ<int>(c: (x, y) => y - x);
		var r = new PQ<int>();
		var s = new List<string>();
		foreach (var q in qs)
		{
			if (q[0] == 2) { s.Add($"{l.First} {a + b}"); continue; }

			if (l.Count == r.Count)
			{
				if (r.Any() && q[1] > r.First)
				{
					l.Push(r.Pop());
					r.Push(q[1]);
				}
				else l.Push(q[1]);
			}
			else
			{
				if (q[1] < l.First)
				{
					r.Push(l.Pop());
					l.Push(q[1]);
					a += r.First - l.First;
				}
				else r.Push(q[1]);
			}
			a += Math.Abs(l.First - q[1]);
			b += q[2];
		}
		Console.WriteLine(string.Join("\n", s));
	}
}
