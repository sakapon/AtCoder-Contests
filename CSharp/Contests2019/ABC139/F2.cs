using System;
using System.Linq;
using static System.Math;

class F2
{
	static void Main()
	{
		var vs = new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var d = vs.SelectMany((v, i) =>
		{
			var a = Atan2(v[1], v[0]);
			return new[] { new { i, isIn = true, a = a > -PI / 2 ? a - PI / 2 : a + 3 * PI / 2 }, new { i, isIn = false, a = a <= PI / 2 ? a + PI / 2 : a - 3 * PI / 2 } };
		})
		.GroupBy(_ => _.a).OrderBy(g => g.Key).ToDictionary(g => g.Key, g => g.ToArray());
		var vis = d.Where(p => p.Key > 0).SelectMany(p => p.Value.Where(_ => _.isIn)).Select(_ => _.i).ToList();

		var M = 0.0;
		foreach (var p in d)
		{
			foreach (var _ in p.Value)
			{
				if (_.isIn) vis.Add(_.i);
				else vis.Remove(_.i);
			}
			M = Max(M, SumNorm(vis.Select(i => vs[i]).ToArray()));
		}
		Console.WriteLine(M);
	}

	static double SumNorm(int[][] vs)
	{
		double x = vs.Sum(v => v[0]);
		double y = vs.Sum(v => v[1]);
		return Sqrt(x * x + y * y);
	}
}
