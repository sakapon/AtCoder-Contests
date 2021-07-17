using System;
using System.Collections.Generic;

namespace CoderLib8.Linq
{
	// Test: https://codeforces.com/contest/1490/problem/D
	static class ArgHelper
	{
		public static int FirstArgMax(this int[] a)
		{
			if (a.Length == 0) throw new ArgumentException();
			var (mi, mv) = (0, a[0]);
			for (int i = 1; i < a.Length; i++)
				if (mv < a[i]) (mi, mv) = (i, a[i]);
			return mi;
		}

		public static T FirstArgMax<T>(this IEnumerable<T> source, Func<T, int> selector)
		{
			var (mo, mv, u) = (default(T), 0, false);
			foreach (var o in source)
			{
				var v = selector(o);
				if (u)
				{
					if (mv < v) (mo, mv) = (o, v);
				}
				else
				{
					u = true;
					(mo, mv) = (o, v);
				}
			}
			if (!u) throw new ArgumentException();
			return mo;
		}
	}
}
