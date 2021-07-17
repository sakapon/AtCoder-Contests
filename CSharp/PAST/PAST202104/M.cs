using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());
		var sum = d.Values.Sum(c => c * (c - 1) / 2);

		// 各グループ内の個数
		var rn = (int)Math.Sqrt(n);
		var gc = (n - 1) / rn + 1;

		var nodes = new LinkedListNode<(int l, int x)>[gc];
		LinkedListNode<(int l, int x)> GetNode(int i) => nodes[i / rn];

		var ll = new LinkedList<(int l, int x)>();
		ll.AddLast((-1, -1));

		for (int i = 0; i < n; i++)
		{
			if (ll.Last.Value.x != a[i])
			{
				ll.AddLast((i, a[i]));
			}

			if (i % rn == 0)
			{
				nodes[i / rn] = ll.Last;
			}
		}
		ll.AddLast((n, -1));

		Dictionary<int, int> Set(int l, int r, int x)
		{
			var lr = r - l;
			var d = new Dictionary<int, int> { [x] = lr };

			var ln = GetNode(l);
			while (ln.Value.l < l)
			{
				ln = ln.Next;
			}

			if (r < ln.Value.l)
			{
				var x0 = ln.Previous.Value.x;
				d[x0] = -lr;
				ll.AddBefore(ln, (l, x));
				ll.AddBefore(ln, (r, x0));

				ln = ln.Previous;
				for (int gi = (ln.Next.Value.l - 1) / rn; r <= gi * rn; gi--)
				{
					nodes[gi] = ln;
				}

				ln = ln.Previous;
				for (int gi = (r - 1) / rn; l <= gi * rn; gi--)
				{
					nodes[gi] = ln;
				}
			}
			else
			{
				// Partitions left
				if (l < ln.Value.l)
				{
					var x0 = ln.Previous.Value.x;
					d[x0] = d.GetValueOrDefault(x0) - (ln.Value.l - l);
				}

				// Inserts
				ln = ll.AddBefore(ln, (l, x));

				for (ln = ln.Next; ln.Value.l < r;)
				{
					var (l0, x0) = ln.Value;
					if (r < ln.Next.Value.l)
					{
						d[x0] = d.GetValueOrDefault(x0) - (r - l0);
						ln.Value = (r, x0);
					}
					else
					{
						d[x0] = d.GetValueOrDefault(x0) - (ln.Next.Value.l - l0);
						var t = ln;
						ln = ln.Next;
						ll.Remove(t);
					}
				}

				ln = ln.Previous;
				for (int gi = (r - 1) / rn; l <= gi * rn; gi--)
				{
					nodes[gi] = ln;
				}
			}

			return d;
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (l, r, x) in qs)
		{
			var delta = Set(l - 1, r, x);

			foreach (var (k, dc) in delta)
			{
				if (dc == 0) continue;

				var c = d.GetValueOrDefault(k);
				sum -= c * (c - 1) / 2;
				c += dc;
				sum += c * (c - 1) / 2;

				d[k] = c;
			}

			Console.WriteLine(sum);
		}
		Console.Out.Flush();
	}
}
