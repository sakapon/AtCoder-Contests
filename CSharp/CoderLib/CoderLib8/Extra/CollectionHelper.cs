using System;
using System.Collections.Generic;

namespace CoderLib8.Extra
{
	static class CollectionHelper
	{
		static int[] ToOrder(this int[] a, int max)
		{
			var o = Array.ConvertAll(new bool[max + 1], _ => -1);
			for (int i = 0; i < a.Length; ++i) o[a[i]] = i;
			return o;
		}

		static int[] Tally(this int[] a, int max)
		{
			var r = new int[max + 1];
			foreach (var x in a) ++r[x];
			return r;
		}

		static int[] Tally(this string s, char start = 'A', int count = 26)
		{
			var r = new int[count];
			foreach (var c in s) ++r[c - start];
			return r;
		}

		// cf. Linq.GE.GroupCounts method
		static Dictionary<T, int> Tally<T>(this T[] a)
		{
			var d = new Dictionary<T, int>();
			foreach (var o in a)
				if (d.ContainsKey(o)) ++d[o];
				else d[o] = 1;
			return d;
		}
	}
}
