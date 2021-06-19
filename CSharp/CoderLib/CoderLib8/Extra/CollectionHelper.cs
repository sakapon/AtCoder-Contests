using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Extra
{
	static class CollectionHelper
	{
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
		// a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		static Dictionary<T, int> Tally<T>(this T[] a)
		{
			var d = new Dictionary<T, int>();
			foreach (var o in a)
				if (d.ContainsKey(o)) ++d[o];
				else d[o] = 1;
			return d;
		}


		static List<int>[] TallyIndexes(this int[] a, int max)
		{
			var d = Array.ConvertAll(new bool[max + 1], _ => new List<int>());
			for (int i = 0; i < a.Length; ++i) d[a[i]].Add(i);
			return d;
		}

		static int[] ToInverseMap(this int[] a, int max)
		{
			var d = Array.ConvertAll(new bool[max + 1], _ => -1);
			for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
			return d;
		}

		// Enumerable.Range(0, a.Length).ToDictionary(i => a[i]);
		static Dictionary<T, int> ToInverseMap<T>(this T[] a)
		{
			var d = new Dictionary<T, int>();
			for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
			return d;
		}

		// cf. Collections.CompressionHashMap class
		static (int[] comp, Dictionary<int, int> map) Compress(this int[] a)
		{
			var c = a.Distinct().OrderBy(v => v).ToArray();
			var d = Enumerable.Range(0, c.Length).ToDictionary(i => c[i]);
			return (c, d);
		}

		// O(n)
		// a.Distinct().Count() == 1
		static bool AreAllSame(int[] a)
		{
			if (a.Length == 0) return false;
			for (int i = 1; i < a.Length; ++i)
				if (a[i - 1] != a[i]) return false;
			return true;
		}
	}
}
