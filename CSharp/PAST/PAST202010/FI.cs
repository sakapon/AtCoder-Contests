using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees.Bsts;

class FI
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		k--;
		var s = Array.ConvertAll(new int[n], _ => Console.ReadLine());

		// TODO: Use IndexedMultiMap.
		var comp = Comparer2<(int count, string s)>.Create(p => p.count, true);
		var set = new IndexedMultiSet<(int count, string s)>(comp);

		foreach (var g in s.GroupBy(w => w))
			set.Add((g.Count(), g.Key));
		var node = set.Root.SearchNode(k);

		if (node.SearchPreviousNode()?.Key.count == node.Key.count || node.SearchNextNode()?.Key.count == node.Key.count)
			return "AMBIGUOUS";
		return node.Key.s;
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public static class Comparer2<T>
	{
		public static IComparer<T> GetDefault()
		{
			if (typeof(T) == typeof(string)) return (IComparer<T>)StringComparer.Ordinal;
			return Comparer<T>.Default;
		}

		public static IComparer<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
		{
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

			var c = Comparer2<TKey>.GetDefault();
			if (descending)
				return Comparer<T>.Create((x, y) => c.Compare(keySelector(y), keySelector(x)));
			else
				return Comparer<T>.Create((x, y) => c.Compare(keySelector(x), keySelector(y)));
		}
	}

	public class IndexedMultiSet<T>
	{
		public class Node
		{
			public T Key { get; set; }
			public Node SearchPreviousNode() => throw new NotImplementedException();
			public Node SearchNextNode() => throw new NotImplementedException();
			public Node SearchNode(int index) => throw new NotImplementedException();
		}

		public Node Root { get; }
		public IndexedMultiSet(IComparer<T> comparer = null) { }
		public bool Add(T item) { throw new NotImplementedException(); }
	}
}
