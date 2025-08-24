class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<string>();

		for (int x = 0; x <= n; x++)
		{
			for (int y = 0; y <= n; y++)
			{
				for (int z = 0; z <= n; z++)
				{
					if (x + y + z <= n) r.Add($"{x} {y} {z}");
				}
			}
		}
		return string.Join("\n", r);
	}
}
