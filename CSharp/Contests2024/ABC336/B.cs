class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var r = 0;
		while (n % 2 == 0)
		{
			n >>= 1;
			r++;
		}
		return r;
	}
}
