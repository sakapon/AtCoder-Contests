using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		return Rec(1 << 29, a);
	}

	static int Rec(int f, int[] a)
	{
		if (f == 0) return 0;

		var l0 = new List<int>();
		var l1 = new List<int>();

		foreach (var v in a)
		{
			if ((v & f) == 0)
			{
				l0.Add(v);
			}
			else
			{
				l1.Add(v);
			}
		}

		if (l0.Count == 0 || l1.Count == 0)
		{
			return Rec(f >> 1, a);
		}
		else
		{
			var r0 = Rec(f >> 1, l0.ToArray());
			var r1 = Rec(f >> 1, l1.ToArray());
			return f | Math.Min(r0, r1);
		}
	}
}
