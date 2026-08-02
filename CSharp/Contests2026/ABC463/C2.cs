using WBTrees;

class C2
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
		var set = new WBMultiSet<int>();
		var qq = ts.Select((t, qi) => (t, qi, -1)).ToList();

		foreach (var (h, l) in ps)
		{
			set.Add(h);
			qq.Add((l, -1, h));
		}

		foreach (var (t, qi, h) in qq.Order())
		{
			if (qi < 0)
				set.Remove(h);
			else
				r[qi] = set.GetLast().Item;
		}
		return string.Join("\n", r);
	}
}
