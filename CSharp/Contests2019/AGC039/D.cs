using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

class D
{
	struct P
	{
		public double x, y;
		public P(double _x, double _y) { x = _x; y = _y; }
	}

	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], l = h[1];
		var t = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToArray();

		var c = t
			.Select(x => x - t[0])
			.Select(x => new P(Cos(2 * PI * x / l) - 1, Sin(2 * PI * x / l)))
			.ToArray();

		var centers = new List<P>();
		for (int i = 1; i < n; i++)
			for (int j = i + 1; j < n; j++)
				centers.Add(GetCenter(c[i], c[j]));
		Console.WriteLine($"{centers.Average(p => p.x) + 1} {centers.Average(p => p.y)}");
	}

	static readonly P O = new P();
	static P GetCenter(P p1, P p2)
	{
		var v = p1.x * p2.y - p2.x * p1.y;
		var d1 = Norm(p1, O);
		var d2 = Norm(p2, O);

		var s = Abs(v) / 2;
		var r = 2 * s / (d1 + d2 + Norm(p1, p2));
		return new P((d1 * p2.x + d2 * p1.x) * r / v, (d1 * p2.y + d2 * p1.y) * r / v);
	}

	static double Norm(P p1, P p2)
	{
		var dx = p1.x - p2.x;
		var dy = p1.y - p2.y;
		return Sqrt(dx * dx + dy * dy);
	}
}
