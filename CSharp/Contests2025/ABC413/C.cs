using AlgorithmLab.Collections.Arrays301;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var r = new List<long>();
		var q = new ArrayDeque<(long x, int c)>();

		foreach (var z in qs)
		{
			if (z[0] == 1)
			{
				q.AddLast((z[2], z[1]));
			}
			else
			{
				var k = z[1];
				var s = 0L;

				while (k > 0)
				{
					var (x0, c0) = q[0];

					if (k < c0)
					{
						q[0] = (x0, c0 - k);
						s += x0 * k;
						k = 0;
					}
					else
					{
						q.PopFirst();
						s += x0 * c0;
						k -= c0;
					}
				}
				r.Add(s);
			}
		}
		return string.Join("\n", r);
	}
}
