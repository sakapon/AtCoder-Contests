using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Values;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = ReadL();
		var pa = new IntV(a[0], a[1]);
		var pb = new IntV(a[2], a[3]);
		var pc = new IntV(a[4], a[5]);

		var sx = Math.Sign(pc.X - pb.X);
		var pb_x = pb - new IntV(sx, 0);

		var sy = Math.Sign(pc.Y - pb.Y);
		var pb_y = pb - new IntV(0, sy);

		var r = 0L;

		if (sx == 0)
		{
			r += (pc - pb).NormL1;
			r += (pa - pb_y).NormL1;
			if (pb.X == pa.X && sy == Math.Sign(pa.Y - pb.Y)) r += 2;
		}
		else if (sy == 0)
		{
			r += (pc - pb).NormL1;
			r += (pa - pb_x).NormL1;
			if (pb.Y == pa.Y && sx == Math.Sign(pa.X - pb.X)) r += 2;
		}
		else
		{
			r += (pc - pb).NormL1;
			r += 2;
			r += Math.Min((pa - pb_x).NormL1, (pa - pb_y).NormL1);
		}
		return r;
	}
}
