class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var ls = Array.ConvertAll(new bool[4], _ => new List<long>());

		foreach (var (x, y) in ps)
		{
			var u = x + y;
			var v = x - y;

			if (u % 2 == 0)
			{
				ls[0].Add(u);
				ls[1].Add(v);
			}
			else
			{
				ls[2].Add(u);
				ls[3].Add(v);
			}
		}
		return ls.Sum(l => Sum(l.ToArray())) >> 1;
	}

	static long Sum(long[] a)
	{
		Array.Sort(a);

		var r = 0L;
		var s = 0L;
		var c = 0;

		foreach (var v in a)
		{
			r += v * c - s;
			s += v;
			c++;
		}
		return r;
	}
}
