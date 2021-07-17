using System;
using System.Linq;
using CoderLib6.Trees;

class Q012
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var qc = int.Parse(Console.ReadLine());

		h += 2;
		w += 2;

		int ToId(int i, int j) => i * w + j;
		int[] Nexts(int id) => new[] { id + 1, id - 1, id + w, id - w };

		var u = new bool[h * w];
		var uf = new UF(h * w);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int k = 0; k < qc; k++)
		{
			var q = Read();
			if (q[0] == 1)
			{
				var (i, j) = (q[1], q[2]);
				var p = ToId(i, j);

				u[p] = true;
				foreach (var np in Nexts(p))
					if (u[np]) uf.Unite(p, np);
			}
			else
			{
				var (ai, aj, bi, bj) = (q[1], q[2], q[3], q[4]);
				var a = ToId(ai, aj);
				var b = ToId(bi, bj);
				Console.WriteLine(u[a] && u[b] && uf.AreUnited(a, b) ? "Yes" : "No");
			}
		}
		Console.Out.Flush();
	}
}
