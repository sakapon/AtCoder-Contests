class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var r = new List<int>();
		var a = new List<int>();

		foreach (var (t, x) in qs)
		{
			if (t == 1)
			{
				a.Add(x);
			}
			else
			{
				r.Add(a[^x]);
			}
		}
		return string.Join("\n", r);
	}
}
