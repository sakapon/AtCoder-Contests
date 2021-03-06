﻿using System;
using System.Linq;

class C
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		int n = h[0], m = h[1];
		var a = new int[n].Select(_ => read()).ToArray();

		var M = 0L;
		for (int j = 0; j < m; j++)
			for (int k = j + 1; k < m; k++)
				M = Math.Max(M, Enumerable.Range(0, n).Sum(i => (long)Math.Max(a[i][j], a[i][k])));
		Console.WriteLine(M);
	}
}
