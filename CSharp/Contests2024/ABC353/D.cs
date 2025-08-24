class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var r10 = Enumerable.Range(0, 11).ToArray();
		var rn = Enumerable.Range(0, n).ToArray();

		var r = rn.Sum(i => i * a[i] % M) % M;

		var dc = new int[11];
		var p10 = new long[11];
		p10[0] = 1;
		for (int i = 1; i < p10.Length; i++)
			p10[i] = p10[i - 1] * 10;

		Array.Reverse(a);
		foreach (var v in a)
		{
			r += v * r10.Sum(d => dc[d] * p10[d] % M) % M;
			dc[v.ToString().Length]++;
		}

		return r % M;
	}

	const long M = 998244353;
}
