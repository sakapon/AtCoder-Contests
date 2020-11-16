using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	static class CollectionHelper
	{
		// 項目数が多いほうにマージします。
		static Dictionary<TK, int> Merge<TK>(Dictionary<TK, int> d1, Dictionary<TK, int> d2)
		{
			if (d1.Count < d2.Count) (d1, d2) = (d2, d1);
			foreach (var (k, v) in d2)
				if (d1.ContainsKey(k)) d1[k] += v;
				else d1[k] = v;
			return d1;
		}
	}
}
