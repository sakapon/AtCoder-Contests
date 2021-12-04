using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	class Wall
	{
		public int l, r;
		public bool used;
	}

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int l, int r) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => { var a = Read(); return new Wall { l = a[0], r = a[1] }; });

		var ql = new Queue<Wall>(ps.OrderBy(p => p.l));
		var qr = new Queue<Wall>(ps.OrderBy(p => p.r));
		var r = 0;

		while (qr.TryDequeue(out var wall))
		{
			if (wall.used) continue;

			r++;
			wall.used = true;

			while (ql.Count > 0 && ql.Peek().l < wall.r + d)
			{
				var wl = ql.Dequeue();
				wl.used = true;
			}
		}
		return r;
	}
}
