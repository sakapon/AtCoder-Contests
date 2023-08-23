using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var xset = new WBMultiSet<int>();
		var xorset = new WBMultiSet<int>();

		var r = new List<int>();

		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				var x = q[1];
				var node = xset.Add(x);

				var n0 = node.GetPrevious();
				var n1 = node.GetNext();

				if (n0 != null && n1 != null)
				{
					xorset.Remove(n0.Item ^ n1.Item);
				}

				if (n0 != null)
				{
					xorset.Add(n0.Item ^ x);
				}
				if (n1 != null)
				{
					xorset.Add(n1.Item ^ x);
				}
			}
			else if (q[0] == 2)
			{
				var x = q[1];
				var node = xset.GetLast(x);

				var n0 = node.GetPrevious();
				var n1 = node.GetNext();

				if (n0 != null && n1 != null)
				{
					xorset.Add(n0.Item ^ n1.Item);
				}

				if (n0 != null)
				{
					xorset.Remove(n0.Item ^ x);
				}
				if (n1 != null)
				{
					xorset.Remove(n1.Item ^ x);
				}

				xset.Remove(x);
			}
			else
			{
				r.Add(xorset.GetFirst().Item);
			}
		}

		return string.Join("\n", r);
	}
}
