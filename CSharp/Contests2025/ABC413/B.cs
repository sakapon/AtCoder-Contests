class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var set = new HashSet<string>();

		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
			{
				set.Add(s[i] + s[j]);
				set.Add(s[j] + s[i]);
			}
		return set.Count;
	}
}
