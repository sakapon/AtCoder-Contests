using System;
using System.Collections.Generic;
using System.Linq;
using static EulerLib8.Common;

class P079
{
	const string textUrl = "https://projecteuler.net/project/resources/p079_keylog.txt";
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = GetText(textUrl).Split('\n', StringSplitOptions.RemoveEmptyEntries)
			.ToArray();

		var vs = Enumerable.Range(0, 10).Select(v => new Vertex(v)).ToArray();

		foreach (var l in s)
		{
			for (int i = 1; i < l.Length; i++)
			{
				var from = l[i - 1] - '0';
				var to = l[i] - '0';
				if (vs[from].Edges.Add(vs[to]))
					vs[to].Indegree++;
			}
		}

		var r = new List<int>();
		var q = new Queue<Vertex>();
		foreach (var v in vs)
			if (v.Edges.Count != 0 && v.Indegree == 0) q.Enqueue(v);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			r.Add(v.Id);

			foreach (var nv in v.Edges)
				if (--nv.Indegree == 0) q.Enqueue(nv);
		}

		return string.Join("", r);
	}

	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges.Count} edges\}")]
	public class Vertex
	{
		public int Id { get; }
		public int Indegree { get; set; }
		public HashSet<Vertex> Edges { get; } = new HashSet<Vertex>();
		public Vertex(int id) { Id = id; }
	}
}
