using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));

	static long Solve()
	{
		var h = Read();
		V p0 = new V(h[0], h[1]), p1 = new V(h[2], h[3]), p2 = new V(h[4], h[5]), p3 = new V(h[6], h[7]);
		V s1 = p1 - p0, s2 = p3 - p2;

		Func<V, V, V, int> sign = (q0, q1, p) => Math.Sign((p.X - q0.X) * (q1.Y - q0.Y) - (q1.X - q0.X) * (p.Y - q0.Y));
		var d1 = sign(p0, p1, p2) * sign(p0, p1, p3);
		var d2 = sign(p2, p3, p0) * sign(p2, p3, p1);

		if (V.IsParallel(s1, s2))
		{
			// 同一直線上にない
			if (d1 != 0) return 0;

			if (p0.X == p1.X && p0.X == p2.X)
			{
				// y 軸に平行
				int m1 = Math.Min(p0.Y, p1.Y), M1 = Math.Max(p0.Y, p1.Y);
				int m2 = Math.Min(p2.Y, p3.Y), M2 = Math.Max(p2.Y, p3.Y);
				return (m1 < m2 ? m2 <= M1 : m1 <= M2) ? 1 : 0;
			}
			else
			{
				int m1 = Math.Min(p0.X, p1.X), M1 = Math.Max(p0.X, p1.X);
				int m2 = Math.Min(p2.X, p3.X), M2 = Math.Max(p2.X, p3.X);
				return (m1 < m2 ? m2 <= M1 : m1 <= M2) ? 1 : 0;
			}
		}
		else
		{
			return d1 <= 0 && d2 <= 0 ? 1 : 0;
		}
	}
}
