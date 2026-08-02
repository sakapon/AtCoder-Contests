static class E3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, qc) = Read3();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var dp = NewArray2<int>(h + 1, w + 1);
		var xs = new char[qc + 1];
		xs[0] = 'A';

		for (int qi = 1; qi <= qc; qi++)
		{
			var q = qs[qi - 1];
			var r = int.Parse(q[0]) - 1;
			var c = int.Parse(q[1]) - 1;
			var x = q[2][0];

			dp[r][c] = qi;
			xs[qi] = x;
		}

		for (int i = h - 1; i >= 0; i--)
			for (int j = w - 1; j >= 0; j--)
			{
				dp[i][j].Chmax(dp[i + 1][j]);
				dp[i][j].Chmax(dp[i][j + 1]);
			}

		var result = dp[..h].Select(row => string.Join("", row[..w].Select(qi => xs[qi])));
		return string.Join("\n", result);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	static int Chmax(this ref int x, int v) => x < v ? x = v : x;
}
