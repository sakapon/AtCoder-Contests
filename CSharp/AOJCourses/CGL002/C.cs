using System;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));

	static string Solve()
	{
		var h = Read();
		V p0 = new V(h[0], h[1]), p1 = new V(h[2], h[3]), p2 = new V(h[4], h[5]), p3 = new V(h[6], h[7]);
		V s1 = p1 - p0, s2 = p3 - p2;

		Func<V, V, double, double> getY = (q0, q1, x) => (double)(q1.Y - q0.Y) / (q1.X - q0.X) * (x - q0.X) + q0.Y;
		var m1 = (double)s1.Y / s1.X;
		var m2 = (double)s2.Y / s2.X;

		if (double.IsInfinity(m1)) return $"{p0.X:F9} {getY(p2, p3, p0.X):F9}";
		if (double.IsInfinity(m2)) return $"{p2.X:F9} {getY(p0, p1, p2.X):F9}";

		var x0 = (m1 * p0.X - m2 * p2.X + p2.Y - p0.Y) / (m1 - m2);
		return $"{x0:F9} {getY(p0, p1, x0):F9}";
	}
}
