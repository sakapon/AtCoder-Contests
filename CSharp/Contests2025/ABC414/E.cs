using CoderLib6.Values;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var r = new MInt(n) * (n + 1) / 2;
		var max = n;

		for (int i = 2; i <= 1000000; i++)
		{
			var q = n / i;
			r -= new MInt(max - q) * (i - 1);
			max = q;
		}

		for (; max > 0; max--)
		{
			r -= n / max;
		}

		return r;
	}
}
