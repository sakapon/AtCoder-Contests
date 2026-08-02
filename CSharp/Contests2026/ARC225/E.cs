class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();

		return p.Select((x, i) => x - 1 - i).Sum(x => (long)Math.Abs(x)) - InversionNumber(p);
	}

	public static long InversionNumber<T>(T[] a, IComparer<T> c = null)
	{
		var n = a.Length;
		var t = new T[n];
		c = c ?? Comparer<T>.Default;
		var r = 0L;

		for (int k = 1; k < n; k <<= 1)
		{
			var ti = 0;
			for (int L = 0; L < n; L += k << 1)
			{
				int R1 = L | k, R2 = R1 + k;
				if (R2 > n) R2 = n;
				int i1 = L, i2 = R1;
				while (ti < R2)
				{
					if (i2 >= R2 || i1 < R1 && i2 < R2 && c.Compare(a[i1], a[i2]) <= 0)
					{
						t[ti++] = a[i1++];
					}
					else
					{
						r += R1 - i1;
						t[ti++] = a[i2++];
					}
				}
			}
			Array.Copy(t, a, n);
		}
		return r;
	}
}
