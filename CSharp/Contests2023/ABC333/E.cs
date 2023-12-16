class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[n], _ => Read2());

		var l = new List<int>();
		var k = 0;
		var c = 0;
		var p = new int[n + 1];

		Array.Reverse(qs);
		foreach (var (t, x) in qs)
		{
			if (t == 1)
			{
				if (p[x] == 0)
				{
					l.Add(0);
				}
				else
				{
					l.Add(1);
					c--;
					p[x]--;
				}
			}
			else
			{
				k = Math.Max(k, ++c);
				p[x]++;
			}
		}

		if (p.Any(x => x != 0)) return -1;
		l.Reverse();
		return $"{k}\n" + string.Join(" ", l);
	}
}
