class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var counts = new long[1 << 10];
		var x = 0;
		counts[x]++;

		foreach (var c in s)
		{
			var f = 1 << c - '0';

			if ((x & f) == 0) x |= f;
			else x &= ~f;

			counts[x]++;
		}
		return counts.Sum(c => c * (c - 1) / 2);
	}
}
