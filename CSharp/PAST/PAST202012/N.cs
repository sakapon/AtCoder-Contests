using System;
using System.Collections.Generic;
using System.Linq;

class N
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();
		(int l, int r)[] lrs = Array.ConvertAll(new bool[n - 1], _ => Read2());
		(int a, int b)[] qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var q2 = lrs.Select((lr, i) => (x: lr.l, q: -1, i))
			.Concat(lrs.Select((lr, i) => (x: lr.r, q: 1, i)))
			.Concat(qs.Select((q, i) => (x: q.a, q: 0, i)))
			.OrderBy(q => q);

		var ll = new LinkedList<(int start, int end)>();
		var nodes = new LinkedListNode<(int start, int end)>[n - 1];
		var r = new int[qc];

		foreach (var q in q2)
		{
			if (q.q == 0)
			{
				var (a, b) = qs[q.i];
				if (b - 2 >= 0 && nodes[b - 2] != null)
				{
					var node = nodes[b - 2];
					var (s, e) = node.Value;
					r[q.i] = e - s + 2;
				}
				else if (b - 1 < n - 1 && nodes[b - 1] != null)
				{
					var node = nodes[b - 1];
					var (s, e) = node.Value;
					r[q.i] = e - s + 2;
				}
				else
				{
					r[q.i] = 1;
				}
			}
			else if (q.q == -1)
			{
				var ei = q.i;
				if (q.i - 1 >= 0 && nodes[q.i - 1] != null && q.i + 1 < n - 1 && nodes[q.i + 1] != null)
				{
					var node1 = nodes[q.i - 1];
					var node2 = nodes[q.i + 1];
					var nr = (node1.Value.start, node2.Value.end);
					if (node1.Value.end - node1.Value.start < node2.Value.end - node2.Value.start)
						(node1, node2) = (node2, node1);
					node1.Value = nr;
					for (int i = node2.Value.start; i <= node2.Value.end; i++)
					{
						nodes[i] = node1;
					}
					nodes[q.i] = node1;
				}
				else if (q.i - 1 >= 0 && nodes[q.i - 1] != null)
				{
					var node = nodes[q.i - 1];
					var (s, e) = node.Value;
					node.Value = (s, e + 1);
					nodes[q.i] = node;
				}
				else if (q.i + 1 < n - 1 && nodes[q.i + 1] != null)
				{
					var node = nodes[q.i + 1];
					var (s, e) = node.Value;
					node.Value = (s - 1, e);
					nodes[q.i] = node;
				}
				else
				{
					var node = new LinkedListNode<(int start, int end)>((q.i, q.i));
					nodes[q.i] = node;
				}
			}
			else
			{
				var ei = q.i;
				var node = nodes[ei];
				if (q.i - 1 >= 0 && nodes[q.i - 1] != null && q.i + 1 < n - 1 && nodes[q.i + 1] != null)
				{
					var r1 = (node.Value.start, ei - 1);
					var r2 = (ei + 1, node.Value.end);

					if (r1.Item2 - r1.start < r2.end - r2.Item1)
						(r1, r2) = (r2, r1);
					var nn = new LinkedListNode<(int start, int end)>(r2);
					node.Value = r1;
					for (int i = r2.Item1; i <= r2.end; i++)
					{
						nodes[i] = nn;
					}
				}
				else if (q.i - 1 >= 0 && nodes[q.i - 1] != null)
				{
					var (s, e) = node.Value;
					node.Value = (s, e - 1);
				}
				else if (q.i + 1 < n - 1 && nodes[q.i + 1] != null)
				{
					var (s, e) = node.Value;
					node.Value = (s + 1, e);
				}
				else
				{
				}
				nodes[ei] = null;
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
