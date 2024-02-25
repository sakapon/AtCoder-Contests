class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine());

		var cs = new char[1 << 7];
		for (char x = 'a'; x <= 'z'; x++)
			cs[x] = x;

		foreach (var q in qs)
		{
			var c = q[0];
			var d = q[2];

			for (char x = 'a'; x <= 'z'; x++)
				if (cs[x] == c)
					cs[x] = d;
		}
		return string.Join("", s.Select(c => cs[c]));
	}
}
