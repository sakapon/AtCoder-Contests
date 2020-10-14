using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var rs = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var ys = rs.Select(r => r[1]).Concat(rs.Select(r => r[3])).Distinct().OrderBy(v => v).ToArray();
		var yd = Enumerable.Range(0, ys.Length).ToDictionary(i => ys[i]);

		var xqs = rs.Select(r => new { x = r[0], d = 1, y1 = yd[r[1]], y2 = yd[r[3]] })
			.Concat(rs.Select(r => new { x = r[2], d = -1, y1 = yd[r[1]], y2 = yd[r[3]] }))
			.OrderBy(q => q.x);

		// Segment Tree (Range) を使うと O(n^2 log n)。
		var c = new int[ys.Length];
		long sum = 0, xt = -1 << 30;

		foreach (var q in xqs)
		{
			if (xt < q.x)
			{
				for (int i = 0; i < ys.Length; i++)
					if (c[i] > 0) sum += (q.x - xt) * (ys[i + 1] - ys[i]);

				xt = q.x;
			}

			for (int i = q.y1; i < q.y2; i++)
				c[i] += q.d;
		}
		Console.WriteLine(sum);
	}
}
