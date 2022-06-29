using System;
using System.Collections.Generic;
using System.Linq;

class CH
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var map = Array.ConvertAll(new bool[n], _ => Read().Skip(1).ToArray());
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var hld = new HLD(n, map, 0);

		return string.Join("\n", qs.Select(q =>
		{
			var nu = hld.Nodes[q[0]];
			var nv = hld.Nodes[q[1]];

			while (nu.Group != nv.Group)
			{
				if (nu.Group.Depth < nv.Group.Depth)
				{
					nv = nv.Group.Root.Parent;
				}
				else
				{
					nu = nu.Group.Root.Parent;
				}
			}
			return nu.Depth < nv.Depth ? nu.Id : nv.Id;
		}));
	}
}
