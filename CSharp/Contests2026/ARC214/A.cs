class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine().ToCharArray());

		var cs = new char[2 * n];
		Array.Fill(cs, '?');

		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
			{
				var k = i + j;
				if (s[i][j] == '?') continue;

				if (cs[k] == '?') cs[k] = s[i][j];
				else if (cs[k] != s[i][j]) return -1;
			}

		cs = cs.Select(c => c == '?' ? '0' : c).ToArray();

		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
			{
				var k = i + j;
				s[i][j] = cs[k];
			}

		return string.Join("\n", s.Select(r => new string(r)));
	}
}
