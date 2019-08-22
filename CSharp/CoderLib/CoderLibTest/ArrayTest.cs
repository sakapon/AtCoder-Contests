using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ArrayTest
{
	static int DotProduct(int[] a, int[] b)
	{
		var r = 0;
		for (var i = 0; i < a.Length; i++) r += a[i] * b[i];
		return r;
	}

	#region Test Methods

	[TestMethod]
	public void DotProduct()
	{
		Assert.AreEqual(32, DotProduct(new[] { 1, 2, 3 }, new[] { 4, 5, 6 }));
		Assert.AreEqual(89, DotProduct(new[] { 2, 3, 5, 7 }, new[] { 11, 7, 5, 3 }));
	}
	#endregion
}
