using System;
using System.Collections.Generic;
using CoderLib6.DataTrees.Bsts;

class DI
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new IndexedMultiSet<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				set.Add(q[1]);
				Console.WriteLine(set.Count);
			}
			else if (q[0] == 1)
				Console.WriteLine(set.GetCount(q[1]));
			else if (q[0] == 2)
			{
				while (set.RemoveOne(q[1])) ;
			}
			else
				foreach (var x in set.GetItems(x => x >= q[1], x => x <= q[2]))
					Console.WriteLine(x);
		}
		Console.Out.Flush();
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public class IndexedMultiSet<T>
	{
		public int Count { get; }
		public IEnumerable<T> GetItems(Func<T, bool> startPredicate, Func<T, bool> endPredicate) { throw new NotImplementedException(); }
		public int GetCount(T item) { throw new NotImplementedException(); }
		public bool Add(T item) { throw new NotImplementedException(); }
		public bool RemoveOne(T item) { throw new NotImplementedException(); }
	}
}
