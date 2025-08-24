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
			x ^= 1 << c - '0';
			counts[x]++;
		}
		return counts.Sum(c => c * (c - 1) / 2);
	}
}
