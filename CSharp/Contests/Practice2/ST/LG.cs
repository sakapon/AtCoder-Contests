using System;

class LG
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		var n = h[0];
		var a = Read();

		var st = new LST<bool, (long c0, long c1, long ord, long inv)>(n,
			(t, u) => t ^ u, false,
			(x, y) => (x.c0 + y.c0, x.c1 + y.c1, x.ord + y.ord + x.c0 * y.c1, x.inv + y.inv + x.c1 * y.c0), default,
			(t, p, _, l) => t ? (p.c1, p.c0, p.inv, p.ord) : p,
			Array.ConvertAll(a, x => x == 0 ? (1L, 0L, 0L, 0L) : (0L, 1L, 0L, 0L)));

		for (int k = 0; k < h[1]; k++)
		{
			var q = Read();
			if (q[0] == 1)
				st.Set(q[1] - 1, q[2], true);
			else
				Console.WriteLine(st.Get(q[1] - 1, q[2]).inv);
		}
		Console.Out.Flush();
	}
}
