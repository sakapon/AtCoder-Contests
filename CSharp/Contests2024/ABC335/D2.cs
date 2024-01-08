class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var s = new SeqArray2<string>(n, n);
		s.a[n * n / 2] = "T";
		s.a[0] = "1";

		var v = 0;
		var d = 1;

		for (int k = 2; k < n * n; k++)
		{
			var nv = v + d;

			if (Math.Abs(d) == 1)
			{
				if (v / n != nv / n || s.a[nv] != null)
				{
					d = Rotate(d);
					nv = v + d;
				}
			}
			else
			{
				if (!(0 <= nv && nv < n * n) || s.a[nv] != null)
				{
					d = Rotate(d);
					nv = v + d;
				}
			}

			s.a[v = nv] = k.ToString();
		}

		return string.Join("\n", Enumerable.Range(0, n).Select(i => string.Join(" ", s[i])));

		int Rotate(int d)
		{
			if (d == 1) return n;
			if (d == n) return -1;
			if (d == -1) return -n;
			return 1;
		}
	}
}
