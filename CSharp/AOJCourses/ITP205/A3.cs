using System;
using System.Collections.Generic;
using System.Linq;

class A3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => Read2());
		MergeSort(a);
		return string.Join("\n", a.Select(p => $"{p.x} {p.y}"));
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
