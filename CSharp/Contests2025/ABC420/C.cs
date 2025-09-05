class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = Read();
		var b = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var rn = Enumerable.Range(0, n).ToArray();
		var s = rn.Sum(i => (long)Math.Min(a[i], b[i]));

		var r = new List<long>();

		foreach (var q in qs)
		{
			var x = int.Parse(q[1]) - 1;
			var v = int.Parse(q[2]);

			s -= Math.Min(a[x], b[x]);
			if (q[0][0] == 'A')
				a[x] = v;
			else
				b[x] = v;
			s += Math.Min(a[x], b[x]);

			r.Add(s);
		}

		return string.Join("\n", r);
	}
}
