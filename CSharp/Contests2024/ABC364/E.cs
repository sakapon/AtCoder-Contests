class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		const int max = 1 << 30;

		// i: 個数、j: 甘さ、値: しょっぱさ
		var dp = NewArray2(n + 1, x + 1, max);
		dp[0][0] = 0;

		foreach (var (a, b) in ps)
		{
			for (int i = n - 1; i >= 0; i--)
			{
				for (int j = 0; j < x; j++)
				{
					var nj = j + a;
					var nv = dp[i][j] + b;
					if (nj > x) continue;
					if (nv > y) continue;
					Chmin(ref dp[i + 1][nj], nv);
				}
			}
		}
		return Math.Min(n, Enumerable.Range(0, n + 1).Last(i => dp[i].Any(v => v < max)) + 1);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	public static int Chmin(ref int x, int v) => x > v ? x = v : x;
}
