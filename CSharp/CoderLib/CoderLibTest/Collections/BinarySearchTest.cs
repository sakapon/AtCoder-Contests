using System;
using System.Collections.Generic;
using System.Linq;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	[TestClass]
	public class BinarySearchTest
	{
		/// <summary>
		/// 条件 f を満たす最初の値を探索します。
		/// [l, x) 上で false、[x, r) 上で true となる x を返します。
		/// f(l) が true のとき、l を返します。
		/// f(r - 1) が false のとき、r を返します。
		/// f(r) は評価されません。
		/// </summary>
		static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}

		/// <summary>
		/// 条件 f を満たす最後の値を探索します。
		/// (l, x] 上で true、(x, r] 上で false となる x を返します。
		/// f(r) が true のとき、r を返します。
		/// f(l + 1) が false のとき、l を返します。
		/// f(l) は評価されません。
		/// </summary>
		static int Last(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
			return l;
		}

		/// <summary>
		/// 条件 f を満たす最初の値を指定された誤差の範囲内で探索します。
		/// (l, x) 上で false、[x, r) 上で true となる x を返します。
		/// r 近傍で false のとき、r を返します。
		/// </summary>
		static double First(double l, double r, Func<double, bool> f, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = l + (r - l) / 2)) r = m; else l = m;
			return r;
		}

		/// <summary>
		/// 条件 f を満たす最後の値を指定された誤差の範囲内で探索します。
		/// (l, x] 上で true、(x, r) 上で false となる x を返します。
		/// l 近傍で false のとき、l を返します。
		/// </summary>
		static double Last(double l, double r, Func<double, bool> f, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = r - (r - l) / 2)) l = m; else r = m;
			return l;
		}

		// 挿入先のインデックスを求めます。
		static int IndexForInsert(IList<int> a, int v) => First(0, a.Count, i => a[i] > v);

		// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は最初のインデックス。
		static int IndexOf(IList<int> a, int v)
		{
			var r = First(0, a.Count, i => a[i] >= v);
			return r < a.Count && a[r] == v ? r : ~r;
		}

		// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は最後のインデックス。
		static int LastIndexOf(IList<int> a, int v)
		{
			var r = Last(-1, a.Count - 1, i => a[i] <= v);
			return r >= 0 && a[r] == v ? r : ~(r + 1);
		}

		#region Test Methods

		[TestMethod]
		public void Index_Int()
		{
			var a = new[] { 3, 5, 6, 6, 6, 8 };
			for (int i = 0; i < 10; i++)
				Assert.AreEqual(Array.BinarySearch(a, i), IndexOf(a, i));
		}

		[TestMethod]
		public void IndexForInsert_Int()
		{
			var a = new[] { 3, 5, 6, 6, 6, 8 };
			Assert.AreEqual(0, IndexForInsert(a, 1));
			Assert.AreEqual(0, IndexForInsert(a, 2));
			Assert.AreEqual(1, IndexForInsert(a, 3));
			Assert.AreEqual(1, IndexForInsert(a, 4));
			Assert.AreEqual(2, IndexForInsert(a, 5));
			Assert.AreEqual(5, IndexForInsert(a, 6));
			Assert.AreEqual(5, IndexForInsert(a, 7));
			Assert.AreEqual(6, IndexForInsert(a, 8));
			Assert.AreEqual(6, IndexForInsert(a, 9));
		}

		[TestMethod]
		public void Index_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Index_Random(n);
				for (int n = 1000; n < 1010; n++) Index_Random(n);
			}
		}

		static void Index_Random(int n)
		{
			var a = RandomHelper.CreateData(n).OrderBy(x => x).ToArray();
			for (int i = -2; i < n + 2; i++)
			{
				var expected = Array.BinarySearch(a, i);
				var actual = IndexOf(a, i);
				if (expected >= 0)
				{
					Assert.AreEqual(i, a[actual]);
					Assert.IsTrue(actual == 0 || a[actual - 1] < i);
				}
				else
				{
					Assert.AreEqual(expected, actual);
				}
			}
		}

		[TestMethod]
		public void IndexForInsert_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) IndexForInsert_Random(n);
				for (int n = 1000; n < 1010; n++) IndexForInsert_Random(n);
			}
		}

		static void IndexForInsert_Random(int n)
		{
			var a = RandomHelper.CreateData(n).OrderBy(x => x).ToArray();
			for (int i = -2; i < n + 2; i++)
			{
				var expected = Array.BinarySearch(a, i);
				var actual = IndexForInsert(a, i);
				if (expected >= 0)
				{
					Assert.AreEqual(i, a[actual - 1]);
					Assert.IsTrue(actual == n || a[actual] > i);
				}
				else
				{
					Assert.AreEqual(~expected, actual);
				}
			}
		}

		[TestMethod]
		public void Index_Time()
		{
			var n = 300000;
			var a = RandomHelper.CreateData(n).OrderBy(x => x).ToArray();
			var r = TimeHelper.Measure(() => Enumerable.Range(0, n).Select(x => IndexOf(a, x)).ToArray());
		}

		[TestMethod]
		public void IndexForInsert_Time()
		{
			var n = 300000;
			var a = RandomHelper.CreateData(n).OrderBy(x => x).ToArray();
			var r = TimeHelper.Measure(() => Enumerable.Range(0, n).Select(x => IndexForInsert(a, x)).ToArray());
		}
		#endregion
	}
}
