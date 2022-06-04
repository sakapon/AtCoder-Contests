using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	class CompressionHashMap
	{
		public int[] Raw { get; }
		public int[] ReverseMap { get; }
		public Dictionary<int, int> Map { get; }
		public int this[int v] => Map[v];
		public int Count => ReverseMap.Length;

		int[] c;
		public int[] Compressed => c ??= Array.ConvertAll(Raw, v => Map[v]);

		public CompressionHashMap(int[] a)
		{
			// r = a.Distinct().OrderBy(v => v).ToArray();
			var hs = new HashSet<int>();
			foreach (var v in a) hs.Add(v);
			var r = new int[hs.Count];
			hs.CopyTo(r);
			Array.Sort(r);
			var map = new Dictionary<int, int>();
			for (int i = 0; i < r.Length; ++i) map[r[i]] = i;

			(Raw, ReverseMap, Map) = (a, r, map);
		}
	}

	public class IdMap<T>
	{
		List<T> l = new List<T>();
		Dictionary<T, int> map = new Dictionary<T, int>();

		public int Count => l.Count;
		public T this[int id] => l[id];
		public int GetId(T item) => map.ContainsKey(item) ? map[item] : -1;
		public bool Contains(T item) => map.ContainsKey(item);
		public int Add(T item)
		{
			if (map.ContainsKey(item)) return map[item];
			l.Add(item);
			return map[item] = l.Count - 1;
		}
	}
}
