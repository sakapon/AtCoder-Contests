using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var es = Array.ConvertAll(new bool[h[1]], _ => Read());

		var r = BellmanFord(h[0], es, h[2]);
		if (r.Item1 == null) { Console.WriteLine("NEGATIVE CYCLE"); return; }
		Console.WriteLine(string.Join("\n", r.Item1.Select(x => x == long.MaxValue ? "INF" : $"{x}")));
	}

	static Tuple<long[], int[][]> BellmanFord(int n, int[][] des, int sv)
	{
		var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var inEdges = new int[n][];
		cs[sv] = 0;

		var next = true;
		for (int k = 0; k < n && next; ++k)
		{
			next = false;
			foreach (var e in des)
			{
				if (cs[e[0]] == long.MaxValue || cs[e[1]] <= cs[e[0]] + e[2]) continue;
				cs[e[1]] = cs[e[0]] + e[2];
				inEdges[e[1]] = e;
				next = true;
			}
		}
		if (next) return Tuple.Create<long[], int[][]>(null, null);
		return Tuple.Create(cs, inEdges);
	}
}
