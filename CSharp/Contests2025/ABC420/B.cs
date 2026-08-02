class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var p = new int[n + 1];
		var rn = Enumerable.Range(0, n).ToArray();

		for (int j = 0; j < m; j++)
		{
			var x = rn.Count(i => s[i][j] == '0');
			var y = n - x;

			var c = x < y ? '0' : '1';
			for (int i = 0; i < n; i++)
			{
				if (s[i][j] == c) p[i + 1]++;
			}
		}

		var max = p.Max();
		return string.Join(" ", Enumerable.Range(1, n).Where(v => p[v] == max));
	}
}
