using System;
using System.Collections.Generic;

namespace CoderLib8.Extra
{
	static class CollectionHelper
	{
		static int[] ToOrder(int[] a, int max)
		{
			var o = Array.ConvertAll(new bool[max + 1], _ => -1);
			for (int i = 0; i < a.Length; ++i) o[a[i]] = i;
			return o;
		}

		static int[] Tally(int[] a, int max)
		{
			var c = new int[max + 1];
			foreach (var x in a) ++c[x];
			return c;
		}

		static int[] Tally(string s)
		{
			var c = new int[1 << 7];
			foreach (var x in s) ++c[x];
			return c;
		}
	}
}
