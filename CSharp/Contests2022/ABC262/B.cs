using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[m], _ => Read());

		var u = new bool[n + 1, n + 1];
		foreach (var p in ps)
		{
			u[p[0], p[1]] = true;
		}

		var r = 0;
		for (int a = 1; a <= n; a++)
		{
			for (int b = a + 1; b <= n; b++)
			{
				if (!u[a, b]) continue;
				for (int c = b + 1; c <= n; c++)
				{
					if (u[b, c] && u[a, c]) r++;
				}
			}
		}
		return r;
	}
}
