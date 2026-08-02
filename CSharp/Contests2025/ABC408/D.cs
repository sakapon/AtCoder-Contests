class D
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var dpl = new int[n + 1];
		var c = 0;

		for (int i = 0; i < n; i++)
		{
			if (s[i] == '0')
			{
				var d = c > 0 ? 1 : 0;
				dpl[i + 1] = dpl[i] + d;
				if (--c < 0) c = 0;
			}
			else
			{
				dpl[i + 1] = dpl[i];
				c++;
			}
		}

		var dpr = new int[n + 1];
		c = 0;

		for (int i = n - 1; i >= 0; i--)
		{
			if (s[i] == '0')
			{
				var d = c > 0 ? 1 : 0;
				dpr[i] = dpr[i + 1] + d;
				if (--c < 0) c = 0;
			}
			else
			{
				dpr[i] = dpr[i + 1];
				c++;
			}
		}

		return Enumerable.Range(0, n + 1).Min(i => dpl[i] + dpr[i]);
	}
}
