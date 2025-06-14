class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var r = new List<int>();
		var a = Enumerable.Range(1, n).ToArray();
		var offset = 0;

		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				a[(q[1] - 1 + offset) % n] = q[2];
			}
			else if (q[0] == 2)
			{
				r.Add(a[(q[1] - 1 + offset) % n]);
			}
			else
			{
				offset += q[1];
				offset %= n;
			}
		}
		return string.Join("\n", r);
	}
}
