using System;
using System.Linq;

class B
{
	static double[] Read() => Console.ReadLine().Split().Select(double.Parse).ToArray();
	static void Main()
	{
		var h = Read();

		// Vector2 は float のため、精度が足りません。
		var p1 = new V(h[0], h[1]);
		var p2 = new V(h[2], h[3]);
		var p0 = p2 - p1;
		var e = p0 / p0.Norm;

		var q = int.Parse(Console.ReadLine());
		var r = new int[q].Select(_ => Read())
			.Select(x => new V(x[0], x[1]))
			.Select(p =>
			{
				var p_ = p - p1;
				return p + 2 * (V.Dot(p_, e) * e - p_);
			});
		Console.WriteLine(string.Join("\n", r));
	}
}
