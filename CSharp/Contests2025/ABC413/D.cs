class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve() ? "Yes" : "No")));
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		Array.Sort(a);
		a = a.OrderBy(Math.Abs).ToArray();

		if (a[0] == -a[^1])
		{
			var c0 = a.Count(x => x == a[0]);
			return Math.Abs(n - 2 * c0) <= 1;
		}

		var r = Enumerable.Range(0, n - 1).Select(i => (n: a[i + 1], d: a[i])).ToArray();
		return Enumerable.Range(0, n - 2).All(i => r[i].n * r[i + 1].d == r[i].d * r[i + 1].n);
	}
}
