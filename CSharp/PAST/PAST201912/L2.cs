using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class L2
{
	struct R
	{
		public int i, j;
		public double Cost;
	}

	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		n = h[0]; m = h[1];
		ps1 = new int[n].Select(_ => read()).ToArray();
		ps2 = new int[m].Select(_ => read()).ToArray();

		Console.WriteLine(Enumerable.Range(0, (int)Math.Pow(2, m)).Min(i => Build(i)));
	}

	static int n, m;
	static int[][] ps1, ps2;

	static double Build(int f)
	{
		var fs = new BitArray(new[] { f });
		var ps = ps1.Concat(ps2.Where((x, i) => fs[i])).ToArray();
		Func<int, int, R> ToR = (i, j) => new R { i = i, j = j, Cost = Norm(ps[i], ps[j]) };

		var c = 0.0;
		var pq = new PQ<R>(Enumerable.Range(1, ps.Length - 1).Select(i => ToR(0, i)), (x, y) => Math.Sign(x.Cost - y.Cost));
		var u = new bool[ps.Length];
		u[0] = true;
		while (pq.Any())
		{
			var r = pq.Pop();
			if (u[r.i] && u[r.j]) continue;

			c += r.Cost;
			var k = u[r.i] ? r.j : r.i;
			for (int i = 1; i < ps.Length; i++)
				if (!u[i]) pq.Push(ToR(k, i));
			u[k] = true;
		}
		return c;
	}

	static double Norm(int[] p, int[] q)
	{
		int dx = p[0] - q[0], dy = p[1] - q[1];
		return (p[2] == q[2] ? 1 : 10) * Math.Sqrt(dx * dx + dy * dy);
	}
}

class PQ<T> : List<T>
{
	Comparison<T> _c;
	public T First => this[0];

	public PQ(IEnumerable<T> vs = null, Comparison<T> c = null)
	{
		_c = c ?? Comparer<T>.Default.Compare;
		if (vs != null) foreach (var v in vs) Push(v);
	}

	void Swap(int i, int j) { var o = this[i]; this[i] = this[j]; this[j] = o; }
	void UpHeap(int i) { for (int j; i > 0 && _c(this[(j = (i - 1) / 2)], this[i]) > 0; Swap(i, j), i = j) ; }
	void DownHeap(int i)
	{
		for (int j; (j = 2 * i + 1) < Count; i = j)
		{
			if (j + 1 < Count && _c(this[j], this[j + 1]) > 0) j++;
			if (_c(this[i], this[j]) > 0) Swap(i, j); else break;
		}
	}

	public void Push(T v) { Add(v); UpHeap(Count - 1); }
	public T Pop()
	{
		var r = this[0];
		this[0] = this[Count - 1];
		RemoveAt(Count - 1);
		DownHeap(0);
		return r;
	}
}
