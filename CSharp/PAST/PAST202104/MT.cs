using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees;

class MT
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());
		var sum = d.Values.Sum(c => c * (c - 1) / 2);

		void Add(int x, int count)
		{
			var c = d.GetValueOrDefault(x);
			sum -= c * (c - 1) / 2;
			c += count;
			sum += c * (c - 1) / 2;
			d[x] = c;
		}

		var map = new AvlMap<int, int>();

		for (var (l, r) = (0, 1); r <= n; r++)
		{
			if (r == n || a[r] != a[r - 1])
			{
				map.Add(l, r);
				l = r;
			}
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var (L, R, X) = Read3();
			L--;

			var ranges = map.GetItems(p => L < p.Value, p => p.Key < R).ToArray();
			var (ll, _) = ranges[0];
			var (rl, rr) = ranges[^1];
			var lx = a[ll];
			var rx = a[rl];

			// 関連する区間を全て除きます。
			foreach (var (l, r) in ranges)
			{
				Add(a[l], -(r - l));
				map.Remove(l);
			}

			Add(lx, L - ll);
			Add(X, R - L);
			Add(rx, rr - R);

			a[L] = X;
			if (R < rr) a[R] = rx;

			if (ll < L) map.Add(ll, L);
			map.Add(L, R);
			if (R < rr) map.Add(R, rr);

			Console.WriteLine(sum);
		}
		Console.Out.Flush();
	}
}

// Dummy
namespace CoderLib6.DataTrees
{
	public class AvlMap<TKey, TValue>
	{
		public IEnumerable<KeyValuePair<TKey, TValue>> GetItems(Func<KeyValuePair<TKey, TValue>, bool> predicateForMin, Func<KeyValuePair<TKey, TValue>, bool> predicateForMax) => throw new NotImplementedException();
		public IEnumerable<KeyValuePair<TKey, TValue>> GetItems(Func<TKey, bool> predicateForMin, Func<TKey, bool> predicateForMax) => throw new NotImplementedException();
		public bool Add(TKey key, TValue value) => throw new NotImplementedException();
		public bool Remove(TKey key) => throw new NotImplementedException();

		public TValue this[TKey key]
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}
	}
}
