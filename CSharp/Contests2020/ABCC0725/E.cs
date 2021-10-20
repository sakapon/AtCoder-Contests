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
		var nsMin = NewArray2(n, 1 << n, 1L << 30);
		var ewMin = NewArray2(n, 1 << n, 1L << 30);

		for (int i = 0; i < n; i++)
		{
			nsMin[i][0] = Math.Abs(ps[i].x);
			ewMin[i][0] = Math.Abs(ps[i].y);
		}

		for (int k = 0; k < n; k++)
		{
			var nsk = nsMin[k];
			var ewk = ewMin[k];
			var (xk, yk, _) = ps[k];

			for (int x = 0; x < 1 << n; x++)
			{
				for (int i = 0; i < n; i++)
				{
					var f = 1 << i;
					if ((x & f) == 0)
					{
						var n_ns = Math.Min(nsk[x], Math.Abs(ps[i].x - xk));
						var n_ew = Math.Min(ewk[x], Math.Abs(ps[i].y - yk));
						nsk[x | f] = Math.Min(nsk[x | f], n_ns);
						ewk[x | f] = Math.Min(ewk[x | f], n_ew);
					}
				}
			}
		}

		var r = NewArray1(n + 1, 1L << 60);
		var k3 = 0;
		var x3 = new int[3];

		Power(new[] { 0, 1, 2 }, n, p =>
		{
			k3 = 0;
			Array.Clear(x3, 0, 3);

			for (int i = 0; i < n; i++)
			{
				if (p[i] != 0) k3++;
				x3[p[i]] |= 1 << i;
			}

			var sum = 0L;
			for (int i = 0; i < n; i++)
			{
				sum += Math.Min(nsMin[i][x3[1]], ewMin[i][x3[2]]) * ps[i].p;
			}
			r[k3] = Math.Min(r[k3], sum);
		});

		return string.Join("\n", r);
	}

	static T[] NewArray1<T>(int n, T v = default) => Array.ConvertAll(new bool[n], _ => v);
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

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
