class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();

		var l = new List<IntV>(Enumerable.Range(1, n).Select(i => new IntV(i, 0)));
		l.Reverse();
		var r = new List<IntV>();

		while (qc-- > 0)
		{
			var z = Console.ReadLine().Split();
			if (int.Parse(z[0]) == 1)
			{
				var c = z[1];
				var v = l[^1];
				if (c == "R") v += IntV.UnitX;
				else if (c == "L") v -= IntV.UnitX;
				else if (c == "U") v += IntV.UnitY;
				else v -= IntV.UnitY;
				l.Add(v);
			}
			else
			{
				var p = int.Parse(z[1]);
				r.Add(l[^p]);
			}
		}

		return string.Join("\n", r);
	}
}
