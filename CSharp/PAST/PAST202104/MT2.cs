using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees;

class MT2
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

		var set = AvlSet<(int l, int r)>.Create(_ => _.l);

		for (var (l, r) = (0, 1); r <= n; r++)
		{
			if (r == n || a[r] != a[r - 1])
			{
				set.Add((l, r));
				l = r;
			}
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var (L, R, X) = Read3();
			L--;

			var ranges = set.GetItems(_ => L < _.r, _ => _.l < R).ToArray();
			var (ll, _) = ranges[0];
			var (rl, rr) = ranges[^1];
			var lx = a[ll];
			var rx = a[rl];

			// 関連する区間を全て除きます。
			foreach (var (l, r) in ranges)
			{
				Add(a[l], -(r - l));
				set.Remove((l, r));
			}

			Add(lx, L - ll);
			Add(X, R - L);
			Add(rx, rr - R);

			a[L] = X;
			if (R < rr) a[R] = rx;

			if (ll < L) set.Add((ll, L));
			set.Add((L, R));
			if (R < rr) set.Add((R, rr));

			Console.WriteLine(sum);
		}
		Console.Out.Flush();
	}
}

// Dummy
namespace CoderLib6.DataTrees
{
	public class AvlSet<T>
	{
		public static AvlSet<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false) => throw new NotImplementedException();

		public IEnumerable<T> GetItems(Func<T, bool> predicateForMin, Func<T, bool> predicateForMax) => throw new NotImplementedException();
		public bool Add(T item) => throw new NotImplementedException();
		public bool Remove(T item) => throw new NotImplementedException();
	}
}
