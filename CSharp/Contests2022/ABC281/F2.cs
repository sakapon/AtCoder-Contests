using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		Array.Sort(a);
		return Rec(1 << 29, a);
	}

	static int Rec(int f, int[] a)
	{
		if (f == 0) return 0;

		var ai = 0;
		while (ai < a.Length && (a[ai] & f) == 0) ai++;

		if (ai == 0 || ai == a.Length)
		{
			return Rec(f >> 1, a);
		}
		else
		{
			var r0 = Rec(f >> 1, a[..ai]);
			var r1 = Rec(f >> 1, a[ai..]);
			return f | Math.Min(r0, r1);
		}
	}
}
