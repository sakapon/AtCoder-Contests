class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var cs = new long[200001];
		foreach (var x in a)
			cs[x]++;

		for (int x = 200000; x > 0; x--)
		{
			if (cs[x] == 0) continue;

			for (int p = 2; p < 500; p++)
			{
				var p2 = p * p;

				if (x % p2 == 0)
				{
					cs[x / p2] += cs[x];
					cs[x] = 0;
					break;
				}
			}
		}

		var r = Enumerable.Range(1, (int)cs[0]).Sum(i => (long)n - i);

		for (int i = 1; i <= 200000; i++)
		{
			r += cs[i] * (cs[i] - 1) / 2;
		}
		return r;
	}
}
