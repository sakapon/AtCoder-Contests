using System;
using System.Linq;
using static System.Math;

class F
{
	static void Main()
	{
		var div = 360;
		var d = 2 * PI / div;
		var n = int.Parse(Console.ReadLine());
		var vs = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var mi = -1;
		var ms = 0.0;
		var mvs = new int[0][];
		for (var i = 0; i < div; i++)
		{
			var a = i * d;
			var vs2 = vs.Select(v => new { v, x = v[0] * Cos(a) + v[1] * Sin(a) }).Where(_ => _.x > 0).ToArray();
			var s = vs2.Sum(_ => _.x);
			if (ms < s) { mi = i; ms = s; mvs = vs2.Select(_ => _.v).ToArray(); }
		}
		double mx = mvs.Sum(v => v[0]);
		double my = mvs.Sum(v => v[1]);
		Console.WriteLine(Sqrt(mx * mx + my * my));
	}
}
