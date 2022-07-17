using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = Read()[0];
		var map = Array.ConvertAll(new bool[n], _ => Read().Skip(1).ToArray());
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var hld = new HLD(n, map, 0);
		var st = new LST<long, long>(n,
			(x, y) => x + y, 0,
			(x, y) => x + y, 0,
			(x, p, _, l) => p + x * l);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			if (q[0] == 0)
			{
				var (v, w) = (hld.Nodes[q[1]], q[2]);

				while (v != null)
				{
					var gr = v.Group.Root;
					st.Set(gr.Order, v.Order + 1, w);
					v = gr.Parent;
				}
			}
			else
			{
				var v = hld.Nodes[q[1]];
				var r = -st.Get(hld.Root.Order);

				while (v != null)
				{
					var gr = v.Group.Root;
					r += st.Get(gr.Order, v.Order + 1);
					v = gr.Parent;
				}
				Console.WriteLine(r);
			}
		}
		Console.Out.Flush();
	}
}
