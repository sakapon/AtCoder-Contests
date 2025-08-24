using AlgorithmLib10.SegTrees.SegTrees111;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var vs = new List<int>();
		var cs = new List<int>();

		var monoid = new Monoid<(int v1, int c1, int v2, int c2)>((x, y) =>
		{
			vs.Clear();
			cs.Clear();

			vs.Add(x.v1);
			cs.Add(x.c1);
			vs.Add(x.v2);
			cs.Add(x.c2);

			if (vs[0] < y.v1)
			{
				vs.Insert(0, y.v1);
				cs.Insert(0, y.c1);

				if (vs[1] < y.v2)
				{
					vs[1] = y.v2;
					cs[1] = y.c2;
				}
				else if (vs[1] == y.v2)
				{
					cs[1] += y.c2;
				}
			}
			else if (vs[0] == y.v1)
			{
				cs[0] += y.c1;

				if (vs[1] < y.v2)
				{
					vs[1] = y.v2;
					cs[1] = y.c2;
				}
				else if (vs[1] == y.v2)
				{
					cs[1] += y.c2;
				}
			}
			else if (vs[1] < y.v1)
			{
				vs[1] = y.v1;
				cs[1] = y.c1;
			}
			else if (vs[1] == y.v1)
			{
				cs[1] += y.c1;
			}

			return (vs[0], cs[0], vs[1], cs[1]);
		},
		(0, 0, -1, 0));

		var st = new MergeTree<(int, int, int, int)>(n, monoid);
		for (int i = 0; i < n; i++)
		{
			st[i] = (a[i], 1, -1, 0);
		}

		var res = new List<int>();

		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				var i = q[1] - 1;
				var x = q[2];
				st[i] = (x, 1, -1, 0);
			}
			else
			{
				var l = q[1] - 1;
				var r = q[2];
				res.Add(st[l, r].Item4);
			}
		}

		return string.Join("\n", res);
	}
}
