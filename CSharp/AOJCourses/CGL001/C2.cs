using System;
using System.Linq;

class C2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int x1 = h[2] - h[0], y1 = h[3] - h[1];

		var q = int.Parse(Console.ReadLine());
		var r = new int[q].Select(_ => Read())
			.Select(v =>
			{
				int x2 = v[0] - h[0], y2 = v[1] - h[1];
				return
					x1 * y2 > x2 * y1 ? "COUNTER_CLOCKWISE" :
					x1 * y2 < x2 * y1 ? "CLOCKWISE" :
					x1 * x2 < 0 || y1 * y2 < 0 ? "ONLINE_BACK" :
					Math.Abs(x1) < Math.Abs(x2) || Math.Abs(y1) < Math.Abs(y2) ? "ONLINE_FRONT" :
					"ON_SEGMENT";
			});
		Console.WriteLine(string.Join("\n", r));
	}
}
