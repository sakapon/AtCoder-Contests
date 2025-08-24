class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var l = new List<int> { -1 };

		foreach (var x in a)
		{
			l.Add(x);

			while (l[^1] == l[^2])
			{
				l.RemoveAt(l.Count - 1);
				l[^1]++;
			}
		}
		return l.Count - 1;
	}
}
