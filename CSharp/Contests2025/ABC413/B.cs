class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new int[n].Select(_ => Console.ReadLine()).ToArray();

		var set = new HashSet<string>();

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (i == j) continue;

				set.Add(s[i] + s[j]);
			}
		}
		Console.WriteLine(set.Count);
	}
}
