using System;
using System.Collections.Generic;
using System.Linq;

class N
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var qs = new int[h[1]].Select(_ => Read()).Select(v => (v[0], v[1], v[2])).ToArray();

		var a = Enumerable.Range(0, n + 2).ToArray();
		var st = new ST(n + 2);
		var q = new Queue<int>();
		var set = new HashSet<int>();

		void Reverse(int x)
		{
			Swap(a, x, x + 1);
			for (int i = -1; i < 2; i++)
				st.Set(x + i, a[x + i] < a[x + i + 1] ? 0 : 1);
		}

		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				Reverse(x);
			}
			else
			{
				foreach (var i in st.GetIndexes(x, y - 1))
				{
					q.Enqueue(i);
					set.Add(i);
				}

				while (q.TryDequeue(out var i))
				{
					Reverse(i);
					set.Remove(i);
					if (x <= i - 1 && !set.Contains(i - 1) && st.Get(i - 1) > 0)
					{
						q.Enqueue(i - 1);
						set.Add(i - 1);
					}
					if (i + 1 < y && !set.Contains(i + 1) && st.Get(i + 1) > 0)
					{
						q.Enqueue(i + 1);
						set.Add(i + 1);
					}
				}
			}
		}

		Console.WriteLine(string.Join(" ", a.Skip(1).Take(n)));
	}

	static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }
}

class ST
{
	int ln;
	long[][] vs;
	public ST(int n)
	{
		ln = (int)Math.Ceiling(Math.Log(n, 2));
		vs = Enumerable.Range(0, ln + 1).Select(k => new long[1 << ln - k]).ToArray();
	}

	(int k, int i)[] GetLevels(int i)
	{
		var r = new List<(int, int)>();
		for (int k = 0; k <= ln; k++, i /= 2) r.Add((k, i));
		return r.ToArray();
	}

	(int k, int i)[] GetRange(int m, int M)
	{
		var r = new List<(int, int)>();
		for (int k = 0, f = 1; k <= ln; k++, f *= 2)
		{
			if (m + f > M + 1) break;
			if ((m & f) != 0)
			{
				r.Add((k, m / f));
				m += f;
			}
		}
		for (int k = ln, f = 1 << ln; k >= 0; k--, f /= 2)
		{
			if (M - m + 1 >= f)
			{
				r.Add((k, m / f));
				m += f;
				if (m > M) break;
			}
		}
		return r.ToArray();
	}

	public int[] GetIndexes(int m, int M)
	{
		var r = new List<int>();
		var q = new Queue<(int k, int i)>(GetRange(m, M));
		while (q.TryDequeue(out var x))
		{
			var (k, i) = x;
			if (k == 0)
			{
				if (vs[k][i] > 0) r.Add(i);
			}
			else
			{
				k--; i *= 2;
				if (vs[k][i] > 0) q.Enqueue((k, i));
				i++;
				if (vs[k][i] > 0) q.Enqueue((k, i));
			}
		}
		return r.ToArray();
	}

	public void Add(int i, long v)
	{
		foreach (var (k, j) in GetLevels(i))
			vs[k][j] += v;
	}

	public long Get(int i) => vs[0][i];
	public void Set(int i, long v) => Add(i, v - vs[0][i]);
}
