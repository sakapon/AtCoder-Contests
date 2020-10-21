using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib6.Collections
{
	class Seq
	{
		int[] a;
		long[] s;
		public Seq(int[] _a) { a = _a; }

		public long Sum(int minIn, int maxEx)
		{
			if (s == null)
			{
				s = new long[a.Length + 1];
				for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
			}
			return s[maxEx] - s[minIn];
		}

		// C# 8.0
		//public long Sum(Range r) => Sum(r.Start.GetOffset(a.Length), r.End.GetOffset(a.Length));
	}

	static class SeqHelper
	{
		public static int[] CumSum(int[] a)
		{
			var s = new int[a.Length + 1];
			for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
			return s;
		}
		public static long[] CumSumL(int[] a)
		{
			var s = new long[a.Length + 1];
			for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
			return s;
		}

		public static int[] Pows(int n, int b)
		{
			var p = new int[n + 1];
			p[0] = 1;
			for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
			return p;
		}
		public static long[] Pows(int n, long b)
		{
			var p = new long[n + 1];
			p[0] = 1;
			for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
			return p;
		}

		public static int[] Pows2() => Enumerable.Range(0, 31).Select(i => 1 << i).ToArray();
		public static long[] Pows2L() => Enumerable.Range(0, 63).Select(i => 1L << i).ToArray();

		// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/3/DSL_3_D
		public static int[] SlideMin(int[] a, int k)
		{
			var r = new int[a.Length - k + 1];
			var q = new int[a.Length];
			for (int i = 1 - k, j = 0, s = 0, t = -1; j < a.Length; i++, j++)
			{
				while (s <= t && a[q[t]] >= a[j]) t--;
				q[++t] = j;
				if (i < 0) continue;
				r[i] = a[q[s]];
				if (q[s] == i) s++;
			}
			return r;
		}

		public static int[] SlideMax(int[] a, int k)
		{
			var r = new int[a.Length - k + 1];
			var q = new int[a.Length];
			for (int i = 1 - k, j = 0, s = 0, t = -1; j < a.Length; i++, j++)
			{
				while (s <= t && a[q[t]] <= a[j]) t--;
				q[++t] = j;
				if (i < 0) continue;
				r[i] = a[q[s]];
				if (q[s] == i) s++;
			}
			return r;
		}

		[Obsolete]
		static int[] SlideMin0(int[] a, int k)
		{
			var r = new int[a.Length - k + 1];
			var q = new List<int>();
			for (int i = 1 - k, j = 0; j < a.Length; i++, j++)
			{
				while (q.Count > 0 && a[q[q.Count - 1]] >= a[j]) q.RemoveAt(q.Count - 1);
				q.Add(j);
				if (i < 0) continue;
				r[i] = a[q[0]];
				if (q[0] == i) q.RemoveAt(0);
			}
			return r;
		}
	}
}
