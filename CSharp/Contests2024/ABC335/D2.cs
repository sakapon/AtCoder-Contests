class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var s = new SeqArray2<string>(n, n);
		var a = s.a;
		a[n * n / 2] = "T";
		a[0] = "1";

		var v = 0;
		var d = 1;

		for (int k = 2; k < n * n; k++)
		{
			var nv = v + d;

			if (Math.Abs(d) == 1)
			{
				if (v / n != nv / n || a[nv] != null)
				{
					d = Rotate(d);
					nv = v + d;
				}
			}
			else
			{
				if (!(0 <= nv && nv < n * n) || a[nv] != null)
				{
					d = Rotate(d);
					nv = v + d;
				}
			}

			a[v = nv] = k.ToString();
		}

		return string.Join("\n", s.Select(r => string.Join(" ", r)));

		int Rotate(int d)
		{
			if (d == 1) return n;
			if (d == n) return -1;
			if (d == -1) return -n;
			return 1;
		}
	}
}
