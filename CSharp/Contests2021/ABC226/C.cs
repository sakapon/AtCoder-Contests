using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => { var a = Read(); return (t: a[0], a: a.Skip(2).ToArray()); });

		var u = new bool[n + 1];
		var r = 0L;

		Rec(n);
		return r;

		void Rec(int i)
		{
			if (u[i]) return;
			u[i] = true;

			var (t, a) = ps[i - 1];
			r += t;

			foreach (var j in a)
			{
				Rec(j);
			}
		}
	}
}
