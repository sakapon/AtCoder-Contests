using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var ps0 = ps.Where(p => p.Item1 == 0).Select(p => p.Item2).ToArray();
		var ps1 = ps.Where(p => p.Item1 == 1).Select(p => p.Item2).ToArray();
		var ps2 = ps.Where(p => p.Item1 == 2).Select(p => p.Item2).ToArray();

		Array.Sort(ps0);
		Array.Reverse(ps0);
		Array.Sort(ps1);
		Array.Sort(ps2);

		var pq = PQ<int>.Create();
		pq.PushRange(ps0[0..Math.Min(m, ps0.Length)]);
		while (pq.Count < m) pq.Push(0);

		var s1 = new Stack<int>(ps1);
		var s2 = new Stack<int>(ps2);

		var r = pq.Sum(x => (long)x);

		while (s1.Count > 0 && s2.TryPop(out var op))
		{
			var t = r;

			if (pq.Count == 0) break;
			t -= pq.Pop();

			for (int k = 0; k < op; k++)
			{
				if (s1.Count == 0) break;
				var x = s1.Pop();
				pq.Push(x);
				t += x;
				t -= pq.Pop();
			}

			r = Math.Max(r, t);
		}

		return r;
	}
}

class PQ<T> : List<T>
{
	public static PQ<T> Create(bool desc = false)
	{
		var c = Comparer<T>.Default;
		return desc ?
			new PQ<T>((x, y) => c.Compare(y, x)) :
			new PQ<T>(c.Compare);
	}

	Comparison<T> c;
	public T First => this[0];
	internal PQ(Comparison<T> _c) { c = _c; }

	void Swap(int i, int j) { var o = this[i]; this[i] = this[j]; this[j] = o; }
	void UpHeap(int i) { for (int j; i > 0 && c(this[j = (i - 1) / 2], this[i]) > 0; Swap(i, i = j)) ; }
	void DownHeap(int i)
	{
		for (int j; (j = 2 * i + 1) < Count;)
		{
			if (j + 1 < Count && c(this[j], this[j + 1]) > 0) j++;
			if (c(this[i], this[j]) > 0) Swap(i, i = j); else break;
		}
	}

	public void Push(T v)
	{
		Add(v);
		UpHeap(Count - 1);
	}
	public void PushRange(T[] vs) { foreach (var v in vs) Push(v); }

	public T Pop()
	{
		var r = this[0];
		this[0] = this[Count - 1];
		RemoveAt(Count - 1);
		DownHeap(0);
		return r;
	}
}
