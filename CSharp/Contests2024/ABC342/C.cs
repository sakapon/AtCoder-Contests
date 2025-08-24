class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().ToCharArray();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine());

		var gs = Array.ConvertAll(new bool[1 << 7], _ => new List<int>());

		for (int i = 0; i < n; i++)
		{
			gs[s[i]].Add(i);
		}

		foreach (var q in qs)
		{
			var c = q[0];
			var d = q[2];

			if (c == d) continue;
			if (gs[c].Count == 0) continue;

			if (gs[d].Count == 0)
			{
				(gs[c], gs[d]) = (gs[d], gs[c]);
			}
			else
			{
				gs[d].AddRange(gs[c]);
				gs[c].Clear();
			}
		}

		for (char c = 'a'; c <= 'z'; c++)
		{
			foreach (var i in gs[c])
			{
				s[i] = c;
			}
		}
		return new string(s);
	}
}
