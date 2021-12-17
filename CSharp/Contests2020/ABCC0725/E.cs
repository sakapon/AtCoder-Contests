using System;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y, long p) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read3L());

		// distances
		// i: town, j: combination
		var nsMin = new long[n][];
		var ewMin = new long[n][];

		for (int i = 0; i < n; i++)
		{
			nsMin[i] = NewArray1(1 << n, Math.Abs(ps[i].x));
			ewMin[i] = NewArray1(1 << n, Math.Abs(ps[i].y));
		}

		for (int i = 0; i < n; i++)
		{
			var nsi = nsMin[i];
			var ewi = ewMin[i];

			for (int j = 0; j < n; j++)
			{
				var f = 1 << j;
				var ns_ij = Math.Abs(ps[i].x - ps[j].x);
				var ew_ij = Math.Abs(ps[i].y - ps[j].y);

				for (int x = 0; x < 1 << n; x++)
				{
					if ((x & f) == 0) x |= f;

					nsi[x] = Math.Min(nsi[x], ns_ij);
					ewi[x] = Math.Min(ewi[x], ew_ij);
				}
			}
		}

		var r = NewArray1(n + 1, 1L << 60);
		var k = 0;
		var x3 = new int[3];

		Power(new[] { 0, 1, 2 }, n, p =>
		{
			k = 0;
			Array.Clear(x3, 0, 3);

			for (int i = 0; i < n; i++)
			{
				if (p[i] != 0) k++;
				x3[p[i]] |= 1 << i;
			}

			var sum = 0L;
			for (int i = 0; i < n; i++)
			{
				sum += Math.Min(nsMin[i][x3[1]], ewMin[i][x3[2]]) * ps[i].p;
			}
			r[k] = Math.Min(r[k], sum);
		});

		return string.Join("\n", r);
	}

	static T[] NewArray1<T>(int n, T v = default) => Array.ConvertAll(new bool[n], _ => v);

	public static void Power<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2);
				else action(p);
			}
		}
	}
}
