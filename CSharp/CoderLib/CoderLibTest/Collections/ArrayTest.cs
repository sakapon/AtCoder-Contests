using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	[TestClass]
	public class ArrayTest
	{
		static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }

		static int[] Range(int start, int count)
		{
			var a = new int[count];
			for (var i = 0; i < count; ++i) a[i] = start + i;
			return a;
		}

		static int[] Range2(int m, int M)
		{
			var a = new int[M - m + 1];
			for (var i = m; i <= M; ++i) a[i - m] = i;
			return a;
		}

		static int KyotoNorm(int[] p, int[] q) => Math.Abs(p[0] - q[0]) + Math.Abs(p[1] - q[1]);
		//static long KyotoNorm((long x, long y) p, (long x, long y) q) => Math.Abs(q.x - p.x) + Math.Abs(q.y - p.y);

		static int DotProduct(int[] a, int[] b)
		{
			var r = 0;
			for (var i = 0; i < a.Length; i++) r += a[i] * b[i];
			return r;
		}

		static int DotProduct2(int[] a, int[] b) { for (int r = 0, i = -1; ; r += a[i] * b[i]) if (++i >= a.Length) return r; }

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

		static int[] SlideMin(int[] a, int k)
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

		static int[] SlideMax(int[] a, int k)
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

		#region Test Methods

		[TestMethod]
		public void Range()
		{
			CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 4 }, Range(0, 5));
			CollectionAssert.AreEqual(new[] { 2, 3, 4, 5, 6 }, Range(2, 5));
		}

		[TestMethod]
		public void Range2()
		{
			CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 4 }, Range2(0, 4));
			CollectionAssert.AreEqual(new[] { 2, 3, 4, 5, 6 }, Range2(2, 6));
		}

		[TestMethod]
		public void DotProduct()
		{
			Assert.AreEqual(32, DotProduct(new[] { 1, 2, 3 }, new[] { 4, 5, 6 }));
			Assert.AreEqual(89, DotProduct(new[] { 2, 3, 5, 7 }, new[] { 11, 7, 5, 3 }));
		}

		[TestMethod]
		public void SlideMin()
		{
			CollectionAssert.AreEqual(new[] { 5, 6, 7 }, SlideMin(new[] { 5, 6, 7, 8, 9 }, 3));
			CollectionAssert.AreEqual(new[] { 7, 6, 5 }, SlideMin(new[] { 9, 8, 7, 6, 5 }, 3));
			CollectionAssert.AreEqual(new[] { 6, 5, 5 }, SlideMin(new[] { 7, 6, 8, 5, 9 }, 3));
			CollectionAssert.AreEqual(new[] { 6, 6, 5 }, SlideMin(new[] { 7, 8, 6, 9, 5 }, 3));
			CollectionAssert.AreEqual(new[] { 5, 5, 5 }, SlideMin(new[] { 9, 7, 5, 6, 8 }, 3));
			CollectionAssert.AreEqual(new[] { 6, 7, 5 }, SlideMin(new[] { 6, 8, 9, 7, 5 }, 3));
		}

		[TestMethod]
		public void SlideMax()
		{
			CollectionAssert.AreEqual(new[] { 7, 8, 9 }, SlideMax(new[] { 5, 6, 7, 8, 9 }, 3));
			CollectionAssert.AreEqual(new[] { 9, 8, 7 }, SlideMax(new[] { 9, 8, 7, 6, 5 }, 3));
			CollectionAssert.AreEqual(new[] { 8, 8, 9 }, SlideMax(new[] { 7, 6, 8, 5, 9 }, 3));
			CollectionAssert.AreEqual(new[] { 8, 9, 9 }, SlideMax(new[] { 7, 8, 6, 9, 5 }, 3));
			CollectionAssert.AreEqual(new[] { 9, 7, 8 }, SlideMax(new[] { 9, 7, 5, 6, 8 }, 3));
			CollectionAssert.AreEqual(new[] { 9, 9, 9 }, SlideMax(new[] { 6, 8, 9, 7, 5 }, 3));
		}
		#endregion
	}
}
