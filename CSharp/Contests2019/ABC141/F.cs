﻿using System;
using System.Linq;

class F
{
	static void Main()
	{
		var p2 = new long[60];
		for (long i = 0, v = 1; i < p2.Length; p2[i++] = v, v <<= 1) ;

		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();
		var s = a.Aggregate((x, y) => x ^ y);

		for (int p = 59, c = 0; p >= 0; p--)
		{
			var j = -1;
			for (int i = c; i < n; i++) if (a[i] >= p2[p]) { j = i; break; }
			if (j == -1) continue;
			Swap(a, c, j);
			for (int i = 0; i < n; i++)
			{
				var v = a[i] ^ a[c];
				if (v < a[i] && i != c) a[i] = v;
			}
			c++;
		}
		var r = a.Aggregate((x, y) => x ^ y);
		Console.WriteLine(r + (s ^ r));
	}

	static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }
}
