using CoderLib8.Values;

class D2
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

		var r = Enumerable.Range(0, n - 1).Select(i => new Rational(a[i + 1], a[i])).ToArray();
		return r.All(q => q == r[0]);
	}
}
