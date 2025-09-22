class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var s = Console.ReadLine().ToCharArray();
		var t = Console.ReadLine();

		var q = new Queue<int>(Enumerable.Range(0, m).OrderBy(i => -t[i]));
		var mj = -1;

		for (int i = 0; i < n; i++)
		{
			if (q.Count == 0) break;
			var j = q.Peek();
			if (s[i] >= t[j]) continue;
			s[i] = t[j];
			q.Dequeue();
			if (mj < j) mj = j;
		}

		if (q.Count > 0)
		{
			var qj = q.Max();
			if (mj < qj) s[n - 1] = t[qj];
		}
		return new string(s);
	}
}
