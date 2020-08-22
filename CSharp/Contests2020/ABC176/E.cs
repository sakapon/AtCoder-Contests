using System;
using System.Linq;

class E
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		int h = z[0], w = z[1], m = z[2];
		var ps = new int[m].Select(_ => Read()).Select(v => (i: v[0], j: v[1])).ToArray();

		var tate = new int[h + 1];
		var yoko = new int[w + 1];

		foreach (var (i, j) in ps)
		{
			tate[i]++;
			yoko[j]++;
		}

		var Mi = tate.Max();
		var Mj = yoko.Max();
		var mis = Enumerable.Range(0, h + 1).Where(i => tate[i] == Mi).ToHashSet();
		var mjs = Enumerable.Range(0, w + 1).Where(j => yoko[j] == Mj).ToHashSet();

		var r = Mi + Mj;
		if (ps.Count(p => mis.Contains(p.i) && mjs.Contains(p.j)) == mis.Count * mjs.Count) r--;
		Console.WriteLine(r);
	}
}
