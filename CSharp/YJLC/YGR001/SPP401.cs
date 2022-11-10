using System;
using System.Collections.Generic;
using System.Linq;
using YGR001.Lib.Dijkstra401;

class SPP401
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, s, t) = Read4();
		var es = Array.ConvertAll(new bool[m], _ => Read3());

		var spp = new Dijkstra(n, es, false);
		spp.Execute(s, t);
		if (spp[t].Cost == long.MaxValue) return -1;

		var path = GetPathVertexes(spp[t]);
		return $"{spp[t].Cost} {path.Length - 1}\n" + string.Join("\n", path[1..].Select(v => $"{v.Previous.Id} {v.Id}"));
	}

	static Vertex[] GetPathVertexes(Vertex ev)
	{
		var path = new Stack<Vertex>();
		for (var v = ev; v != null; v = v.Previous)
			path.Push(v);
		return path.ToArray();
	}
}
