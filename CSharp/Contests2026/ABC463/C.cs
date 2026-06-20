class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		var qc = int.Parse(Console.ReadLine());
		var ts = Read();

		var r = new int[qc];

		var qq = new Queue<(int t, int qi)>(ts.Select((t, qi) => (t, qi)).OrderBy(p => p.t));

		foreach (var (h, l) in ps.OrderBy(p => -p.Item1))
		{
			while (qq.Count > 0 && qq.Peek().t < l)
			{
				var (_, qi) = qq.Dequeue();
				r[qi] = h;
			}
		}
		return string.Join("\n", r);
	}
}
