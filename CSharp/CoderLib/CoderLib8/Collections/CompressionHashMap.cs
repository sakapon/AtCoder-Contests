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
}
