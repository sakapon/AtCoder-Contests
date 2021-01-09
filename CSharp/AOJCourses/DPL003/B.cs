using System;
using System.Collections.Generic;

class B
{
	struct P
	{
		public int i, length;
		public P(int _i, int _l) { i = _i; length = _l; }
	}

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var z = Read();
		int h = z[0], w = z[1];
		var c = Array.ConvertAll(new int[h], _ => Read());

		// Histogram
		var a = new int[w];
		var M = 0L;

		for (int i = 0; i < h; i++)
		{
			var q = new Stack<P>();

			for (int j = 0; j < w; j++)
			{
				if (c[i][j] == 0) a[j]++;
				else a[j] = 0;

				var p = new P(j, a[j]);
				while (q.Count > 0 && q.Peek().length > a[j])
				{
					p = q.Pop();
					M = Math.Max(M, (long)(j - p.i) * p.length);
				}

				if (q.Count == 0 || q.Peek().length < a[j])
					q.Push(new P(p.i, a[j]));
			}

			// right bound
			while (q.Count > 0)
			{
				var p = q.Pop();
				M = Math.Max(M, (long)(w - p.i) * p.length);
			}
		}
		Console.WriteLine(M);
	}
}
