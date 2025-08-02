class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = 0L;
		var c = new int[400000];

		for (int j = 0; j < n; j++)
		{
			var v = a[j] - j;
			if (v < 0) r += c[-v];
			c[a[j] + j]++;
		}
		return r;
	}
}
