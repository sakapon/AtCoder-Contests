using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections.Statics.Typed;

class B2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var t = int.Parse(Console.ReadLine());
		var ns = Array.ConvertAll(new bool[t], _ => long.Parse(Console.ReadLine()));

		var l = new List<long>();

		for (int i = 59; i >= 0; i--)
		{
			for (int j = i - 1; j >= 0; j--)
			{
				for (int k = j - 1; k >= 0; k--)
				{
					l.Add((1L << i) | (1L << j) | (1L << k));
				}
			}
		}
		l.Add(-1);
		l.Reverse();

		var set = new ArrayItemSet<long>(l.ToArray());
		return string.Join("\n", ns.Select(set.GetLastLeq));
	}
}
