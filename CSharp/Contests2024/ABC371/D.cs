using AlgorithmLib10.SegTrees.SegTrees214;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var x = Read();
		var p = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var rsq_ = new Int32RSQTree();
		var rsq = new Int32RSQTree();

		for (int i = 0; i < n; i++)
		{
			if (x[i] >= 0)
			{
				rsq.Add(x[i], p[i]);
			}
			else
			{
				rsq_.Add(-x[i], p[i]);
			}
		}

		var res = qs.Select(q =>
		{
			var (l, r) = q;
			return
				(l < 0 ? rsq_[Math.Max(1, -r), -l + 1] : 0) +
				(r >= 0 ? rsq[Math.Max(0, l), r + 1] : 0);
		});
		return string.Join("\n", res);
	}
}
