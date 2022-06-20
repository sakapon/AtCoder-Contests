using System;
using System.Collections.Generic;
using System.Linq;

class B3
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());
		var b = Array.ConvertAll(a, v => (int.Parse(v[0]), int.Parse(v[1]), v[2][0], long.Parse(v[3]), v[4]));
		MergeSort(b);
		return string.Join("\n", b.Select(p => $"{p.Item1} {p.Item2} {p.Item3} {p.Item4} {p.Item5}"));
	}

	static void MergeSort<T>(T[] a)
	{
		var c = Comparer<T>.Default;
		var n = a.Length;
		var t = new T[n];

		for (int k = 1; k < n; k <<= 1)
		{
			var ti = 0;
			for (int L = 0; L < n; L += k << 1)
			{
				int R1 = L | k, R2 = R1 + k;
				if (R2 > n) R2 = n;
				int i1 = L, i2 = R1;

				while (ti < R2)
					t[ti++] = (i2 >= R2 || i1 < R1 && i2 < R2 && c.Compare(a[i1], a[i2]) <= 0) ? a[i1++] : a[i2++];
			}
			Array.Copy(t, a, n);
		}
	}
}
