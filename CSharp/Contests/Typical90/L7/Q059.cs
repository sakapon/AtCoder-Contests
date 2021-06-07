using System;
using System.Collections;
using System.Collections.Generic;

class Q059
{
	const int Buffer = 1 << 14;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
	static void Main()
	{
		var (n, m, qc) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, true);
		var u = Array.ConvertAll(new bool[n + 1], _ => new BitArray(Buffer));
		var bs = new int[Buffer];

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc > 0)
		{
			var c = Math.Min(qc, Buffer);
			qc -= c;

			for (int v = 1; v <= n; v++)
			{
				u[v].SetAll(false);
			}

			for (int i = 0; i < c; i++)
			{
				var (a, b) = Read2();
				u[a][i] = true;
				bs[i] = b;
			}

			for (int v = 1; v < n; v++)
			{
				foreach (var nv in map[v])
				{
					u[nv].Or(u[v]);
				}
			}

			for (int i = 0; i < c; i++)
			{
				var b = bs[i];
				WriteYesNo(u[b][i]);
			}
		}
		Console.Out.Flush();
	}

	static List<int>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
