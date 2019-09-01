using System;
using System.Linq;
using static System.Math;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var vs = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var mi = -1;
		var ms = 0.0;
		var div = 360;
		var d = 2 * PI / div;
		for (int i = 0; i < div; i++)
		{
			var a = i * d;
			var s = vs.Select(v => v[0] * Cos(a) + v[1] * Sin(a)).Where(x => x > 0).Sum();
			if (ms < s) { mi = i; ms = s; }
		}

		var ma = mi * d;
		var mvs = vs.Select(v => new { v, x = v[0] * Cos(ma) + v[1] * Sin(ma) }).Where(_ => _.x > 0).ToArray();
		double xs = mvs.Sum(_ => _.v[0]);
		double ys = mvs.Sum(_ => _.v[1]);
		Console.WriteLine(Sqrt(xs * xs + ys * ys));
	}
}
