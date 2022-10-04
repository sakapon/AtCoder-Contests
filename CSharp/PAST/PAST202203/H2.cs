using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoderLib6.Trees;

class H2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var sb = new StringBuilder();

		var uf = new UF(n + 1);
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		var l = new List<int>();

		while (qc-- > 0)
		{
			var q = Read();
			if (q[0] == 1)
			{
				if (!uf.Unite(q[1], q[2])) continue;
				map[q[1]].Add(q[2]);
				map[q[2]].Add(q[1]);
			}
			else
			{
				l.Clear();
				DFS(q[1], -1);
				l.Sort();
				sb.AppendLine(string.Join(" ", l));

				void DFS(int v, int pv)
				{
					l.Add(v);
					foreach (var nv in map[v])
					{
						if (nv == pv) continue;
						DFS(nv, v);
					}
				}
			}
		}
		Console.Write(sb);
	}
}
