using System;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var p = Read();

		var r = new int[n - k + 1];
		var u = new bool[n + 1];
		var t = 0;

		for (int i = 0; i < k; i++)
		{
			u[p[i]] = true;
		}
		while (!u[t]) t++;
		r[0] = t;

		for (int i = k; i < n; i++)
		{
			u[p[i]] = true;
			if (t < p[i]) t++;
			while (!u[t]) t++;
			r[i - k + 1] = t;
		}
		return string.Join("\n", r);
	}
}
