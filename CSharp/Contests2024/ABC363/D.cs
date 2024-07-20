class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		if (n <= 10) return n - 1;

		var maxDeg = 36;
		var a = new long[maxDeg];
		a[0] = 10;
		a[1] = 9;
		a[2] = 90;

		for (int i = 3; i < maxDeg; i++)
		{
			a[i] = a[i - 2] * 10;
		}

		var s = new long[maxDeg + 1];
		for (int i = 1; i <= maxDeg; i++)
		{
			s[i] = s[i - 1] + a[i - 1];
		}

		var deg = Enumerable.Range(0, maxDeg + 1).First(i => n <= s[i]);
		n -= s[deg - 1];
		n--;

		var x0 = n.ToString().PadLeft((deg + 1) / 2, '0').ToCharArray();
		x0[0]++;
		var q = x0.Concat(deg % 2 == 0 ? x0.Reverse() : x0[..^1].Reverse());
		return string.Join("", q);
	}
}
