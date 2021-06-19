using System;
using System.Linq;
using CoderLib6.Trees;

class Q068
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int t, int x, int y, int v) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read4());

		var roots = new int[n + 1];
		var d = new long[n + 1];

		foreach (var (_, x, y, v) in qs.Where(q => q.t == 0).OrderBy(q => q.x))
		{
			if (roots[x] == -1) roots[x] = x;

			roots[y] = roots[x];
			d[y] = v - d[x];
		}

		var uf = new UF(n + 1);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (t, x, y, v) in qs)
		{
			if (t == 0)
			{
				uf.Unite(x, y);
			}
			else
			{
				if (uf.AreUnited(x, y))
				{
					var v0 = (x - roots[x]) % 2 == 0 ? v - d[x] : d[x] - v;
					var vy = (y - roots[y]) % 2 == 0 ? d[y] + v0 : d[y] - v0;
					Console.WriteLine(vy);
				}
				else
				{
					Console.WriteLine("Ambiguous");
				}
			}
		}
		Console.Out.Flush();
	}
}
