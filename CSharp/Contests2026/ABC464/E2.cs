class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, qc) = Read3();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var s = NewArray2(h, w, '/');

		Array.Reverse(qs);
		foreach (var q in qs)
		{
			var r = int.Parse(q[0]);
			var c = int.Parse(q[1]);
			var x = q[2][0];

			for (int i = r - 1; i >= 0; i--)
			{
				if (s[i][c - 1] != '/') break;
				for (int j = c - 1; j >= 0; j--)
				{
					if (s[i][j] != '/') break;
					s[i][j] = x;
				}
			}
		}

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				if (s[i][j] == '/') s[i][j] = 'A';

		return string.Join("\n", s.Select(cs => new string(cs)));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
