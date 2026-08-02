class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var random = new Random();
		var rn = Enumerable.Range(0, n).ToArray();

		for (int k = 0; k < 30; k++)
		{
			// 相異なる i, j
			var rn2 = (int[])rn.Clone();
			var i = random.Next(n);
			rn2[i] = n - 1;
			var j = rn2[random.Next(n - 1)];

			var (x1, y1) = ps[i];
			var (x2, y2) = ps[j];

			var a = y2 - y1;
			var b = x1 - x2;
			var c = x2 * y1 - x1 * y2;

			if (Check(a, b, c))
				return $"Yes\n{a} {b} {c}";
		}
		return "No";

		bool Check(long a, long b, long c)
		{
			return ps.Count(p => a * p.x + b * p.y + c == 0) > n >> 1;
		}
	}
}
