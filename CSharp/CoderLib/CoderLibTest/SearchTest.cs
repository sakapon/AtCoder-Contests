using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SearchTest
{
	// 挿入先の番号を二分探索で求めます。値が重複する場合は後ろの番号です。
	static int Search(IList<int> l, int v) => l.Count > 0 ? Search(l, v, 0, l.Count) : 0;
	static int Search(IList<int> l, int v, int start, int count)
	{
		if (count == 1) return start + (v < l[start] ? 0 : 1);
		var c = count / 2;
		var s = start + c;
		return v < l[s] ? Search(l, v, start, c) : Search(l, v, s, count - c);
	}

	#region Test Methods

	[TestMethod]
	public void Search_Int()
	{
		var a = new[] { 3, 5, 6, 6, 6, 8 };
		Assert.AreEqual(0, Search(a, 1));
		Assert.AreEqual(0, Search(a, 2));
		Assert.AreEqual(1, Search(a, 3));
		Assert.AreEqual(1, Search(a, 4));
		Assert.AreEqual(2, Search(a, 5));
		Assert.AreEqual(5, Search(a, 6));
		Assert.AreEqual(5, Search(a, 7));
		Assert.AreEqual(6, Search(a, 8));
		Assert.AreEqual(6, Search(a, 9));
	}
	#endregion
}
