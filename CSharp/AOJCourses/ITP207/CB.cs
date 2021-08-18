using System;
using System.Collections.Generic;
using CoderLib6.DataTrees.Bsts;

class CB
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<int>();
		var set = new IndexedSet<int>();

		for (int i = 0; i < n; i++)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				set.Add(q[1]);
				r.Add(set.Count);
			}
			else if (q[0] == 1)
				r.Add(set.Contains(q[1]) ? 1 : 0);
			else if (q[0] == 2)
				set.Remove(q[1]);
			else
				r.AddRange(set.GetItems(x => x >= q[1], x => x <= q[2]));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public class IndexedSet<T>
	{
		public int Count { get; }
		public IEnumerable<T> GetItems(Func<T, bool> startPredicate, Func<T, bool> endPredicate) { throw new NotImplementedException(); }
		public bool Contains(T item) { throw new NotImplementedException(); }
		public bool Add(T item) { throw new NotImplementedException(); }
		public bool Remove(T item) { throw new NotImplementedException(); }
	}
}
