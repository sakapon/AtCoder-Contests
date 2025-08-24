using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	static class CollectionHelper
	{
		public static TV GetValue<TK, TV>(this Dictionary<TK, TV> d, TK k, TV v0 = default) => d.ContainsKey(k) ? d[k] : v0;

		public static List<T> Merge<T>(List<T> l1, List<T> l2)
		{
			if (l1.Count < l2.Count) (l1, l2) = (l2, l1);
			foreach (var v in l2)
				l1.Add(v);
			return l1;
		}

		// 項目数が大きいほうにマージします。
		public static Dictionary<TK, int> Merge<TK>(Dictionary<TK, int> d1, Dictionary<TK, int> d2)
		{
			if (d1.Count < d2.Count) (d1, d2) = (d2, d1);
			foreach (var (k, v) in d2)
				if (d1.ContainsKey(k)) d1[k] += v;
				else d1[k] = v;
			return d1;
		}

		// XOR の場合。SymmetricExceptWith メソッドでも可。
		public static HashSet<int> MergeXor(HashSet<int> s1, HashSet<int> s2)
		{
			if (s1.Count < s2.Count) (s1, s2) = (s2, s1);
			foreach (var v in s2)
				if (!s1.Add(v)) s1.Remove(v);
			return s1;
		}

		public static void Move<T>(ref List<T> from, ref List<T> to)
		{
			if (to.Count < from.Count) (to, from) = (from, to);
			foreach (var v in from) to.Add(v);
			from.Clear();
		}

		public static void Move<T>(ref HashSet<T> from, ref HashSet<T> to)
		{
			if (to.Count < from.Count) (to, from) = (from, to);
			foreach (var v in from) to.Add(v);
			from.Clear();
		}

		public static void Move<TK>(ref Dictionary<TK, int> from, ref Dictionary<TK, int> to)
		{
			if (to.Count < from.Count) (to, from) = (from, to);
			foreach (var (k, v) in from) to[k] = to.GetValueOrDefault(k) + v;
			from.Clear();
		}
	}
}
