class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var setx = Array.ConvertAll(new bool[h + 1], _ => new HashSet<int>());
		var sety = Array.ConvertAll(new bool[w + 1], _ => new HashSet<int>());

		for (int id = 0; id < n; id++)
		{
			var (x, y) = ps[id];
			setx[x].Add(id);
			sety[y].Add(id);
		}

		var r = new List<int>();
		foreach (var (t, v) in qs)
		{
			if (t == 1)
			{
				var set = setx[v];
				r.Add(set.Count);
				foreach (var id in set)
					sety[ps[id].y].Remove(id);
				set.Clear();
			}
			else
			{
				var set = sety[v];
				r.Add(set.Count);
				foreach (var id in set)
					setx[ps[id].x].Remove(id);
				set.Clear();
			}
		}
		return string.Join("\n", r);
	}
}
