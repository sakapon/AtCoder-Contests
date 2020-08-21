using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));

	static double Solve()
	{
		var h = Read();
		V p0 = new V(h[0], h[1]), p1 = new V(h[2], h[3]), p2 = new V(h[4], h[5]), p3 = new V(h[6], h[7]);

		if (Intersects(p0, p1, p2, p3)) return 0;
		return new[] { Distance(p0, p1, p2), Distance(p0, p1, p3), Distance(p2, p3, p0), Distance(p2, p3, p1) }.Min();
	}

	static bool Intersects(V p0, V p1, V p2, V p3)
	{
		Func<V, V, V, int> sign = (q0, q1, p) => Math.Sign((p.X - q0.X) * (q1.Y - q0.Y) - (q1.X - q0.X) * (p.Y - q0.Y));
		var d1 = sign(p0, p1, p2) * sign(p0, p1, p3);
		var d2 = sign(p2, p3, p0) * sign(p2, p3, p1);

		if (V.IsParallel(p1 - p0, p3 - p2))
		{
			// 同一直線上にない
			if (d1 != 0) return false;

			if (p0.X == p1.X && p0.X == p2.X)
			{
				// y 軸に平行
				int m1 = Math.Min(p0.Y, p1.Y), M1 = Math.Max(p0.Y, p1.Y);
				int m2 = Math.Min(p2.Y, p3.Y), M2 = Math.Max(p2.Y, p3.Y);
				return m1 < m2 ? m2 <= M1 : m1 <= M2;
			}
			else
			{
				int m1 = Math.Min(p0.X, p1.X), M1 = Math.Max(p0.X, p1.X);
				int m2 = Math.Min(p2.X, p3.X), M2 = Math.Max(p2.X, p3.X);
				return m1 < m2 ? m2 <= M1 : m1 <= M2;
			}
		}
		else
		{
			return d1 <= 0 && d2 <= 0;
		}
	}

	// 線分 P0P1 と点 Q の距離
	static double Distance(V p0, V p1, V q)
	{
		var r = new List<double> { (p0 - q).Norm, (p1 - q).Norm };

		Func<V, V, V, int> f = (q0, q1, p) => (p.X - q0.X) * (q1.Y - q0.Y) - (q1.X - q0.X) * (p.Y - q0.Y);
		var s = p1 - p0;
		if (V.Dot(q - p0, s) > 0 && V.Dot(q - p1, -s) > 0)
			r.Add(Math.Abs(f(p0, p1, q)) / s.Norm);

		return r.Min();
	}
}
