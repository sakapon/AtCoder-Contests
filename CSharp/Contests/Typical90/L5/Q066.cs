using System;

class Q066
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var r = 0D;

		for (int i = 0; i < n; i++)
		{
			var (l1, r1) = ps[i];

			for (int j = i + 1; j < n; j++)
			{
				var (l2, r2) = ps[j];

				var rij = 0;
				for (int k = l1; k <= r1; k++)
				{
					if (r2 < k)
					{
						rij += r2 - l2 + 1;
					}
					else if (l2 < k)
					{
						rij += k - l2;
					}
				}
				r += (double)rij / ((r1 - l1 + 1) * (r2 - l2 + 1));
			}
		}

		return r;
	}
}
