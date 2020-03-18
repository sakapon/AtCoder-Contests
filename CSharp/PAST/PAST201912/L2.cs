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
		var pq = PQ<R>.Create(x => x.Cost, Enumerable.Range(1, ps.Length - 1).Select(i => ToR(0, i)).ToArray());
		var u = new bool[ps.Length];
		u[0] = true;
		while (pq.Any)
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

class PQ<T>
{
	public static PQ<T> Create<TKey>(Func<T, TKey> getKey, T[] vs = null, bool desc = false)
	{
		var c = Comparer<TKey>.Default;
		return desc ?
			new PQ<T>(vs, (x, y) => c.Compare(getKey(y), getKey(x))) :
			new PQ<T>(vs, (x, y) => c.Compare(getKey(x), getKey(y)));
	}

	List<T> l = new List<T> { default(T) };
	Comparison<T> c;
	public T First => l[1];
	public int Count => l.Count - 1;
	public bool Any => l.Count > 1;

	PQ(T[] vs, Comparison<T> _c)
	{
		c = _c;
		if (vs != null) foreach (var v in vs) Push(v);
	}

	void Swap(int i, int j) { var o = l[i]; l[i] = l[j]; l[j] = o; }
	void UpHeap(int i) { for (int j; (j = i / 2) > 0 && c(l[j], l[i]) > 0; Swap(i, i = j)) ; }
	void DownHeap(int i)
	{
		for (int j; (j = 2 * i) < l.Count;)
		{
			if (j + 1 < l.Count && c(l[j], l[j + 1]) > 0) j++;
			if (c(l[i], l[j]) > 0) Swap(i, i = j); else break;
		}
	}

	public void Push(T v)
	{
		l.Add(v);
		UpHeap(Count);
	}
	public T Pop()
	{
		var r = l[1];
		l[1] = l[Count];
		l.RemoveAt(Count);
		DownHeap(1);
		return r;
	}
}
