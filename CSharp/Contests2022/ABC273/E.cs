using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	class Node
	{
		public int Value;
		public Node Parent;
	}

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());

		var d = new Dictionary<int, Node>();
		var root = new Node { Value = -1 };
		var a = root;

		var r = new int[qc];

		for (int k = 0; k < qc; k++)
		{
			var q = Console.ReadLine().Split();

			var c = q[0][0];

			if (c == 'A')
			{
				var x = int.Parse(q[1]);
				var node = new Node { Value = x, Parent = a };
				a = node;
			}
			else if (c == 'D')
			{
				if (a.Parent != null) a = a.Parent;
			}
			else if (c == 'S')
			{
				var y = int.Parse(q[1]);
				d[y] = a;
			}
			else
			{
				var z = int.Parse(q[1]);
				if (d.ContainsKey(z))
				{
					a = d[z];
				}
				else
				{
					a = root;
				}
			}

			r[k] = a.Value;
		}

		return string.Join(" ", r);
	}
}
