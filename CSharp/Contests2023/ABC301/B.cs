using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = new List<int>();

		foreach (var x in a)
		{
			if (r.Count == 0)
			{
				r.Add(x);
			}
			else
			{
				var p = r[^1];
				if (p < x)
				{
					r.AddRange(Enumerable.Range(p + 1, x - p));
				}
				else
				{
					r.AddRange(Enumerable.Range(x, p - x).Reverse());
				}
			}
		}

		return string.Join(" ", r);
	}
}
