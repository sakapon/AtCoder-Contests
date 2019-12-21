using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BinarySearchTest
{
	/// <summary>
	/// 条件 f を満たす最初の値を探索します。
	/// [l, x) 上で false、[x, r) 上で true となる x を返します。
	/// f(l) が true のとき、l を返します。
	/// f(r - 1) が false のとき、r を返します。
	/// </summary>
	/// <param name="f">半開区間 [l, r) 上で定義される条件。</param>
	/// <param name="l">探索範囲の下限。</param>
	/// <param name="r">探索範囲の上限。</param>
	/// <returns>条件 f を満たす最初の値。</returns>
	static int First(Func<int, bool> f, int l, int r)
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
	/// </summary>
	/// <param name="f">半開区間 (l, r] 上で定義される条件。</param>
	/// <param name="l">探索範囲の下限。</param>
	/// <param name="r">探索範囲の上限。</param>
	/// <returns>条件 f を満たす最後の値。</returns>
	static int Last(Func<int, bool> f, int l, int r)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}

	/// <summary>
	/// 条件 f を満たす最初の値を指定された誤差の範囲内で探索します。
	/// (l, x) 上で false、[x, r) 上で true となる x を返します。
	/// l 近傍で true のとき、l を返します。
	/// r 近傍で false のとき、r を返します。
	/// </summary>
	/// <param name="f">開区間 (l, r) 上で定義される条件。</param>
	/// <param name="l">探索範囲の下限。</param>
	/// <param name="r">探索範囲の上限。</param>
	/// <param name="digits">誤差を表す小数部の桁数。</param>
	/// <returns>条件 f を満たす最初の値。</returns>
	public static double First(Func<double, bool> f, double l, double r, int digits = 9)
	{
		double m;
		while (Math.Round(r - l, digits) > 0) if (f(m = l + (r - l) / 2)) r = m; else l = m;
		return r;
	}

	/// <summary>
	/// 条件 f を満たす最後の値を指定された誤差の範囲内で探索します。
	/// (l, x] 上で true、(x, r) 上で false となる x を返します。
	/// r 近傍で true のとき、r を返します。
	/// l 近傍で false のとき、l を返します。
	/// </summary>
	/// <param name="f">開区間 (l, r) 上で定義される条件。</param>
	/// <param name="l">探索範囲の下限。</param>
	/// <param name="r">探索範囲の上限。</param>
	/// <param name="digits">誤差を表す小数部の桁数。</param>
	/// <returns>条件 f を満たす最後の値。</returns>
	public static double Last(Func<double, bool> f, double l, double r, int digits = 9)
	{
		double m;
		while (Math.Round(r - l, digits) > 0) if (f(m = r - (r - l) / 2)) l = m; else r = m;
		return l;
	}

	// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は先頭の番号。
	static int Index(IList<int> a, int v)
	{
		var r = First(i => a[i] >= v, 0, a.Count);
		return r < a.Count && a[r] == v ? r : ~r;
	}

	// 挿入先の番号を求めます。値が重複する場合は最後尾に挿入するときの番号です。すべて正の値です。
	static int IndexForInsert(IList<int> a, int v) => First(i => a[i] > v, 0, a.Count);

	// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は最後の番号。
	static int IndexLast(IList<int> a, int v)
	{
		var r = Last(i => a[i] <= v, -1, a.Count - 1);
		return r >= 0 && a[r] == v ? r : ~(r + 1);
	}

	#region Test Methods

	[TestMethod]
	public void Index_Int()
	{
		var a = new[] { 3, 5, 6, 6, 6, 8 };
		for (int i = 0; i < 10; i++)
			Assert.AreEqual(Array.BinarySearch(a, i), Index(a, i));
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

	static readonly Random random = new Random();

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
		var a = Enumerable.Range(0, n).Select(_ => random.Next(0, n)).OrderBy(x => x).ToArray();
		for (int i = -2; i < n + 2; i++)
		{
			var expected = Array.BinarySearch(a, i);
			var actual = Index(a, i);
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
		var a = Enumerable.Range(0, n).Select(_ => random.Next(0, n)).OrderBy(x => x).ToArray();
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
		var a = Enumerable.Range(0, n).Select(_ => random.Next(0, n)).OrderBy(x => x).ToArray();
		var r = TestHelper.MeasureTime(() => Enumerable.Range(0, n).Select(x => Index(a, x)).ToArray());
	}

	[TestMethod]
	public void IndexForInsert_Time()
	{
		var n = 300000;
		var a = Enumerable.Range(0, n).Select(_ => random.Next(0, n)).OrderBy(x => x).ToArray();
		var r = TestHelper.MeasureTime(() => Enumerable.Range(0, n).Select(x => IndexForInsert(a, x)).ToArray());
	}
	#endregion
}
