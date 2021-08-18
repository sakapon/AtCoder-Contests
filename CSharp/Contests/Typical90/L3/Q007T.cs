using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees.Bsts;

class Q007T
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		var set = new IndexedSet<int>();
		foreach (var v in a) set.Add(v);
		set.Add(-1 << 30);
		set.Add(int.MaxValue);

		return string.Join("\n", qs.Select(GetMin));

		int GetMin(int bv)
		{
			var an2 = set.Root.SearchFirstNode(x => x >= bv);
			var an1 = an2.SearchPreviousNode();
			return Math.Min(an2.Item - bv, bv - an1.Item);
		}
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public class IndexedSet<T>
	{
		public class Node
		{
			public T Item { get; set; }
			public Node SearchFirstNode(Func<T, bool> predicate) => throw new NotImplementedException();
			public Node SearchLastNode(Func<T, bool> predicate) => throw new NotImplementedException();
			public Node SearchPreviousNode() => throw new NotImplementedException();
			public Node SearchNextNode() => throw new NotImplementedException();
		}

		public Node Root { get; }
		public T GetFirst() => throw new NotImplementedException();
		public T GetLast() => throw new NotImplementedException();
		public bool Contains(T item) => throw new NotImplementedException();
		public bool Add(T item) => throw new NotImplementedException();
		public bool Remove(T item) => throw new NotImplementedException();
	}
}
