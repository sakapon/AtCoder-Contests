class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = 0L;

		for (int k = 0; k < 30; k++)
		{
			var f = 1 << k;

			var b = a[0] & f;
			var s = 0L;
			for (int i = 1; i < n; i++)
			{
				b ^= a[i] & f;
				s += b;
			}

			for (int i = 0; i < n - 1; i++)
			{
				r += s;
				if ((a[i] & f) != 0) s = (long)(n - 1 - i) * f - s;
				s -= a[i + 1] & f;
			}
		}
		return r;
	}
}
