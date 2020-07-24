using System;
using System.Collections.Generic;
using System.Linq;

class N2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var qs = new int[h[1]].Select(_ => Read()).Select(v => (v[0], v[1], v[2])).ToArray();

		var a = Enumerable.Range(0, n + 2).ToArray();
		var set = new HashSet<int>();
		var q = new Queue<int>();

		void Reverse(int x)
		{
			Swap(a, x, x + 1);
			for (int i = x - 1; i <= x + 1; i++)
				if (a[i] > a[i + 1]) set.Add(i);
				else set.Remove(i);
		}

		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				Reverse(x);
			}
			else
			{
				foreach (var i in set)
					if (x <= i && i < y) q.Enqueue(i);

				while (q.TryDequeue(out var i))
				{
					if (!set.Contains(i)) continue;
					Reverse(i);
					if (x <= i - 1 && set.Contains(i - 1)) q.Enqueue(i - 1);
					if (i + 1 < y && set.Contains(i + 1)) q.Enqueue(i + 1);
				}
			}
		}

		Console.WriteLine(string.Join(" ", a.Skip(1).Take(n)));
	}

	static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }
}
