using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLib10.SegTrees.SegTrees111;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int l, int r) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var c = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		// 各色に対して、最も右のインデックス
		var ris = new int[n + 1];
		Array.Fill(ris, -1);

		var st = new RSQTree(n);
		var ti = -1;
		var counts = new int[qc];

		foreach (var qi in Enumerable.Range(0, qc).OrderBy(qi => qs[qi].r))
		{
			var (l, r) = qs[qi];
			l--;

			while (ti < r - 1)
			{
				ti++;

				var ri = ris[c[ti]];
				if (ri != -1) st[ri]--;

				ris[c[ti]] = ti;
				st[ti]++;
			}

			counts[qi] = (int)st[l, r];
		}
		return string.Join("\n", counts);
	}
}
