class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[m], _ => Console.ReadLine().Split());

		var u = new bool[n + 1];
		var r = new bool[m];

		for (int j = 0; j < m; j++)
		{
			var a = int.Parse(ps[j][0]);
			var b = ps[j][1];

			r[j] = !u[a] && b == "M";
			if (r[j]) u[a] = true;
		}

		return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
	}
}
