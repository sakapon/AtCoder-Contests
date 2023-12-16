class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var reps = new List<long> { 1 };
		for (int i = 0; i < 15; i++)
		{
			reps.Add(reps[i] * 10 + 1);
		}

		var r = new List<long>();
		for (int i = 0; i < reps.Count; i++)
		{
			for (int j = 0; j < reps.Count; j++)
			{
				for (int k = 0; k < reps.Count; k++)
				{
					r.Add(reps[i] + reps[j] + reps[k]);
				}
			}
		}
		return r.Distinct().OrderBy(x => x).ElementAt(n - 1);
	}
}
