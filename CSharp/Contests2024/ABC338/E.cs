class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var l = Enumerable.Range(1, 2 * n - 1).Prepend(2 * n).Prepend(-1).ToArray();
		var r = Enumerable.Range(2, 2 * n - 1).Append(1).Prepend(-1).ToArray();

		void Remove(int i)
		{
			var li = l[i];
			var ri = r[i];
			r[li] = ri;
			l[ri] = li;
		}

		int Dist(int a, int b)
		{
			var d = Math.Abs(a - b);
			return Math.Min(d, 2 * n - d);
		}

		var qs = ps.OrderBy(p => Dist(p.a, p.b));
		foreach (var (a, b) in qs)
		{
			if (!(r[a] == b || l[a] == b)) return true;

			Remove(a);
			Remove(b);
		}
		return false;
	}
}
