using System;
using System.Collections.Generic;
using System.Linq;

class DO
{
	class Node
	{
		public int id;
		public Node prev, next;
	}

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var nodes = Enumerable.Range(0, n + 1).Select(id => new Node { id = id }).ToArray();
		var r = new List<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				nodes[q[1]].next = nodes[q[2]];
				nodes[q[2]].prev = nodes[q[1]];
			}
			else if (q[0] == 2)
			{
				nodes[q[1]].next = null;
				nodes[q[2]].prev = null;
			}
			else
			{
				r.Clear();
				var x = nodes[q[1]];

				while (x.prev != null)
				{
					x = x.prev;
				}
				for (; x != null; x = x.next)
				{
					r.Add(x.id);
				}

				Console.WriteLine($"{r.Count} " + string.Join(" ", r));
			}
		}
		Console.Out.Flush();
	}
}
