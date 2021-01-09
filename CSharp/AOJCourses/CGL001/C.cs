using System;
using System.Linq;

class C
{
	static double[] Read() => Console.ReadLine().Split().Select(double.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var p0 = new V(h[0], h[1]);
		var p1 = new V(h[2], h[3]) - p0;

		var q = int.Parse(Console.ReadLine());
		var r = new int[q].Select(_ => Read())
			.Select(x => new V(x[0], x[1]) - p0)
			.Select(p => p.Rotate(-p1.Angle))
			.Select(p => new V(Math.Round(p.X, 9) == 0 ? 0 : p.X, Math.Round(p.Y, 9) == 0 ? 0 : p.Y))
			.Select(p =>
				p.Y > 0 ? "COUNTER_CLOCKWISE" :
				p.Y < 0 ? "CLOCKWISE" :
				p.X < 0 ? "ONLINE_BACK" :
				Math.Round(p.X - p1.Norm, 9) == 0 ? "ON_SEGMENT" :
				p.X > p1.Norm ? "ONLINE_FRONT" :
				"ON_SEGMENT");
		Console.WriteLine(string.Join("\n", r));
	}
}
