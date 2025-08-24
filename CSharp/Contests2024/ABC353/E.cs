class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ss = Console.ReadLine().Split();

		Array.Sort(ss);

		var r = 0L;
		var sub = 0L;
		var lc = new int[300000];
		var lmax = -1;

		for (int i = 1; i < n; i++)
		{
			var len = Prefix(ss[i - 1], ss[i]);

			for (int j = lmax - 1; j >= len; j--)
			{
				sub -= lc[j + 1];
				lc[j] += lc[j + 1];
				lc[j + 1] = 0;
			}

			sub += len;
			lc[len]++;
			lmax = len;
			r += sub;
		}
		return r;
	}

	static int Prefix(string s, string t)
	{
		var n = Math.Min(s.Length, t.Length);
		for (int i = 0; i < n; i++)
			if (s[i] != t[i]) return i;
		return n;
	}
}
