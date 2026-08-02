class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var r = new int[n];
		Array.Fill(r, n);

		var c = 0;
		void Open()
		{
			if (c == n) return;
			c++;
			if (s[c - 1] == 'x') return;
			Open();
		}

		for (int i = 0; i < n; i++)
		{
			Open();
			if (c == n) break;
			r[i] = c;
		}

		return string.Join("\n", r);
	}
}
