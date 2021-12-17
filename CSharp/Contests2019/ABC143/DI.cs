using System;
using System.Collections.Generic;
using CoderLib6.DataTrees.Bsts;

class DI
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var l = Read();

		var set = new IndexedMultiSet<int>();
		foreach (var x in l) set.Add(x);

		Array.Sort(l);

		var r = 0;
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				r += set.GetLastIndex(x => x < l[i] + l[j]) - j;
		Console.WriteLine(r);
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public class IndexedMultiSet<T>
	{
		public int GetFirstIndex(Func<T, bool> predicate) { throw new NotImplementedException(); }
		public int GetLastIndex(Func<T, bool> predicate) { throw new NotImplementedException(); }
		public bool Add(T item) { throw new NotImplementedException(); }
	}
}
