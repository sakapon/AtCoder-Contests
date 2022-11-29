using System;
using System.Collections.Generic;

namespace EulerLib8.Numerics
{
	public static class Combinatorics
	{
		public static T[] PermutationAt<T>(this IEnumerable<T> source, long index)
		{
			var l = new List<T>(source);
			var r = new List<T>();

			var c = 1L;
			for (int i = 1; i <= l.Count; ++i) c *= i;

			for (int i = l.Count; i > 0; --i)
			{
				c /= i;
				var q = (int)Math.DivRem(index, c, out index);
				r.Add(l[q]);
				l.RemoveAt(q);
			}
			return r.ToArray();
		}
	}
}
