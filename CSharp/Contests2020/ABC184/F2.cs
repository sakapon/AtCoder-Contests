using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, t) = Read2();
		var a = Read();

		var n2 = n / 2;

		var s1 = CreateSums(a.Take(n2).ToArray(), t);
		var s2 = CreateSums(a.Skip(n2).ToArray(), t);

		Array.Sort(s1);
		Array.Sort(s2);
		Array.Reverse(s2);

		var r = 0;
		for (int i = 0, j = 0; i < s1.Length; i++)
		{
			while (s1[i] + s2[j] > t) j++;
			r = Math.Max(r, s1[i] + s2[j]);
		}
		Console.WriteLine(r);
	}

	static int[] CreateSums(int[] a, int t)
	{
		var l = new List<int> { 0 };

		for (int i = 0; i < a.Length; i++)
		{
			for (int j = l.Count - 1; j >= 0; j--)
			{
				var v = a[i] + l[j];
				if (v <= t) l.Add(v);
			}
		}
		return l.ToArray();
	}
}
