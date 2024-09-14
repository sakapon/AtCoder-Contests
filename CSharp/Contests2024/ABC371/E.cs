class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var p = new int[n + 1];
		Array.Fill(p, -1);

		var t = 0L;
		var r = 0L;

		for (int i = 0; i < n; i++)
		{
			t += i - p[a[i]];
			r += t;
			p[a[i]] = i;
		}
		return r;
	}
}
