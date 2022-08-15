using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var c = Array.ConvertAll(Console.ReadLine().Split(), s => s[0]);
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var (gc, gis) = SCC(n + 1, es);
		gc--;

		var map = Array.ConvertAll(new bool[gc], _ => new HashSet<int>());
		foreach (var e in es)
		{
			var g0 = gis[e[0]];
			var g1 = gis[e[1]];
			if (g0 != g1) map[g0].Add(g1);
		}

		var gcls = Array.ConvertAll(map, _ => new List<char>());
		for (int v = 1; v <= n; ++v) gcls[gis[v]].Add(c[v - 1]);
		var gss = Array.ConvertAll(gcls, l =>
		{
			var cs = l.ToArray();
			Array.Sort(cs);
			return new string(cs);
		});

		var r = "~";
		var gsls = Array.ConvertAll(map, _ => new List<string>());

		for (int v = 0; v < gc; v++)
		{
			if (gsls[v].Count == 0) gsls[v].Add(gss[v]);

			foreach (var nv in map[v])
			{
				foreach (var s in gsls[v])
				{
					gsls[nv].Add(s + gss[nv]);
				}
			}

			if (map[v].Count == 0)
			{
				foreach (var s in gsls[v])
				{
					if (s.Length < k) continue;
					var min = Min(s);
					if (string.CompareOrdinal(r, min) > 0) r = min;
				}
			}
		}
		if (r == "~") return -1;
		return r;

		string Min(string s)
		{
			if (s.Length <= k) return s;

			var l = new List<char>();
			var pq = PQ<int>.Create(i => s[i] * 1000000L + i);
			for (int i = 0; i < s.Length - k; i++) pq.Push(i);
			var t = -1;

			for (int i = s.Length - k; i < s.Length; i++)
			{
				pq.Push(i);
				while (pq.First <= t) pq.Pop();

				var j = pq.Pop();
				l.Add(s[j]);
				t = j;
			}
			return new string(l.ToArray());
		}
	}

	// 1-based の場合、頂点 0 は最後のグループに含まれます。
	public static (int gc, int[] gis) SCC(int n, int[][] es)
	{
		var u = new bool[n];
		var t = n;
		var map = Array.ConvertAll(u, _ => new List<int>());
		var mapr = Array.ConvertAll(u, _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			mapr[e[1]].Add(e[0]);
		}

		var vs = new int[n];
		for (int v = 0; v < n; ++v) Dfs(v);

		Array.Clear(u, 0, n);
		var gis = new int[n];
		foreach (var v in vs) if (Dfsr(v)) ++t;
		return (t, gis);

		void Dfs(int v)
		{
			if (u[v]) return;
			u[v] = true;
			foreach (var nv in map[v]) Dfs(nv);
			vs[--t] = v;
		}

		bool Dfsr(int v)
		{
			if (u[v]) return false;
			u[v] = true;
			foreach (var nv in mapr[v]) Dfsr(nv);
			gis[v] = t;
			return true;
		}
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

	public static PQ<T> Create<TKey>(Func<T, TKey> toKey, bool desc = false)
	{
		var c = Comparer<TKey>.Default;
		return desc ?
			new PQ<T>((x, y) => c.Compare(toKey(y), toKey(x))) :
			new PQ<T>((x, y) => c.Compare(toKey(x), toKey(y)));
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
