class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var cl = new int[n]
			.Select(_ => Console.ReadLine().Split())
			.Select(z => (c: z[0][0], l: long.Parse(z[1])))
			.ToArray();

		if (cl.Any(p => p.l > 100)) return "Too Long";
		if (cl.Sum(p => p.l) > 100) return "Too Long";
		return string.Join("", cl.Select(p => new string(p.c, (int)p.l)));
	}
}
