using System;
using System.Collections.Generic;
using System.Linq;

// https://github.com/sakapon/Documents/blob/master/Software/Algorithms/BFS-DFS-Viewer.md
class A240223
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var sseq = s.SelectMany(a => a).ToArray();
		var sv = Array.IndexOf(sseq, 's');
		var ev = Array.IndexOf(sseq, 'g');

		var o = new A240223 { h = h, w = w, n = h * w, s = sseq };
		var r = o.BFSByQueue(sv);
		return r[ev] != -1;
	}

	int h, w, n;
	char[] s;

	public int[] BFSByQueue(int sv)
	{
		var d = new int[n];
		Array.Fill(d, -1);
		d[sv] = 0;

		var q = new Queue<int>();
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();

			foreach (var nv in GetNexts(v))
			{
				if (d[nv] != -1) continue;
				d[nv] = d[v] + 1;
				q.Enqueue(nv);
			}
		}
		return d;
	}

	public int[] DFSByStack(int sv)
	{
		var d = new int[n];
		Array.Fill(d, -1);
		d[sv] = 0;

		var q = new Stack<int>();
		q.Push(sv);

		while (q.Count > 0)
		{
			var v = q.Pop();

			foreach (var nv in GetNexts(v))
			{
				if (d[nv] != -1) continue;
				d[nv] = d[v] + 1;
				q.Push(nv);
			}
		}
		return d;
	}

	public int[] DFSByRec(int sv)
	{
		var d = new int[n];
		Array.Fill(d, -1);
		d[sv] = 0;

		Rec(sv);
		return d;

		void Rec(int v)
		{
			foreach (var nv in GetNexts(v))
			{
				if (d[nv] != -1) continue;
				d[nv] = d[v] + 1;
				Rec(nv);
			}
		}
	}

	IEnumerable<int> GetNexts(int v)
	{
		var (i, j) = (v / w, v % w);
		if (i > 0 && s[v - w] != '#') yield return v - w;
		if (j > 0 && s[v - 1] != '#') yield return v - 1;
		if (i + 1 < h && s[v + w] != '#') yield return v + w;
		if (j + 1 < w && s[v + 1] != '#') yield return v + 1;
	}
}
