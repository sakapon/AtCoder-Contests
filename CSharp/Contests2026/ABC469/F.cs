using Oomph.Data.UF09Lib.UFs.v301;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var max = 1000000;
		var map = new int[max + 1];
		for (int v = 1; v <= n; v++)
			map[a[v - 1]] = v;

		var r = 0L;
		var uf = new UnionFind(n + 1);

		for (int x = max; x > 0; x--)
		{
			var v0 = 0;

			for (int p = x; p <= max; p += x)
			{
				var v = map[p];
				if (v == 0) continue;

				if (v0 == 0)
				{
					v0 = v;
					continue;
				}

				if (uf.Union(v0, v)) r += x;
			}
		}
		return r;
	}
}
