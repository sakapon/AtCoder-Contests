using AlgorithmLab.Collections.Arrays301;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var r = 0L;

		var map = new ArrayHashMap<long, long>();
		var mapt = new ArrayHashMap<long, long>();
		map[n] = 1;

		while (map.Count > 0)
		{
			foreach (var (k, v) in map)
			{
				r += k * v;

				if (k % 2 == 0)
				{
					if (k == 2) continue;
					mapt[k / 2] += v * 2;
				}
				else
				{
					if (k == 3)
					{
						mapt[2] += v;
					}
					else
					{
						mapt[k / 2] += v;
						mapt[k / 2 + 1] += v;
					}
				}
			}

			(map, mapt) = (mapt, map);
			mapt.Clear();
		}

		return r;
	}
}
