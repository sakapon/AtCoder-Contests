using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmLab.DataTrees.UF411;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var sb = new StringBuilder();

		var uf = new QuickFind(n + 1);

		while (qc-- > 0)
		{
			var q = Read();
			if (q[0] == 1)
			{
				uf.Union(q[1], q[2]);
			}
			else
			{
				sb.AppendLine(string.Join(" ", uf.Find(q[1]).OrderBy(v => v)));
			}
		}
		Console.Write(sb);
	}
}
