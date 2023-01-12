using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, t, k) = Read4();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMapList(n + 1, es, false);
		var r = Dijkstra(n + 1, v => map[v].ToArray(), 1, n);
		return r[n] == long.MaxValue ? -1 : r[n];

		long[] Dijkstra(int n, Func<int, int[][]> nexts, int sv, int ev = -1)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var q = PriorityQueue<int>.CreateWithKey(v => costs[v]);
			costs[sv] = 0;
			q.Push(sv);

			while (q.Any)
			{
				var (v, c) = q.Pop();
				if (v == ev) break;
				if (costs[v] < c) continue;

				foreach (var e in nexts(v))
				{
					var (nv, nc) = (e[1], c + e[2]);

					// 改造
					if (e[3] > k && t + k - e[2] - e[3] < c && c < t - k + e[3])
					{
						nc += t - k + e[3] - c;
					}

					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					q.Push(nv);
				}
			}
			return costs;
		}
	}

	public static List<int[]>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2], e[3] });
		}
		return map;
	}
}

public class PriorityQueue<T>
{
	public static PriorityQueue<T> Create(bool descending = false)
	{
		var c = Comparer<T>.Default;
		return descending ?
			new PriorityQueue<T>((x, y) => c.Compare(y, x)) :
			new PriorityQueue<T>(c.Compare);
	}

	public static PriorityQueue<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
	{
		if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

		var c = Comparer<TKey>.Default;
		return descending ?
			new PriorityQueue<T>((x, y) => c.Compare(keySelector(y), keySelector(x))) :
			new PriorityQueue<T>((x, y) => c.Compare(keySelector(x), keySelector(y)));
	}

	public static PriorityQueue<T, TKey> CreateWithKey<TKey>(Func<T, TKey> keySelector, bool descending = false)
	{
		var c = Comparer<TKey>.Default;
		return descending ?
			new PriorityQueue<T, TKey>(keySelector, (x, y) => c.Compare(y.key, x.key)) :
			new PriorityQueue<T, TKey>(keySelector, (x, y) => c.Compare(x.key, y.key));
	}

	List<T> l = new List<T> { default };
	Comparison<T> c;

	public T First
	{
		get
		{
			if (l.Count <= 1) throw new InvalidOperationException("The heap is empty.");
			return l[1];
		}
	}

	public int Count => l.Count - 1;
	public bool Any => l.Count > 1;

	internal PriorityQueue(Comparison<T> comparison)
	{
		c = comparison ?? throw new ArgumentNullException(nameof(comparison));
	}

	// x の親: x/2
	// x の子: 2x, 2x+1
	void UpHeap(int i)
	{
		for (int j; (j = i >> 1) > 0 && c(l[j], l[i]) > 0; i = j)
			(l[i], l[j]) = (l[j], l[i]);
	}

	void DownHeap(int i)
	{
		for (int j; (j = i << 1) < l.Count; i = j)
		{
			if (j + 1 < l.Count && c(l[j], l[j + 1]) > 0) j++;
			if (c(l[i], l[j]) > 0) (l[i], l[j]) = (l[j], l[i]); else break;
		}
	}

	public void Push(T value)
	{
		l.Add(value);
		UpHeap(l.Count - 1);
	}

	public void PushRange(IEnumerable<T> values)
	{
		if (values != null) foreach (var v in values) Push(v);
	}

	public T Pop()
	{
		if (l.Count <= 1) throw new InvalidOperationException("The heap is empty.");

		var r = l[1];
		l[1] = l[l.Count - 1];
		l.RemoveAt(l.Count - 1);
		DownHeap(1);
		return r;
	}
}

// キーをキャッシュすることにより、キーが不変であることを保証します。
public class PriorityQueue<T, TKey> : PriorityQueue<(T value, TKey key)>
{
	Func<T, TKey> KeySelector;

	internal PriorityQueue(Func<T, TKey> keySelector, Comparison<(T value, TKey key)> comparison) : base(comparison)
	{
		KeySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
	}

	public void Push(T value)
	{
		Push((value, KeySelector(value)));
	}

	public void PushRange(IEnumerable<T> values)
	{
		if (values != null) foreach (var v in values) Push(v);
	}
}
