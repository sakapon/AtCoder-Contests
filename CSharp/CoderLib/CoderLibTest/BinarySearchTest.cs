using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BinarySearchTest
{
	// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は先頭の番号。
	static int Index(IList<int> a, int v)
	{
		int l = 0, r = a.Count, m;
		while (l < r)
		{
			m = (l + r - 1) / 2;
			if (a[m] < v) l = m + 1; else r = m;
		}
		return r < a.Count && a[r] == v ? r : ~r;
	}

	// 挿入先の番号を求めます。値が重複する場合は最後尾です。すべて正の値です。
	static int IndexForInsert(IList<int> a, int v)
	{
		int l = 0, r = a.Count, m;
		while (l < r)
		{
			m = (l + r - 1) / 2;
			if (a[m] <= v) l = m + 1; else r = m;
		}
		return r;
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
	#endregion
}
