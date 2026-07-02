using CoderLib8.Collections.Dynamics.Int;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		if (a.DistinctBy(x => x % n).Count() != n) return 0;

		var r = 1L;
		var p = 1L;

		for (long i = 2; i < n; i++)
		{
			p *= i;
			p %= n;
			r *= p;
			r %= n;
		}
		if (r == 0) return 0;

		Array.Sort(a);
		Array.Reverse(a);

		var count = 0L;
		var set = new IntSegmentCountSet(n, Enumerable.Repeat(1L, n).ToArray());

		foreach (var x in a)
		{
			var xn = x % n;
			count += set.GetCountGeq(xn + 1);
			set.Remove(xn);
		}
		return count % 2 == 0 ? r : n - r;
	}
}
